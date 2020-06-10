using System.Collections.Generic;
using System.Threading.Tasks;
using genmed_data.Database;
using Reumed.DataAccess.BusinessObjects;

namespace genmed_data.Services
{
    public class ServiceManager : IService
    {
        public Task<List<Usuario>> GetUsuarioAsync()
        {
            return Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuarios(); });
        }
    }
}