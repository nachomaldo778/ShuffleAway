using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Entrada
    {
        public long idEntrada { get; set; }
        public string tipoEntrada { get; set; }
        public long idPlataforma { get; set; }

        public Entrada()
        {

        }

        public Entrada(long idEntrada, string tipoEntrada, long idPlataforma)
        {
            this.idEntrada = idEntrada;
            this.tipoEntrada = tipoEntrada;
            this.idPlataforma = idPlataforma;
        }
    }
}