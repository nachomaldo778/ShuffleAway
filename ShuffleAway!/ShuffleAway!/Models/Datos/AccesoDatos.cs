using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Dapper;
using MySql.Data.MySqlClient;

namespace ShuffleAway_.Models.Datos
{
	public class AccesoDatos
	{
		// comentar para usar datos de la base
		//public bool setRegistroUsuario(Usuario u)
		//{
		//	bool cargado = false;
		//	Usuario usr = new Usuario
		//	{
		//		userName = "prueba",
		//		password = "claveprueba"
		//	};

		//	if (u.userName != usr.userName)
		//	{
		//		cargado = true;
		//	}

		//	return cargado;
		//}


		public int RegistrarUsuario(Usuario u) //método para registrar un nuevo usuario
		{
			bool cargado = false;
			int lastId = 0, lastUsrId = 0;

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1 FROM Usuarios WHERE email = @email";
				
				//string sql2 = "INSERT INTO Usuarios(nombreUsuario,pass,idTipoUsuario, nombre, apellido, email, pais, fechaNacimiento) " +
				//			"VALUES (@nomU,@pass,@idTusr, @nom, @ape, @em, @idPa, @fecNac); SELECT LAST_INSERT_ID()";

				string sql2 = "INSERT INTO Usuarios(pass,idTipoUsuario, email, pais) " +
							"VALUES (@pass,@idTusr, @em, @idPa); SELECT LAST_INSERT_ID()";

				//var row = (IDictionary<string, object>)conect.Query(verificar, new { nom_u = u.NomUsr, correo = u.Correo }).FirstOrDefault();

				if (!conect.Query<bool>(verificar, new { email = u.email }).FirstOrDefault())
				{
					//se guarda el ultimo Id insertado en la tabla Usuarios, en la variable LastId para hacer una verificacion
					// se inserta siempre 
					lastUsrId = conect.Query<int>(sql2, new {
						pass = u.pass, idTusr = 1, em = u.email, idPa = u.idPais
					}).FirstOrDefault();
					
				}

			}

			return lastUsrId;
		}
	}
}