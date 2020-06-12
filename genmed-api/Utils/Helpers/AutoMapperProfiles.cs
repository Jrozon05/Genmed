using AutoMapper;
using genmed_api.Dtos.Usuario;
using Reumed.Data.BusinessObjects;

namespace genmed_api.Utils.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioRegistrarDto, Usuario>();
        }
    }
}