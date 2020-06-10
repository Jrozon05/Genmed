using System.Collections.Generic;
using System.Threading.Tasks;
using Reumed.DataAccess.BusinessObjects;

namespace genmed_data.Services
{
    public interface IService
    {
        #region Usuario

        Task<List<Usuario>> GetUsuarioAsync();

        #endregion
    }
}