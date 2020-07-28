using System.Threading.Tasks;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class PosicionController : ControllerBase
    {
        private readonly IService _service;
        public PosicionController()
        {
            _service = Factory.GetService();
        }

        [HttpGet]
        public async Task<IActionResult> GetPosiciones()
        {
            string errMsg = $"{nameof(GetPosiciones)} un error se ha producido mientra se genera la lista de posiciones";

            var posiciones = await _service.GetPosiciones();

            if (posiciones == null)
            {
                return NotFound(new
                {
                    error = "No se encontro ningun record para las posiciones"
                });
            }

            return Ok(posiciones);
        }
    }
}