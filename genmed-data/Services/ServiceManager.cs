using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using genmed_data.Database;
using Reumed.Data.BusinessObjects;

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

            if (usuario == null || !VerificarClaveHash(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            return usuario;
        }

        public async Task<Usuario> CreateUpdateUsuario(Usuario usuario, string clave)
        {
            string errMsg = $"{nameof(CreateUpdateUsuario)} - Error en salvar o actualizar las informaciones del usuario";
            try
            {
                byte[] claveHash;
                byte[] claveSalt;

                CreateClaveHash(clave, out claveHash, out claveSalt);

                usuario.ClaveHash = claveHash;
                usuario.ClaveSalt = claveSalt;

                usuario = Factory.GetDatabase().CreateUpdateUsuario(usuario, clave);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception(errMsg, ex);
            }

            return await Task.Factory.StartNew(() => { return usuario; });
        }

        public async Task<Usuario> GetUsuarioByGuidOrNombreUsuario(Guid? guid = null, string nombreUsuario = null)
        {
            return await Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuario(guid, nombreUsuario); });
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