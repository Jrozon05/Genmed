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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
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

        private const string USUARIO_SESION = "cUsuario";
        private readonly IMemoryCache _memoryCache;

        public UsuarioController(IMapper mapper, IConfiguration config, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _config = config;
            _mapper = mapper;
            _service = Factory.GetService();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsuarios()
        {
            string errMsg = $"{nameof(GetUsuarios)} un error se ha producido mientras se genera la lista de usuarios";

            var values = await _service.GetUsuarioAsync();

            var usuario = _memoryCache.Get("cUsuario");

            if (values == null)
            {
                return StatusCode(202, errMsg);
            }

            return StatusCode(200, values);
        }

        [HttpGet("usuarionoasignado")]
        [Authorize]
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

        [HttpGet("{guid}")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetUsuarioByGuid)} un error se ha producido mientras se busca informaciones del usuario";

            var usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null, null, null);

            if (usuario == null)
                return StatusCode(202, errMsg);

            return StatusCode(200, usuario);
        }

        [HttpPost("registrar")]
        [Authorize]
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
                    return StatusCode(400, errMsg + ex);
                }
            }

            return StatusCode(200, usuarioCreated);
        }

        [HttpPost("actualizarclave")]
        [Authorize]
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

                    if (!usuarioActualizarClaveDto.Clave.validarClave())
                    {
                        return StatusCode(400, "La clave debe cumplir con el formato indicado.");
                    }

                    if (usuarioTemporal.Email == null)
                    {
                        return StatusCode(400, "No existe usuario con el correo electronico indicado.");
                    }

                    if (!usuarioActualizarClaveDto.Clave.Equals(usuarioActualizarClaveDto.ConfirmarClave))
                    {
                        return StatusCode(400, "Ambas claves deben ser iguales.");
                    }

                    if (usuarioTemporal.Clave.Equals(usuarioActualizarClaveDto.Clave.Encrypt()))
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
        }

        [HttpPost("actualizar")]
        [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            Usuario usuario = null;

            string claveEncrypt = usuarioLoginDto.Clave.Encrypt();
            usuario = await _service.Login(usuarioLoginDto.NombreUsuario, claveEncrypt);

            if (usuario == null)
            {
<<<<<<< HEAD
                return StatusCode(401, new 
                { 
                    error = "El nombre de usuario o la clave ha sido indicado de manera incorrecta"
                });
=======
                return StatusCode(401, new { error = "El nombre de usuario o la clave ha sido indicado de manera incorrecta" });
>>>>>>> b4394ef9ae9b01474bb3381817ac93e6654ad105
            }

            if (!usuario.Activo)
                return StatusCode(401, new
                {
                    error = "El usuario ha sido desactivado"
                });

            var cacheExpirationOptions =
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(20),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            
            _memoryCache.Set("cUsuario", usuario, cacheExpirationOptions);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Guid.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claimsIdentity = new ClaimsIdentity(claims);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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