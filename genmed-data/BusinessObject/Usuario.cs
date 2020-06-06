using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
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
        public Guid Guid { get; set; }

        [JsonProperty("nombreusuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("clave")]
        public string Clave { get; set; }

        [JsonProperty("clavehash")]
        public byte[] ClaveHash { get; set; }

        [JsonProperty("clavesalt")]
        public byte[] ClaveSalt { get; set; }

        [JsonProperty("doctor")]
        public List<Doctor> Doctor { get; set; }

        public List<Rol> Rol { get; set; }

        [JsonProperty("utlimaactividad")]
        public DateTime UltimaActividad { get; set; }

        #endregion
    }
}
