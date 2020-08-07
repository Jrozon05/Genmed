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

        Task<Usuario> GetUsuarioByGuidOrNombreUsuario(Guid? guid = null, string nombreUsuario = null);

        Task<List<Usuario>> GetUsuarioAsync();

        Task<Usuario> Login(string nombreUsuario, string clave);

        Task<Usuario> CreateUpdateUsuario(Usuario usuario, string clave, int doctorId, int rolId);

        #endregion

        #region Doctor

        Task<List<Doctor>> GetDoctoresAsync();

        Task<Doctor> GetDoctorByGuid(Guid? guid = null);

        Task<Doctor> CreateUpdateDoctor(Doctor doctor, int usuarioId);

        #endregion

        #region Posicion
        Task<List<Posicion>> GetPosiciones();
        #endregion

        #region Direccion
        Task<List<Provincia>> GetProvincias();

        Task<List<Ciudad>> GetCiudadesByProvincia(int provinciaId);

        Task<List<Sector>> GetSectoresByCiudad();
        #endregion
    }
}