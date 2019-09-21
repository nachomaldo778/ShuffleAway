using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
    public class TiposDeUsuarios
    {
        public long idTipoUsuario { get; set; }
        public string tipoUsuario { get; set; }

        public TiposDeUsuarios()
        {

        }

        public TiposDeUsuarios(long idTipoUsuarios, string tipoUsuario)
        {
            this.idTipoUsuario = idTipoUsuario;
            this.tipoUsuario = tipoUsuario;
        }
    }
}