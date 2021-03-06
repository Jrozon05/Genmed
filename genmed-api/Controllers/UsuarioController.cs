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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reumed.Data.BusinessObjects;
using genmed_api.Entidad;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
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
                return StatusCode(Status.Accepted, new 
                { 
                    error = errMsg
                });
            }

            return StatusCode(Status.OK, values);
        }

        [HttpGet("usuarionoasignado")]
        [Authorize]
        public async Task<IActionResult> GetUsuariosNoAsignado()
        {
            string errMsg = $"{nameof(GetUsuarios)} un error se ha producido mientras se genera la lista de usuarios";

            var values = await _service.GetUsuariosNoAsignado();

            if (values == null)
            {
                return StatusCode(Status.Accepted, new 
                { 
                    error = errMsg
                });
            }

            return StatusCode(Status.OK, values);
        }

        [HttpGet("{guid}")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetUsuarioByGuid)} un error se ha producido mientras se busca informaciones del usuario";

            var usuario = await _service.GetUsuarioByGuid(guid);

            if (usuario == null)
                return StatusCode(Status.Accepted, new 
                { 
                    error = errMsg
                });

            return StatusCode(Status.OK, usuario);
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
                        return StatusCode(Status.Accepted, new 
                        { 
                            error = "No se ha indicado un nombre usuario, debe intentarlo nuevamente."
                        });
                    }

                    if(!usuarioRegistrarDto.Email.validarEmail())
                    {
                        return StatusCode(Status.Accepted, new 
                        { 
                            error = "No se ha indicado un correo electronico, debe intentarlo nuevamente."
                        });
                    }

                    var usuarioExiste = await _service.GetUsuarioByNombreUsuario(usuarioRegistrarDto.NombreUsuario);
                    
                    if(usuarioExiste == null)
                    {
                        usuarioExiste = await _service.GetUsuarioByEmail(usuarioRegistrarDto.Email);
                    }

                    if (usuarioExiste != null)
                    {
                        if (usuarioExiste.NombreUsuario != null && usuarioExiste.NombreUsuario.Equals(usuarioRegistrarDto.NombreUsuario))
                        {
                            return StatusCode(Status.Accepted, new 
                            { 
                                error = "El nombre usuario: " + usuario.NombreUsuario + " actualmente existe." 
                            });
                        }

                        if (usuarioExiste.Email != null && usuarioExiste.Email.Equals(usuarioRegistrarDto.Email))
                        {
                            return StatusCode(Status.Accepted, new 
                            { 
                                error = "El correo electronico: " + usuario.Email + " actualmente existe." 
                            });
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
                    return StatusCode(Status.BadRequest, errMsg + ex);
                }
            }

            return StatusCode(Status.OK, usuarioCreated);
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
                    Usuario usuarioTemporal = await _service.GetUsuarioByGuid(usuario.Guid);

                    if (!usuarioActualizarClaveDto.Clave.validarClave())
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "La clave debe cumplir con el formato indicado."
                        });
                    }

                    if (usuarioTemporal.Email == null)
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "No existe usuario con el correo electronico indicado."
                        });
                    }

                    if (!usuarioActualizarClaveDto.Clave.Equals(usuarioActualizarClaveDto.ConfirmarClave))
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "Ambas claves deben ser iguales."
                        });
                    }

                    if (usuarioTemporal.Clave.Equals(usuarioActualizarClaveDto.Clave.Encrypt()))
                    {
                        return StatusCode(Status.Accepted, new 
                        { 
                            error = "Debes seleccionar una clave nueva."
                        });
                    }

                    result = await _service.UpdateClaveUsuario(usuario, claveEncrypt);

                }
                catch (Exception ex)
                {
                    return StatusCode(Status.BadRequest, new 
                    { 
                        error = errMsg + ex
                    });
                }
            }

            return StatusCode(Status.OK,  new 
            { 
                flag = result
            });
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
                    Usuario usuarioTemporal = await _service.GetUsuarioByGuid(usuario.Guid);
                    
                    if (usuarioTemporal.Email == null)
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "El correo electronico indicado es nulo."
                        });
                    }
                    
                    if(!usuarioTemporal.Email.Equals(usuario.Email))
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "Se ha intentado modificar el correo electronico para el usuario indicado."
                        });
                    }

                    if(!usuarioActualizarDto.NombreUsuario.validarUserName())
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "El nombre de usuario debe cumplir con el patron correcto."
                        });
                    }

                    if(!usuarioActualizarDto.Email.validarEmail())
                    {
                        return StatusCode(Status.Accepted,  new 
                        { 
                            error = "El correo electronico debe cumplir con el patron correcto."
                        });
                    }

                    usuarioUpdated = await _service.CreateUpdateUsuario(usuario, usuarioActualizarDto.RolId);

                }

                catch (Exception ex)
                {
                    return StatusCode(Status.Accepted, new 
                    { 
                        error = errMsg + ex
                    });
                }
            }
            return StatusCode(Status.OK, usuarioUpdated);
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
                return StatusCode(Status.Unauthorized, new 
                { 
                    error = "El nombre de usuario o la clave ha sido indicado de manera incorrecta" 
                });
            }

            if (!usuario.Activo)
                return StatusCode(Status.Unauthorized, new
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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return StatusCode(Status.OK, new
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
                usuario = await _service.GetUsuarioByGuid(guid);

                if (usuario != null)
                {
                    usuarioActivated = await _service.ActivateUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Status.BadRequest,  new 
                { 
                    error = errMsg + ex
                });
            }
            return StatusCode(Status.OK,  new 
            { 
                flag = usuarioActivated
            });
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
                usuario = await _service.GetUsuarioByGuid(guid);

                if (usuario != null)
                {
                    usuarioDeactivated = await _service.DeactivateUsuario(usuario);
                    await _service.DeactivateUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Status.BadRequest,  new 
                { 
                    error = errMsg + ex
                });
            }
            
            return StatusCode(Status.OK,  new 
            { 
                flag = usuarioDeactivated
            });
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
                usuario = await _service.GetUsuarioByGuid(guid);

                if (usuario != null)
                {
                    if(usuario.verificarDisponibilidadUsuario())
                    {
                        usuarioAsignado = await _service.AsignarUsuario(usuario);
                    }
                    return StatusCode(200, new 
                    {
                        error = $"El usuario {usuario.NombreUsuario} no esta disponible, por favor intente con otro."
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Status.BadRequest,  new 
                { 
                    error = errMsg + ex
                });
            }
            
            return StatusCode(Status.OK,  new 
            { 
                flag = usuarioAsignado
            });
        }

        /*
        TODO:
            Elavuar si es necesario este metodo ya que los usuarios desactivados 
            no son desasignados
        */
        [HttpPost("desasignar/{guid}")]
        [Authorize]
        public async Task<IActionResult> DeasignarUsuario(Guid guid)
        {
            string errMsg = $"{nameof(AsignarUsuario)} un error se ha producido mientras se busca informaciones del usuario";

            Usuario usuario = new Usuario();
            bool usuarioAsignado = false;
            try
            {
                usuario = await _service.GetUsuarioByGuid(guid);

                if (usuario != null)
                {
                    usuarioAsignado = await _service.DesasignarUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Status.BadRequest,  new 
                { 
                    error = errMsg + ex
                });
            }
            
            return StatusCode(Status.OK,  new 
            { 
                flag = usuarioAsignado
            });
        }

    }
}