using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
	public class Ganador
	{
		public long idGanador { get; set; }
		public long idUsuario { get; set; }
		public string nombreUsuario { get; set; }
		public long sorteosGanados { get; set; }
		public long rankGanadores { get; set; }
		public bool estado { get; set; }
		public DateTime fechaGanador { get;set;}

	}
}