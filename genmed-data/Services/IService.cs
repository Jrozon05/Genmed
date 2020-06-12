using System.Collections.Generic;
using System.Threading.Tasks;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Services
{
    public interface IService
    {
        #region Usuario

        Task<List<Usuario>> GetUsuarioAsync();

        Task<Usuario> Login(string nombreUsuario, string clave);

        Task<Usuario> CreateUpdateUsuario(Usuario usuario, string clave);

        #endregion
    }
}