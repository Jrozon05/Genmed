using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using genmed_data.BusinessObject;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    public interface IDatabase
    {
        #region Usuario
        Usuario GetUsuario(Guid? guid = null, string nombreUsuario = null);

        List<Usuario> GetUsuarios();

        Usuario CreateUpdateUsuario(Usuario usuario, string clave, int doctorId, int rolId);

        #endregion

        #region Doctor

        List<Doctor> GetDoctores();

        #endregion

        #region Posicion
        List<Posicion> GetPosiciones();
        #endregion

        #region Direccion
        List<Ciudad> GetCiudades(int provinciaId);

        List<Provincia> GetProvincias();

        List<Sector> GetSectores();
        #endregion
    }
}