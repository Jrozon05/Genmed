using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using genmed_api.Dtos.Usuario;
using genmed_api.Utils.Extensions;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Authorization;
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
                return BadRequest(new
                {
                    error = errMsg
                });
            }

            return Ok(values);
        }

        [Authorize]
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUsuarioByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetUsuarioByGuid)} un error se ha producido mientras se busca informaciones del usuario";

            var usuario = await _service.GetUsuarioByGuidOrNombreUsuario(guid, null);

            if (usuario == null)
                return NotFound();

            return Ok(JsonConvert.SerializeObject(usuario, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
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

                    var usuarioExiste = await _service.GetUsuarioByGuidOrNombreUsuario(null, usuarioRegistrarDto.NombreUsuario);

                    if (usuarioExiste.NombreUsuario != null)
                    {
                        return Ok(new { Error = "El nombre usuario: " + usuario.NombreUsuario + " actualmente existe." });
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

            return Ok(JsonConvert.SerializeObject(usuarioCreated, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [HttpPut("actualizarclave")]
        public async Task<IActionResult> UpdateClaveUsuario(UsuarioActualizarClaveDto usuarioActualizarClaveDto)
        {
            string errMsg =  $"{nameof(UpdateUsuario)} un error producido mientras se actualiza la clave del usuario";
            Usuario usuarioUpdated = new Usuario();

            if(ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    string claveEncrypt = usuarioActualizarClaveDto.Clave.Encrypt();  
                    usuario = _mapper.Map<Usuario>(usuarioActualizarClaveDto);
                    if(usuarioActualizarClaveDto.Clave.Equals(usuarioActualizarClaveDto.ConfirmarClave))
                    {
                        await _service.UpdateClaveUsuario(usuario, claveEncrypt);
                    }
                    else {
                        return NotFound();
                    }

                }
                catch (Exception ex) 
                {
                    return BadRequest( new
                    {
                        error  = errMsg + ex
                    });
                }
            }
            return Ok(JsonConvert.SerializeObject(usuarioUpdated, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> UpdateUsuario(UsuarioActualizarDto usuarioActualizarDto)
        {
            string errMsg =  $"{nameof(UpdateUsuario)} un error producido mientras se actualiza el usuario";
            Usuario usuarioUpdated = new Usuario();

            if(ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuario = _mapper.Map<Usuario>(usuarioActualizarDto);
                    usuarioUpdated = await _service.CreateUpdateUsuario(usuario, usuarioActualizarDto.RolId);

                }
                catch (Exception ex) 
                {
                    return BadRequest( new
                    {
                        error  = errMsg + ex
                    });
                }
            }
            return Ok(JsonConvert.SerializeObject(usuarioUpdated, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            Usuario usuario = null;

            string claveEncrypt = usuarioLoginDto.Clave;
            usuario = await _service.Login(usuarioLoginDto.NombreUsuario, claveEncrypt.Encrypt());

            if (usuario == null)
                return Unauthorized(new
                {
                    error = "El nombre de usuario o la clave esta incorrecta"
                });
                
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

            return Ok(JsonConvert.SerializeObject(new
            {
                token = tokenHandler.WriteToken(token),
                usuario
            }, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}