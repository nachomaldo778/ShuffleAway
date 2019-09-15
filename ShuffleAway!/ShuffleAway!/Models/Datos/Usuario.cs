using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models.Datos
{
	public class Usuario
	{
		public long Id { get; set; }
		public string userName { get; set; }
		public string password { get; set; }
		public string email { get; set; }
	}
}