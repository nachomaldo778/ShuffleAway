using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Usuario
    {
        public long idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string pass { get; set; }
        public long idTipoUsuario { get; set; }
        public string email { get; set; }
        public long idPais { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }

        public Usuario()
        {

        }

        public Usuario(long idUsuario, string nombreUsuario, string pass, long idTipoUsuario, string email, long pais, string nombre, string apellido, DateTime fechaNacimiento)
        {
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.pass = pass;
            this.idTipoUsuario = idTipoUsuario;
            this.email = email;
            this.idPais = pais;
            this.nombre = nombre;
            this.apellido = apellido;
            this.fechaNacimiento = fechaNacimiento;
        }
    }
}