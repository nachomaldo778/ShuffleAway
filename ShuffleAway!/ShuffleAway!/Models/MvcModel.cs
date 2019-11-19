using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
	public class MvcModel
	{
		public string[] str { get; set; }
		public Usuario usuario { get; set; }
		public Entrada entrada { get; set; }
		public Sorteo sorteo { get; set; }
		public Inscripciones inscripcion { get; set; }
		public Plataforma plataforma { get; set; }
		public List<Provincia> lstProvincias { get; set; }
		public List<TiposDeUsuarios> lstTipoUsuario { get; set; }
		public List<string> lstErrores { get; set; }
		public List<Plataforma> lstPlataformas { get; set; }
		public List<Entrada> lstEntradas { get; set; }
		public List<Sorteo> lstSorteos { get; set; }
		public List<Inscripciones> lstInscripciones { get; set; }
		public List<Ganador> lstGanadores { get; set; }
		
	}
}