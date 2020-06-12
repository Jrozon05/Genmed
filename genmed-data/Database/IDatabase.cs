using System.Collections.Generic;
using System.Threading.Tasks;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    public interface IDatabase
    {
        #region Usuario
        Usuario Login(string nombreUsuario, string clave);

        List<Usuario> GetUsuarios();

        Usuario CreateUpdateUsuario(Usuario usuario, string clave);

        #endregion
    }
}