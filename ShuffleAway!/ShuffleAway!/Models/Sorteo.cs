using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class Sorteo
    {
        public long idSorteo { get; set; }
        public string nombreSorteo { get; set; }
        public string terminosCondiciones { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public long idEntrada { get; set; }
        public string premio { get; set; }
        public long idRestriccion { get; set; }
        public long idProvincia { get; set; }

        public Sorteo()
        {

        }

        public Sorteo(long idSorteo, string nombreSorteo, string terminosCondiciones, DateTime fechaIncio, DateTime fechaFin, long idEntrada, string premio, long idRestriccion, long idProvincia)
        {
            this.idSorteo = idSorteo;
            this.nombreSorteo = nombreSorteo;
            this.terminosCondiciones = terminosCondiciones;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.idEntrada = idEntrada;
            this.premio = premio;
            this.idRestriccion = idRestriccion;
            this.idProvincia = idProvincia;
        }
    }
}