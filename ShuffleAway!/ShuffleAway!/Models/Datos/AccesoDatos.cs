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
		public bool setRegistroUsuario(Usuario u)
		{
			bool cargado = false;
			Usuario usr = new Usuario
			{
				userName = "prueba",
				password = "claveprueba"
			};

			if (u.userName != usr.userName)
			{
				cargado = true;
			}

			return cargado;
		}


		public int RegistrarUsuario(Usuario u) //método para registrar un nuevo usuario
		{
			bool cargado = false;
			int lastId = 0, lastUsrId = 0;

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1, contrasenia FROM usuarios WHERE nom_usr = @nom_u OR correo = @correo";
				

				string sql2 = "INSERT INTO usuarios(nom_usr,correo,contrasenia, id_maestra) " +
							"VALUES (@nom_u,@correo,@contra, @idM); SELECT LAST_INSERT_ID()";

				//var row = (IDictionary<string, object>)conect.Query(verificar, new { nom_u = u.NomUsr, correo = u.Correo }).FirstOrDefault();

				if (conect.Query<int>(verificar, new { nom_u = u.userName, correo = u.email }).FirstOrDefault() < 1)
				{
					//se guarda el ultimo Id insertado en la tabla Usuarios, en la variable LastId
					lastUsrId = conect.Query<int>(sql2, new { nom_u = u.userName, correo = u.email, contra = u.password, idM = lastId }).FirstOrDefault();
					
				}

			}

			return lastUsrId;
		}
	}
}