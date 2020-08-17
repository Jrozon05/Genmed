using System.Threading.Tasks;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionController : ControllerBase
    {
        private readonly IService _service;
        
        public DireccionController()
        {
            _service = Factory.GetService();
        }

        [HttpGet("provincias")]
        public async Task<IActionResult> GetProvincias()
        {
            string errMsg = $"{nameof(GetCiudades)} un error se ha producido mientras se genera la lista de ciudades";

            var provincias = await _service.GetProvincias();

            if (provincias == null)
            {
                return NotFound(new
                {
                    error = errMsg
                });
            }

            return Ok(JsonConvert.SerializeObject(provincias, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [HttpGet("ciudades/{provinciaid}")]
        public async Task<IActionResult> GetCiudades(int provinciaid) 
        {
            string errMsg = $"{nameof(GetCiudades)} un error se ha producido mientras se genera la lista de ciudades";

            var ciudades = await _service.GetCiudadesByProvincia(provinciaid);

            if (ciudades == null)
            {
                return NotFound(new
                {
                    error = errMsg
                });
            }

            return Ok(JsonConvert.SerializeObject(ciudades, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [HttpGet("sectores")]
        public async Task<IActionResult> GetSectores()
        {
            string errMsg = $"{nameof(GetCiudades)} un error se ha producido mientras se genera la lista de ciudades";

            var sectores = await _service.GetSectoresByCiudad();

            if (sectores == null)
            {
                return NotFound(new
                {
                    error = errMsg
                });
            }

            return Ok(JsonConvert.SerializeObject(sectores, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}