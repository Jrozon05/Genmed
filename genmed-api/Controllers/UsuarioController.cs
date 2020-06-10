using System.Threading.Tasks;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Mvc;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IService _service;
        public UsuarioController()
        {
            _service = Factory.GetService();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var values = await _service.GetUsuarioAsync();

            return Ok(values);
        }
    }
}