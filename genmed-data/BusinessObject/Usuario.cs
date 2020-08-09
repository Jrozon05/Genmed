using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.Data.BusinessObjects
{
    public class Usuario : RecordBase<Usuario>
    {
        #region Constructor
        public Usuario() : base()
        {
            base.SetInitialState();
        }

        #endregion

        #region Properties

        [JsonProperty("usuarioid")]
        public int UsuarioId { get; set; }

        [JsonProperty("guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("nombreusuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("clave")]
        public string Clave { get; set; }

        [JsonProperty("clavehash")]
        public byte[] ClaveHash { get; set; }

        [JsonProperty("clavesalt")]
        public byte[] ClaveSalt { get; set; }
<<<<<<< HEAD
=======
        
        //TODO: Remover campo
        [JsonProperty("doctor")]
        public List<Doctor> Doctor { get; set; }
>>>>>>> 663bd06380540a50d117b0e18bf1ece72c906539

        [JsonProperty("rol")]
        public Rol Rol { get; set; }

        [JsonProperty("utlimaactividad")]
        public DateTime UltimaActividad { get; set; }

        #endregion
    }
}
