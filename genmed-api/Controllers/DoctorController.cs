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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace genmed_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _memoryCache;

        public DoctorController(IMapper mapper, IConfiguration config, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _mapper = mapper;
            _config = config;
            _service = Factory.GetService();
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctores()
        {
            string errMsg = $"{nameof(GetDoctores)} un error se ha producido mientras se genera la lista de doctores";

            var usuario = _memoryCache.Get("cUsuario");

            var doctores = await _service.GetDoctoresAsync();

            if (doctores == null)
            {
                return StatusCode(400, new 
                { 
                    error = errMsg
                });
            }

            return StatusCode(200, doctores);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetDoctoresByGuid(Guid guid)
        {
            string errMsg = $"{nameof(GetDoctoresByGuid)} un error se ha producido mientras se busca informaciones del doctor";

            var doctor = await _service.GetDoctorByGuid(guid);

            if (doctor == null)
            {
                return StatusCode(400, new 
                { 
                    error = errMsg
                });
            }

            return StatusCode(200, doctor);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> CreateDoctor(DoctorRegistrarDto doctorRegistrarDto)
        {
            string errMsg = $"{nameof(CreateDoctor)} un error producido mientras la creacion de un nuevo doctor";
            Doctor doctorCreated = new Doctor();

            if (ModelState.IsValid)
            {
                try
                {
                    Doctor doctor = new Doctor();
                    doctor = _mapper.Map<Doctor>(doctorRegistrarDto);

                    if( !doctorRegistrarDto.Nombre.validarNombreApellido())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "El nombre debe cumplir con el formato correcto."
                        });
                    }

                    if(!doctorRegistrarDto.Apellido.validarNombreApellido())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "El apellido debe cumplir con el formato correcto."
                        });
                    }

                    if(!doctorRegistrarDto.Posicion.validarPosicion())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "La posicion debe cumplir con el formato correcto."
                        });
                    }

                    doctorCreated = await _service.CreateUpdateDoctor(doctor, doctorRegistrarDto.UsuarioId);
                    Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorRegistrarDto.UsuarioId);
                    await _service.AsignarUsuario(usuario);
                }
                catch (Exception ex)
                {
                    return StatusCode(400, new 
                    { 
                        error = errMsg + ex
                    });
                }
            }
            return StatusCode(200, doctorCreated);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> UpdateDoctor(DoctorActualizarDto doctorActualizarDto)
        {
            string errMsg = $"{nameof(UpdateDoctor)} un error producido mientras se actualiza el doctor";
            Doctor doctorUpdated = new Doctor();

            var doctorTemp = await _service.GetDoctorByGuid(doctorActualizarDto.Guid);
            Usuario usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorTemp.Usuario.UsuarioId);
            
            if(usuario == null)
            {
                return StatusCode(400, new 
                { 
                    error = "No se ha seleccionado un usuario de manera apropiada."
                });
            }

            if(doctorTemp == null)
            {
                return StatusCode(400, new 
                { 
                    error = "Se ha intentado actualizar un doctor no registrado en el sistema."
                });
            }

            await _service.DesasignarUsuario(usuario);

            if (ModelState.IsValid)
            {
                try
                {
                    Doctor doctor = new Doctor();
                    doctor = _mapper.Map<Doctor>(doctorActualizarDto);

                    if( !doctorActualizarDto.Nombre.validarNombreApellido())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "El nombre debe cumplir con el formato correcto."
                        });
                    }

                    if(!doctorActualizarDto.Apellido.validarNombreApellido())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "El apellido debe cumplir con el formato correcto."
                        });
                    }

                    if( !doctorActualizarDto.Posicion.validarPosicion())
                    {
                        return StatusCode(400, new 
                        { 
                            error = "La posicion debe cumplir con el formato correcto."
                        });
                    }

                    doctorUpdated = await _service.CreateUpdateDoctor(doctor, doctorActualizarDto.UsuarioId);
                    usuario = await _service.GetUsuarioByGuidOrNombreUsuario(null, null, doctorActualizarDto.UsuarioId);
                    await _service.AsignarUsuario(usuario);
                }
                catch (Exception ex)
                {
                    return StatusCode(400, new 
                { 
                    error = errMsg + ex
                });
                }
            }
            return StatusCode(200, doctorUpdated);
        }

        [HttpPost("activar/{guid}")]
        public async Task<IActionResult> ActivateDoctor(Guid guid)
        {
            string errMsg = $"{nameof(ActivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctor = new Doctor();
            bool doctorActivated = false;
            try
            {
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
                return StatusCode(400, new 
                { 
                    error = errMsg + ex
                });
            }
            return StatusCode(200, new 
                { 
                    flag = doctorActivated
                });
        }

        [HttpPost("desactivar/{guid}")]
        public async Task<IActionResult> DeactivateDoctor(Guid guid)
        {
            string errMsg = $"{nameof(DeactivateDoctor)} un error se ha producido mientras se busca informaciones del doctor";

            Doctor doctor = new Doctor();
            bool doctorDeactivated = true;
            try
            {
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
                return StatusCode(400, new 
                { 
                    error = errMsg + ex
                });
            }
            return StatusCode(200, new 
                { 
                    flag = doctorDeactivated
                });
        }

    }
}