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
        public long edadMinima { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public long idEntrada { get; set; }
        public string premio { get; set; }
        public string descripcionPremio { get; set; }
        public long numeroGanadores { get; set; }
        public long idProvincia { get; set; }
        public long idPlataforma { get; set; }

        public Sorteo()
        {

        }

        public Sorteo(long idSorteo, string nombreSorteo, string terminosCondiciones, long edadMinima, DateTime fechaInicio, DateTime fechaFin, long idEntrada, string premio, string descripcionPremio, long numeroGanadores, long idProvincia, long idPlataforma)
        {
            this.idSorteo = idSorteo;
            this.nombreSorteo = nombreSorteo;
            this.terminosCondiciones = terminosCondiciones;
            this.edadMinima = edadMinima;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.idEntrada = idEntrada;
            this.premio = premio;
            this.descripcionPremio = descripcionPremio;
            this.numeroGanadores = numeroGanadores;
            this.idProvincia = idProvincia;
            this.idPlataforma = idPlataforma;
        }
    }
}