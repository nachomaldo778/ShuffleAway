using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
	public class MvcModel
	{
		public Usuario usuario { get; set; }
		public Entrada entrada { get; set; }
		public Inscripciones inscripcion { get; set; }
		public Plataforma plataforma { get; set; }
		public List<Provincia> lstProvincias { get; set; }
		public Restriccion restriccion { get; set; }
		public List<TiposDeUsuarios> lstTipoUsuario { get; set; }
		public List<string> ListErrores { get; set; }
	}
}