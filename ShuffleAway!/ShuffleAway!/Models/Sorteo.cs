using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Sorteo
    {
        public long idSorteo { get; set; }
		[Required(ErrorMessage = "Debe ingresar un nombre de sorteo.")]
		public string nombreSorteo { get; set; }
		[Required(ErrorMessage = "Debe ingresar los términos y condiciones.")]
		public string terminosCondiciones { get; set; }
		[Required(ErrorMessage = "Debe ingresar una edad mínima.")]
		public long edadMinima { get; set; }
		[Required(ErrorMessage = "Debe ingresar una fecha de inicio.")]
		public string fechaInicio { get; set; }
		[Required(ErrorMessage = "Debe ingresar una fecha de fin.")]
		public string fechaFin { get; set; }
		[Required(ErrorMessage = "Debe ingresar un premio.")]
		public string premio { get; set; }
		[Required(ErrorMessage = "Debe ingresar el detalle del premio.")]
		public string descripcionPremio { get; set; }
		[Required(ErrorMessage = "Debe ingresar el número de ganadores.")]
		public long numeroGanadores { get; set; }

        public long idProvincia { get; set; }
        public long idPlataforma { get; set; }	
		public List<long> lstIdEntradas { get; set; }
		public List<long> lstIdProvincias { get; set; }
    }
}