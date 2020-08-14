using AutoMapper;
using genmed_api.Dtos.Doctor;
using genmed_api.Dtos.Usuario;
using Reumed.Data.BusinessObjects;

namespace genmed_api.Utils.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioRegistrarDto, Usuario>();
            CreateMap<UsuarioActualizarDto, Usuario>();
            CreateMap<UsuarioActualizarClaveDto, Usuario>();
            CreateMap<Usuario, UsuarioActualizarClaveDto>();
            CreateMap<DoctorRegistrarDto, Doctor>();
            CreateMap<DoctorActualizarDto, Doctor>();
        }
    }
}