using System;
using System.Threading.Tasks;
using AutoMapper;
using genmed_api.Dtos.Usuario;
using genmed_api.Utils.Extensions;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Mvc;
using Reumed.Data.BusinessObjects;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper)
        {
            _mapper = mapper;
            _service = Factory.GetService();
        }

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


    }
}