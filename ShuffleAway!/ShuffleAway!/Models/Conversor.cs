using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShuffleAway_.Models
{
	public class Conversor
	{
		#region STRING FECHA FULL
		public string txtAFecha(string fecha)
		{
			string[] a = fecha.Split(' '); //separo el string por cada espacio de la cadena

			// guarda cada valor del array donde corresponda
			int year = Convert.ToInt32(a[2]); //índice 2 para el año
			int month = txtMesANumero(a[1]); //índice 1 para el mes y lo envío a otro metodo para que devuelva el mes
			int day = Convert.ToInt32(a[0]); //índice 0 para el día

			string[] b = a[4].Split(':'); //separo la hora donde hayan dos puntos (:)

			int hora = Convert.ToInt32(b[0]);
			int minutos = Convert.ToInt32(b[1]);
			int segundos = 0;
			return new DateTime(year, month, day, hora, minutos, segundos).ToString();
		}

		#endregion

		#region MES TEXTO A INT
		public int txtMesANumero(string txtMes)
		{
			int mes = 0;
			if (!string.IsNullOrEmpty(txtMes))
			{
				if (txtMes == "Enero")
				{
					mes = 1;
				}
				if (txtMes == "Febrero")
				{
					mes = 2;
				}
				if (txtMes == "Marzo")
				{
					mes = 3;
				}
				if (txtMes == "Abril")
				{
					mes = 4;
				}
				if (txtMes == "Mayo")
				{
					mes = 5;
				}
				if (txtMes == "Junio")
				{
					mes = 6;
				}
				if (txtMes == "Julio")
				{
					mes = 7;
				}
				if (txtMes == "Agosto")
				{
					mes = 8;
				}
				if (txtMes == "Septiembre")
				{
					mes = 9;
				}
				if (txtMes == "Octubre")
				{
					mes = 10;
				}
				if (txtMes == "Noviembre")
				{
					mes = 11;
				}
				if (txtMes == "Diciembre")
				{
					mes = 12;
				}
			}
			return mes;
		}

		#endregion
	}
}