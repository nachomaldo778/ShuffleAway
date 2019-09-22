using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Plataforma
    {
        public long idPlataforma { get; set; }
        public string nombrePlataforma { get; set; }

        public Plataforma()
        {

        }

        public Plataforma(long idPlataforma, string nombrePlataforma)
        {
            this.idPlataforma = idPlataforma;
            this.nombrePlataforma = nombrePlataforma;
        }
    }
}