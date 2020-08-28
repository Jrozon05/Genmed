using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using genmed_data.Database;
using Reumed.Data.BusinessObjects;
using genmed_data.BusinessObject;

namespace genmed_data.Services
{
    public class ServiceManager : IService
    {
        #region Usuario
        public async Task<List<Usuario>> GetUsuarioAsync()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuarios(); });
        }

        public async Task<List<Usuario>> GetUsuariosNoAsignado()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuariosNoAsignado(); });
        }

        public async Task<Usuario> Login(string nombreUsuario, string clave)
        {
            var usuario = await GetUsuarioByGuidOrNombreUsuario(null, nombreUsuario, null, null);

            if (usuario == null || usuario.ClaveHash == null && usuario.ClaveSalt == null)
                return null;

            if (!VerificarClaveHash(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            return usuario;
        }

        public async Task<bool> UpdateClaveUsuario(Usuario usuario, string clave)
        {
            bool updated = false;
            string errMsg = $"{nameof(UpdateClaveUsuario)} - Error actualizando la clave del usuario";
            try
            {
                byte[] claveHash;
                byte[] claveSalt;

                CreateClaveHash(clave, out claveHash, out claveSalt);

                usuario.ClaveHash = claveHash;
                usuario.ClaveSalt = claveSalt;
                var usuarioCreated = await Task.Factory.StartNew(() => { return Factory.GetDatabase().UpdateClaveUsuario(usuario, clave); });

                if (usuario == null || usuarioCreated == null)
                    return updated;

                usuario = await GetUsuarioByGuidOrNombreUsuario(usuarioCreated.Guid, null, null, null);
                if (usuario != null)
                    updated = true;
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception(errMsg, ex);
            }

            return updated;
        }

        public async Task<Usuario> CreateUpdateUsuario(Usuario usuario, int rolId)
        {
            string errMsg = $"{nameof(CreateUpdateUsuario)} - Error en salvar o actualizar las informaciones del usuario";
            try
            {

                var usuarioCreated = await Task.Factory.StartNew(() => { return Factory.GetDatabase().CreateUpdateUsuario(usuario, rolId); });

                if (usuario == null || usuarioCreated == null)
                    return null;

                usuario = await GetUsuarioByGuidOrNombreUsuario(usuarioCreated.Guid, null, null, null);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception(errMsg, ex);
            }

            return usuario;
        }

        public async Task<Usuario> GetUsuarioByGuidOrNombreUsuario(Guid? guid = null, string nombreUsuario = null, int? usuarioId = null, string email = null)
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuario(guid, nombreUsuario, usuarioId, email); });
        }

        public async Task<bool> ActivateUsuario(Usuario usuario)
        {
            usuario.Activo = true;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateUsuario(usuario, usuario.Rol.RolId));
            return usuario.Activo;
        }

        public async Task<bool> DeactivateUsuario(Usuario usuario)
        {
            usuario.Activo = false;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateUsuario(usuario, usuario.Rol.RolId));
            return usuario.Activo;
        }

        public async Task<bool> AsignarUsuario(Usuario usuario)
        {
            usuario.Asignado = true;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateUsuario(usuario, usuario.Rol.RolId));
            return usuario.Asignado;
        }

        public async Task<bool> DesasignarUsuario(Usuario usuario)
        {
            usuario.Asignado = false;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateUsuario(usuario, usuario.Rol.RolId));
            return usuario.Asignado;
        }

        #endregion

        #region Doctor

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetDoctores(); });
        }

        public async Task<Doctor> GetDoctorByGuid(Guid? guid = null)
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetDoctor(guid); });
        }

        public async Task<Doctor> CreateUpdateDoctor(Doctor doctor, int usuarioId)
        {
            string errMsg = $"{nameof(CreateUpdateDoctor)} - Error en salvar o actualizar las informaciones del doctor";
            try
            {
                var doctorCreated = await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateDoctor(doctor, usuarioId));

                if (doctor == null || doctorCreated == null)
                    return null;

                doctor = await GetDoctorByGuid(doctorCreated.Guid);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception(errMsg, ex);
            }
            return doctor;
        }

        public async Task<bool> ActivateDoctor(Doctor doctor)
        {
            doctor.Activo = true;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateDoctor(doctor, doctor.Usuario.UsuarioId));
            return doctor.Activo;
        }

        public async Task<bool> DeactivateDoctor(Doctor doctor)
        {
            doctor.Activo = false;
            await Task.Factory.StartNew(() => Factory.GetDatabase().CreateUpdateDoctor(doctor, doctor.Usuario.UsuarioId));
            return doctor.Activo;
        }

        #endregion

        #region Direccion

        public async Task<List<Provincia>> GetProvincias()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetProvincias(); });
        }

        public async Task<List<Ciudad>> GetCiudadesByProvincia(int provinciaId)
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetCiudades(provinciaId); });
        }

        public async Task<List<Sector>> GetSectoresByCiudad()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetSectores(); });
        }
        #endregion

        #region Extensiones
        private void CreateClaveHash(string clave, out byte[] claveHash, out byte[] claveSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                claveSalt = hmac.Key;
                claveHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        private bool VerificarClaveHash(string clave, byte[] claveHash, byte[] claveSalt)
        {
            using (var hmac = new HMACSHA512(claveSalt))
            {
                var hashCreado = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));

                for (int i = 0; i < hashCreado.Length; i++)
                {
                    if (hashCreado[i] != claveHash[i])
                        return false;
                }
            }

            return true;
        }
        #endregion
    }
}