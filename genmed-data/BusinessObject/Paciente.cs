using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Paciente : RecordBase<Paciente>
    {
        #region Constructor
        public Paciente() : base()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("pacienteid")]
        public int PacienteId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("genero")]
        public string Genero { get; set; }

        [JsonProperty("fechanacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonProperty("numeroexpendiente")]
        public int NumeroExpendiente { get; set; }

        [JsonProperty("numeroidentificacion")]
        public string NumeroIdentificacion { get; set; }

        [JsonProperty("numerocarnet")]
        public string NumeroCarnet { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("etnia")]
        public string Etnia { get; set; }

        [JsonProperty("profesion")]
        public string Profesion { get; set; }

        [JsonProperty("niveleducacional")]
        public string NivelEducacional { get; set; }

        [JsonProperty("religion")]
        public string Religion { get; set; }

        [JsonProperty("seguromedico")]
        public SeguroMedico SeguroMedico { get; set; }

        [JsonProperty("paisid")]
        public Pais PaisId { get; set; }

        [JsonProperty("provinciaid")]
        public Provincia ProvinciaId { get; set; }

        [JsonProperty("sectorid")]
        public Sector SectorId { get; set; }

        [JsonProperty("ciudadid")]
        public Ciudad CuidadId { get; set; }
        #endregion
    }
}
