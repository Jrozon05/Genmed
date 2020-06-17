using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    public interface IDatabase
    {
        #region Usuario
        Usuario Login(string nombreUsuario, string clave);

        Usuario GetUsuario(Guid? guid = null, string nombreUsuario = null);

        List<Usuario> GetUsuarios();

        Usuario CreateUpdateUsuario(Usuario usuario, string clave);

        #endregion
    }
}