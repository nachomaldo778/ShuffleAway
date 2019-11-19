using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Inscripciones
    {
        public long idInscripcion { get; set; }
        public long idSorteo { get; set; }
        public long idUsuario { get; set; }
		public DateTime fechaInscripcion { get; set; }
		public DateTime fechaFin { get; set; }
		public string nombreSorteo { get; set; }
		public long cantidadInscripciones { get; set; }
		public string nombreUsuario { get; set; }


	}
}