using System;
using System.Threading.Tasks;
using AutoMapper;
using genmed_api.Dtos.Doctor;
using genmed_data.Database;
using genmed_data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Reumed.Data.BusinessObjects;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public DoctorController(IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
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

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetDoctoresByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetDoctoresByGuid)} un error se ha producido mientras se busca informaciones del doctor";

            var doctor = await _service.GetDoctorByGuid(guid);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> CreateDoctor(DoctorRegistrarDto doctorRegistrarDto)
        {
            string errMsg =  $"{nameof(CreateDoctor)} un error producido mientras la creacion de un nuevo doctor";
            Doctor doctorCreated = new Doctor();
            
            if (ModelState.IsValid)
            {
                try 
                {
                    Doctor doctor = new Doctor();
                    doctor = _mapper.Map<Doctor>(doctorRegistrarDto);

                    doctorCreated = await _service.CreateUpdateDoctor(doctor, doctorRegistrarDto.UsuarioId);

                }
                catch (Exception ex) 
                {
                    return BadRequest( new
                    {
                        error  = errMsg + ex
                    });
                }
            }
            return Ok(doctorCreated);
        }

        [HttpPost("activar/{guid}")]
        public async Task<IActionResult> ActivateDoctor(Guid guid) {
            string errMsg = $"{nameof(ActivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctorActivated = new Doctor();
            try {
                doctorActivated = await _service.GetDoctorByGuid(guid);

                if (doctorActivated != null)
                {
                    doctorActivated = await _service.ActivateDoctor(doctorActivated);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest( new
                {
                    error  = errMsg + ex
                });
            }
            return Ok(doctorActivated);
        }

        [HttpPost("desactivar/{guid}")]
        public async Task<IActionResult> DeactivateDoctor(Guid guid) {
            string errMsg = $"{nameof(DeactivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctorDeactivated = new Doctor();
            try {
                doctorDeactivated = await _service.GetDoctorByGuid(guid);

                if (doctorDeactivated != null)
                {
                    doctorDeactivated = await _service.DeactivateDoctor(doctorDeactivated);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest( new
                {
                    error  = errMsg + ex
                });
            }
            return Ok(doctorDeactivated);
        }
    }
}