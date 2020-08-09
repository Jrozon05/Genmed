using Newtonsoft.Json;
using Reumed.Data.BusinessObjects;

//Remover Entidad
namespace genmed_data.BusinessObject
{
    public class Posicion : RecordBase<Posicion>
    {
        [JsonProperty("posicionid")]
        public int PosicionId { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("nombrecorto")]
        public string NombreCorto { get; set; }
    }
}