using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Usuario
    {
        public long idUsuario { get; set; }
        public string nombreUsuario { get; set; }
		[Required(ErrorMessage = "Debe ingresar una contraseña.")]
		[DataType(DataType.Password)]
		public string pass { get; set; }
		[DataType(DataType.Password)]
		[Compare("pass", ErrorMessage = "Las contraseñas no coinciden. Intente nuevamente.")]
		public string rePass { get; set; }
		[Required(ErrorMessage = "Ingrese un email válido.")]
		[DataType(DataType.EmailAddress)]
        public long idTipoUsuario { get; set; }
		public string email { get; set; }
        public long idProvincia { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
		public bool logueado { get; set; }
		public string strFechaNac { get; set; }

    }
}