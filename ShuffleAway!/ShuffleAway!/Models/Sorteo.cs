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
		public DateTime fechaInicio { get; set; }
		[Required(ErrorMessage = "Debe ingresar una fecha de fin.")]
		public DateTime fechaFin { get; set; }
		[Required(ErrorMessage = "Debe ingresar un premio.")]
		public string premio { get; set; }
		[Required(ErrorMessage = "Debe ingresar el detalle del premio.")]
		public string descripcionPremio { get; set; }
		[Required(ErrorMessage = "Debe ingresar el número de ganadores.")]
		public long numeroGanadores { get; set; }
		public string strFechaIn { get; set; }
		public string strFechaFn { get; set; }
		public long id_usuario { get; set; }

        public long idProvincia { get; set; }
        public long idPlataforma { get; set; }	
		public List<Entrada> lstEntradas { get; set; }
		public List<long> lstIdProvincias { get; set; }
		public List<long> lstIdEntradas { get; set; }
		public List<string> lstStrEntradas { get; set; }
		public int estado { get; set; }

		public string DevolverEstado()
		{
			return Enum.GetName(typeof(EstadosEnum), estado).Replace("_", " ");
		}
    }
}