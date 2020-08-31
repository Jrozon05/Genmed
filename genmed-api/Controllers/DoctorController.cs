using System;
using System.Threading.Tasks;
using AutoMapper;
using genmed_api.Dtos.Doctor;
using genmed_data.Database;
using genmed_data.Services;
using genmed_api.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

                    if( !doctorRegistrarDto.Nombre.validarNombreApellido() || 
                        !doctorRegistrarDto.Apellido.validarNombreApellido() || 
                        !doctorRegistrarDto.Posicion.validarPosicion())
                        {
                            return BadRequest(new
                            {
                                error = errMsg
                            });
                        }
                    doctorCreated = await _service.CreateUpdateDoctor(doctor, doctorRegistrarDto.UsuarioId);
                    Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorRegistrarDto.UsuarioId);
                    await _service.AsignarUsuario(usuario);
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

        [HttpPost("actualizar")]
        public async Task<IActionResult> UpdateDoctor(DoctorActualizarDto doctorActualizarDto)
        {
            string errMsg =  $"{nameof(UpdateDoctor)} un error producido mientras se actualiza el doctor";
            Doctor doctorUpdated = new Doctor();
            
            var doctorTemp = await _service.GetDoctorByGuid(doctorActualizarDto.Guid);
            Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorTemp.Usuario.UsuarioId);
            
            if(doctorTemp == null || usuario == null)
            {
                return NotFound();
            }
            
            await _service.DesasignarUsuario(usuario);

            if(ModelState.IsValid)
            {
                try
                {
                    Doctor doctor = new Doctor();
                    doctor = _mapper.Map<Doctor>(doctorActualizarDto);

                    if( !doctorActualizarDto.Nombre.validarNombreApellido() || 
                        !doctorActualizarDto.Apellido.validarNombreApellido() || 
                        !doctorActualizarDto.Posicion.validarPosicion())
                    {
                        return BadRequest(new
                        {
                            error = errMsg
                        });
                    }

                    doctorUpdated = await _service.CreateUpdateDoctor(doctor, doctorActualizarDto.UsuarioId);
                    usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorActualizarDto.UsuarioId);
                    await _service.AsignarUsuario(usuario);
                }
                catch (Exception ex) 
                {
                    return BadRequest( new
                    {
                        error  = errMsg + ex
                    });
                }
            }
            return Ok(doctorUpdated);
        }

        [HttpPost("activar/{guid}")]
        public async Task<IActionResult> ActivateDoctor(Guid guid) 
        {
            string errMsg = $"{nameof(ActivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctor = new Doctor();
            bool doctorActivated = false;
            try {
                doctor = await _service.GetDoctorByGuid(guid);

                if (doctor != null)
                   {
                    doctorActivated = await _service.ActivateDoctor(doctor);
                    Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctor.Usuario.UsuarioId);
                    await _service.ActivateUsuario(usuario);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest( new
                {
                    error  = errMsg + ex
                });
            }
            return Ok(new {
                flag = doctorActivated
            });
        }

        [HttpPost("desactivar/{guid}")]
        public async Task<IActionResult> DeactivateDoctor(Guid guid) 
        {
            string errMsg = $"{nameof(DeactivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctor = new Doctor();
            bool doctorDeactivated = true;
            try {
                doctor = await _service.GetDoctorByGuid(guid);

                if (doctor != null)
                {
                    doctorDeactivated = await _service.DeactivateDoctor(doctor);
                    Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctor.Usuario.UsuarioId);
                    await _service.DeactivateUsuario(usuario);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest( new
                {
                    error  = errMsg + ex
                });
            }
            return Ok(new
            {
                flag = doctorDeactivated
            });
        }
    
    }
}