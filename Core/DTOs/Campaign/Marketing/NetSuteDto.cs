using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.DTOs.Campaign.Marketing
{
    public class NetSuteUserResponseDto
    {
        [JsonProperty("ID de campaña")]
        public string IDCampania { get; set; }

        [JsonProperty("Fecha de creación")]
        public string FechaCreacion { get; set; }

        [JsonProperty("Descripción")]
        public string Descripcion { get; set; }

        [JsonProperty("Fecha de finalización")]
        public string FechaFinalizacion { get; set; }

        [JsonProperty("Evento")]
        public string Evento { get; set; }

        [JsonProperty("Ejecutado el")]
        public string EjecutadoEl { get; set; }

        [JsonProperty("Promoción")]
        public string Promocion { get; set; }

        [JsonProperty("Programado el")]
        public string ProgramadoEl { get; set; }

        [JsonProperty("Fecha de inicio")]
        public string FechaInicio { get; set; }

        [JsonProperty("Estado")]
        public string Estado { get; set; }

        [JsonProperty("Título")]
        public string Titulo { get; set; }

        [JsonProperty("Suscripción")]
        public string Suscripcion { get; set; }

        [JsonProperty("WappTemplateId")]
        public string WappTemplateId { get; set; }

        [JsonProperty("TimeToExecute")]
        public string TimeToExecute { get; set; }

        [JsonProperty("ContactName")]
        public string ContactName { get; set; }

        [JsonProperty("ContactId")]
        public string ContactId { get; set; }

        [JsonProperty("ContactPhone")]
        public string ContactPhone { get; set; }

        [JsonProperty("WappTemplateMessage")]
        public string WappTemplateMessage { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { 
            get {
                string formato = "d/M/yyyy h:mm tt";
                DateTime fecha = DateTime.ParseExact(FechaCreacion, formato, CultureInfo.InvariantCulture);
                return fecha;
            } 
        }

        [JsonIgnore]
        public DateTime TimeExecute
        {
            get
            {
                string formato = "d/M/yyyy h:mm tt";
                DateTime fecha = DateTime.ParseExact(TimeToExecute, formato, CultureInfo.InvariantCulture);
                return fecha;
            }
        }

        [JsonIgnore]
        public String HourForSending
        {
            get
            {
                string formato = "d/M/yyyy h:mm:ss tt";
                DateTime fecha = DateTime.ParseExact(TimeToExecute, formato, CultureInfo.InvariantCulture);
                return $"{fecha.Hour}:{fecha.Minute}";
            }
        }
    }

    public class NetSuteBodyResponseDto
    {
        [JsonProperty("body")]
        public List<List<NetSuteUserResponseDto>> body { get; set; }

        [JsonIgnore]
        public int AmountUsers { get { return body.First().Count; } }

        [JsonIgnore]
        public List<NetSuteUserResponseDto> Users { get { return body.First(); } }
    }

    public class NetSuiteRequestDto {
        [JsonProperty("action")]
        public string action { get; set; }

        [JsonProperty("searchId")]
        public string searchId { get; set; }

        [JsonProperty("start")]
        public int start { get; set; }

        [JsonProperty("end")]
        public int end { get; set; }
        
    }
}
