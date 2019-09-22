using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Restriccion
    {
        public long idRestriccion { get; set; }
        public long edadMinima { get; set; }
        public long idPlataforma { get; set; }

        public Restriccion()
        {

        }

        public Restriccion(long idRestriccion, long edadMinima, long idPlataforma)
        {
            this.idRestriccion = idRestriccion;
            this.edadMinima = edadMinima;
            this.idPlataforma = idPlataforma;
        }
    }
}