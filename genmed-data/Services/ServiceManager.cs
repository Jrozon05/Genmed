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

        public async Task<Usuario> Login(string nombreUsuario, string clave)
        {
            var usuario = await GetUsuarioByGuidOrNombreUsuario(null, nombreUsuario);

            if (usuario == null || usuario.ClaveHash == null && usuario.ClaveSalt == null)
                return null;

            if (!VerificarClaveHash(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            return usuario;
        }

        public async Task<Usuario> CreateUpdateUsuario(Usuario usuario, string clave, int doctorId, int rolId)
        {
            string errMsg = $"{nameof(CreateUpdateUsuario)} - Error en salvar o actualizar las informaciones del usuario";
            try
            {
                byte[] claveHash;
                byte[] claveSalt;

                CreateClaveHash(clave, out claveHash, out claveSalt);

                usuario.ClaveHash = claveHash;
                usuario.ClaveSalt = claveSalt;

                var usuarioCreated = await Task.Factory.StartNew(() => { return Factory.GetDatabase().CreateUpdateUsuario(usuario, clave, doctorId, rolId); });

                if (usuario == null || usuarioCreated == null)
                    return null;

                usuario = await GetUsuarioByGuidOrNombreUsuario(usuarioCreated.Guid, null);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception(errMsg, ex);
            }

            return usuario;
        }

        public async Task<Usuario> GetUsuarioByGuidOrNombreUsuario(Guid? guid = null, string nombreUsuario = null)
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuario(guid, nombreUsuario); });
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
            try {
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

        #endregion


        #region Posicion
        public async Task<List<Posicion>> GetPosiciones()
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetPosiciones(); });
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