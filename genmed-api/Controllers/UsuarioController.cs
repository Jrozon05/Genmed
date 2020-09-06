using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using genmed_api.Dtos.Usuario;
using genmed_api.Utils.Extensions;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Reumed.Data.BusinessObjects;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuarioController(IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _service = Factory.GetService();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            string errMsg = $"{nameof(GetUsuarios)} un error se ha producido mientras se genera la lista de usuarios";

            var values = await _service.GetUsuarioAsync();

            if (values == null)
            {
                return StatusCode(202, errMsg);
            }

            return StatusCode(200, values);
        }

        [Authorize]
        [HttpGet("usuarionoasignado")]
        public async Task<IActionResult> GetUsuariosNoAsignado()
        {
            string errMsg = $"{nameof(GetUsuarios)} un error se ha producido mientras se genera la lista de usuarios";

            var values = await _service.GetUsuariosNoAsignado();

            if (values == null)
            {
                return StatusCode(202, errMsg);
            }

            return StatusCode(200, values);
        }

        [Authorize]
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUsuarioByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetUsuarioByGuid)} un error se ha producido mientras se busca informaciones del usuario";

            var usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

            if (usuario == null)
                return StatusCode(202, errMsg);

            return StatusCode(200, usuario);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> CreateUsuario(UsuarioRegistrarDto usuarioRegistrarDto)
        {
            string errMsg = $"{nameof(CreateUsuario)} un error producido mientras la creacion de un nuevo usuario";
            Usuario usuarioCreated = new Usuario();

            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuarioRegistrarDto.NombreUsuario = usuarioRegistrarDto.NombreUsuario.ToLower();
                    usuario = _mapper.Map<Usuario>(usuarioRegistrarDto);
                    
                    if(!usuarioRegistrarDto.NombreUsuario.validarUserName())
                    {
                        return StatusCode(202, new { Error = "No se ha indicado un nombre usuario, debe intentarlo nuevamente."});
                    }

                    if(!usuarioRegistrarDto.Email.validarEmail())
                    {
                        return StatusCode(202, new { Error = "No se ha indicado un correo electronico, debe intentarlo nuevamente."});
                    }

                    var usuarioExiste = await _service.GetUsuarioByGuidOrNombreUsuario(null, usuarioRegistrarDto.NombreUsuario, null, usuarioRegistrarDto.Email);

                    if (usuarioExiste != null) 
                    {
                        if (usuarioExiste.NombreUsuario != null && usuarioExiste.NombreUsuario.Equals(usuarioRegistrarDto.NombreUsuario))
                        {
                            return StatusCode(202, new { Error = "El nombre usuario: " + usuario.NombreUsuario + " actualmente existe." });
                        }

                        if (usuarioExiste.Email != null && usuarioExiste.Email.Equals(usuarioRegistrarDto.Email))
                        {
                            return StatusCode(202, new { Error = "El correo electronico: " + usuario.Email + " actualmente existe." });
                        }
                    }

                    usuarioCreated = await _service.CreateUpdateUsuario(usuario, usuarioRegistrarDto.RolId);
                    var createClave = _mapper.Map<UsuarioActualizarClaveDto>(usuarioCreated);
                    createClave.Clave = usuarioRegistrarDto.Clave;
                    createClave.ConfirmarClave = usuarioRegistrarDto.ConfirmarClave;

                    await UpdateClaveUsuario(createClave);
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        error = errMsg + ex
                    });
                }
            }

            return Ok(usuarioCreated);
        }

        [HttpPost("actualizarclave")]
        public async Task<IActionResult> UpdateClaveUsuario(UsuarioActualizarClaveDto usuarioActualizarClaveDto)
        {
            string errMsg = $"{nameof(UpdateUsuario)} un error producido mientras se actualiza la clave del usuario";

            var result = false;

            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    string claveEncrypt = usuarioActualizarClaveDto.Clave.Encrypt();
                    usuario = _mapper.Map<Usuario>(usuarioActualizarClaveDto);
                    Usuario usuarioTemporal = await _service.GetUsuarioByGuidOrNombreUsuario(usuario.Guid, null, null, null);

                    if(!usuarioActualizarClaveDto.Clave.validarClave())
                    {
                        return StatusCode(400, "La clave debe cumplir con el formato indicado.");
                    }

                    if(usuarioTemporal.Email == null)
                    {
                        return StatusCode(400, "No existe usuario con el correo electronico indicado.");
                    }

                    if (!usuarioActualizarClaveDto.Clave.Equals(usuarioActualizarClaveDto.ConfirmarClave))
                    {
                        return StatusCode(400, "Ambas claves deben ser iguales.");
                    }

                    if(usuarioTemporal.Clave.Equals(usuarioActualizarClaveDto.Clave.Encrypt()))
                    {
                        return StatusCode(400, "Debes seleccionar una clave nueva.");
                    }

                    result = await _service.UpdateClaveUsuario(usuario, claveEncrypt);

                }
                catch (Exception ex)
                {
                    return StatusCode(400, errMsg + ex);
                }
            }

            return StatusCode(200, result);
            // return Ok(new
            // {
            //     flag = result
            // });
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> UpdateUsuario(UsuarioActualizarDto usuarioActualizarDto)
        {
            string errMsg = $"{nameof(UpdateUsuario)} un error producido mientras se actualiza el usuario";
            Usuario usuarioUpdated = new Usuario();

            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuario = _mapper.Map<Usuario>(usuarioActualizarDto);
                    Usuario usuarioTemporal = await _service.GetUsuarioByGuidOrNombreUsuario(usuario.Guid, null, null, null);
                    
                    if (usuarioTemporal.Email == null)
                    {
                        return StatusCode(400, "El correo electronico indicado es nulo.");
                    }
                    
                    if(!usuarioTemporal.Email.Equals(usuario.Email))
                    {
                        return StatusCode(400, "Se ha intentado modificar el correo electronico para el usuario indicado.");
                    }

                    if(!usuarioActualizarDto.NombreUsuario.validarUserName())
                    {
                        return StatusCode(400, "El nombre de usuario debe cumplir con el patron correcto.");
                    }

                    if(!usuarioActualizarDto.Email.validarEmail())
                    {
                        return StatusCode(400, "El correo electronico debe cumplir con el patron correcto.");
                    }

                    usuarioUpdated = await _service.CreateUpdateUsuario(usuario, usuarioActualizarDto.RolId);
                    
                }

                catch (Exception ex)
                {
                    return StatusCode(400, errMsg + ex);
                }
            }
            return StatusCode(200, usuarioUpdated);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            Usuario usuario = null;

            string claveEncrypt = usuarioLoginDto.Clave.Encrypt();
            usuario = await _service.Login(usuarioLoginDto.NombreUsuario, claveEncrypt);

            if (usuario == null)
            {
                return StatusCode(401, "El nombre de usuario o la clave ha sido indicado de manera incorrecta");
            }

            if (!usuario.Activo)
            {
                return StatusCode(401, "El usuario ha sido desactivado");       
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Guid.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return StatusCode(200, new
            {
                token = tokenHandler.WriteToken(token),
                usuario
            });
        }

        [HttpPost("activar/{guid}")]
        public async Task<IActionResult> ActivateUsuario(Guid guid)
        {
            string errMsg = $"{nameof(ActivateUsuario)} un error se ha producido mientras se busca informaciones del usuario";

            Usuario usuario = new Usuario();
            bool usuarioActivated = false;
            try
            {
                usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

                if (usuario != null)
                {
                    usuarioActivated = await _service.ActivateUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, errMsg + ex);
            }
            return StatusCode(200, usuarioActivated);
        }

        [HttpPost("desactivar/{guid}")]
        public async Task<IActionResult> DeactivateUsuario(Guid guid)
        {
            string errMsg = $"{nameof(DeactivateUsuario)} un error se ha producido mientras se busca informaciones del usuario";

            Usuario usuario = new Usuario();
            bool usuarioDeactivated = true;
            try
            {
                usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

                if (usuario != null)
                {
                    usuarioDeactivated = await _service.DeactivateUsuario(usuario);
                    await _service.DeactivateUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, errMsg + ex);
            }
            
            return StatusCode(200, usuarioDeactivated);
        }

        [HttpPost("asignar/{guid}")]
        public async Task<IActionResult> AsignarUsuario(Guid guid)
        {
            string errMsg = $"{nameof(AsignarUsuario)} un error se ha producido mientras se busca informaciones del usuario";

            Usuario usuario = new Usuario();
            bool usuarioAsignado = false;
            try
            {
                usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

                if (usuario != null)
                {
                    usuarioAsignado = await _service.AsignarUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, errMsg + ex);
            }
            return StatusCode(200, usuarioAsignado);
        }

        [HttpPost("desasignar/{guid}")]
        public async Task<IActionResult> DeasignarUsuario(Guid guid)
        {
            string errMsg = $"{nameof(AsignarUsuario)} un error se ha producido mientras se busca informaciones del usuario";

            Usuario usuario = new Usuario();
            bool usuarioAsignado = false;
            try
            {
                usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

                if (usuario != null)
                {
                    usuarioAsignado = await _service.DesasignarUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, errMsg + ex);
            }
            return StatusCode(200, usuarioAsignado);
        }

    }
}