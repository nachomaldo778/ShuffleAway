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
        public long idEntrada { get; set; }

        public Inscripciones()
        {

        }

        public Inscripciones(long idInscripcion, long idSorteo, long idEntrada)
        {
            this.idInscripcion = idInscripcion;
            this.idSorteo = idSorteo;
            this.idEntrada = idEntrada;
        }
    }
}