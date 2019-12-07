using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
	public class Notificacion
	{
		public long id { get; set; }
		public long idNotificacionXUsuario { get; set; }
		public string detalle { get; set; }
		public long idUsuario { get; set; }
		public long idNotificacion { get; set; }
		public DateTime fechaNotificacion { get; set; }
		public bool estado { get; set; }
	}
}