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
            CreateMap<UsuarioActualizarDto, Doctor>();
            CreateMap<DoctorRegistrarDto, Doctor>();
            CreateMap<DoctorActualizarDto, Doctor>();
        }
    }
}