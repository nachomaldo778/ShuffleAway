using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Sorteo
    {
        public long idSorteo { get; set; }
        public string nombreSorteo { get; set; }
        public string terminosCondiciones { get; set; }
        public long edadMinima { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string premio { get; set; }
        public string descripcionPremio { get; set; }
        public long numeroGanadores { get; set; }
        public long idProvincia { get; set; }
        public long idPlataforma { get; set; }	
		public List<long> lstIdEntradas { get; set; }
		public List<long> lstIdProvincias { get; set; }
    }
}