using System.Collections.Generic;
using Reumed.DataAccess.BusinessObjects;

namespace genmed_data.Database
{
    public interface IDatabase
    {
        #region Usuario

        List<Usuario> GetUsuarios();

        #endregion
    }
}