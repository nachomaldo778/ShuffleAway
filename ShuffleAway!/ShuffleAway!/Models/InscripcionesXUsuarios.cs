using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class InscripcionesXUsuarios
    {
        public long idInscripcionUsuario { get; set; }
        public long idInscripcion { get; set; }
        public long idUsuario { get; set; }
        public DateTime fechaInscripcion { get; set; }

        public InscripcionesXUsuarios()
        {

        }

        public InscripcionesXUsuarios(long idInscripcionUsuario, long idInscripcion, long idUsuario, DateTime fechaInscripcion)
        {
            this.idInscripcionUsuario = idInscripcionUsuario;
            this.idInscripcion = idInscripcion;
            this.idUsuario = idUsuario;
        }
    }
}