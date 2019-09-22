using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Provincia
    {
        public long idProvincia { get; set; }
        public string nombreProvincia { get; set }

        public Provincia()
        {

        }

        public Provincia(long idProvincia, string nombreProvincia)
        {
            this.idProvincia = idProvincia;
            this.nombreProvincia = nombreProvincia;
        }
    }
}