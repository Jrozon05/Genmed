using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using genmed_data.BusinessObject;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Services
{
    public interface IService
    {
        #region Usuario

        Task<Usuario> GetUsuarioByGuid(Guid guid);

        Task<Usuario> GetUsuarioByNombreUsuario(string nombreUsuario);

        Task<Usuario> GetUsuarioByEmail(string email);

        Task<Usuario> GetUsuarioByUsuarioId(int usuarioId);

        Task<List<Usuario>> GetUsuarioAsync();

        Task<List<Usuario>> GetUsuariosNoAsignado();

        Task<Usuario> Login(string nombreUsuario, string clave);

        Task<Usuario> CreateUpdateUsuario(Usuario usuario, int rolId);

        Task<bool> UpdateClaveUsuario(Usuario usuario, string clave);

        Task<bool> ActivateUsuario(Usuario usuario);

        Task<bool> DeactivateUsuario(Usuario usuario);

        Task<bool> AsignarUsuario(Usuario usuario);

        Task<bool> DesasignarUsuario(Usuario usuario);

        #endregion

        #region Doctor

        Task<List<Doctor>> GetDoctoresAsync();

        Task<Doctor> GetDoctorByGuid(Guid? guid = null);

        Task<Doctor> CreateUpdateDoctor(Doctor doctor, int usuarioId);

        Task<bool> ActivateDoctor(Doctor doctor);

        Task<bool> DeactivateDoctor(Doctor doctor);

        #endregion

        #region Direccion
        Task<List<Provincia>> GetProvincias();

        Task<List<Ciudad>> GetCiudadesByProvincia(int provinciaId);

        Task<List<Sector>> GetSectoresByCiudad();
        #endregion
    }
}