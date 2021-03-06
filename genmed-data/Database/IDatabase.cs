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
        Usuario GetUsuarioByGuid(Guid guid);

        Usuario GetUsuarioByNombreUsuario(string nombreUsuario);

        Usuario GetUsuarioByEmail(string email);

        Usuario GetUsuarioByUsuarioId(int usuarioId);

        List<Usuario> GetUsuarios();

        List<Usuario> GetUsuariosNoAsignado();

        Usuario CreateUpdateUsuario(Usuario usuario, int rolId);

        Usuario UpdateClaveUsuario(Usuario usuario, string clave);

        #endregion

        #region Doctor

        List<Doctor> GetDoctores();
        
        Doctor GetDoctor(Guid? guid = null);

        Doctor CreateUpdateDoctor(Doctor doctor, int usuarioId);

        #endregion

        #region Direccion
        List<Ciudad> GetCiudades(int provinciaId);

        List<Provincia> GetProvincias();

        List<Sector> GetSectores();
        #endregion
    }
}