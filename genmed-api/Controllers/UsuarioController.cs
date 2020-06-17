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
            var values = await _service.GetUsuarioAsync();

            return Ok(values);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> CreateUsuario(UsuarioRegistrarDto usuarioRegistrarDto)
        {
            string errMsg = $"{nameof(CreateUsuario)} un error producido mientras la creacion de un nuevo usuario";

            if (ModelState.IsValid)
            {
                try
                {
                    usuarioRegistrarDto.NombreUsuario = usuarioRegistrarDto.NombreUsuario.ToLower();

                    string claveEncrypt = usuarioRegistrarDto.Clave.Encrypt();
                    var nuevoUsuario = _mapper.Map<Usuario>(usuarioRegistrarDto);
                    await _service.CreateUpdateUsuario(nuevoUsuario, claveEncrypt);
                }
                catch (Exception ex)
                {
                    return BadRequest(errMsg + ex);
                }
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            Usuario usuario = null;

            string claveEncrypt = usuarioLoginDto.Clave;
            usuario = await _service.Login(usuarioLoginDto.NombreUsuario, claveEncrypt.Encrypt());

            if (usuario == null)
                return Unauthorized(new {
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

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                usuario
            });
        }
    }
}