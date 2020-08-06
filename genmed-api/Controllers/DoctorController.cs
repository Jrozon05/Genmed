using System.Threading.Tasks;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IService _service;

        public DoctorController()
        {
            _service = Factory.GetService();
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctores() 
        {
            string errMsg = $"{nameof(GetDoctores)} un error se ha producido mientras se genera la lista de doctores";

            var doctores = await _service.GetDoctoresAsync();

            if (doctores == null)
            {
                return BadRequest(new
                {
                    error = errMsg
                });
            }

            return Ok(doctores);
        }
    }
}