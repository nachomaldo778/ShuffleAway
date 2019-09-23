﻿using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Helpers;

namespace ShuffleAway_.Models.Datos
{
	public class AccesoDatos
	{
		public long RegistrarUsuario(Usuario u) //método para registrar un nuevo usuario
		{
			//bool cargado = false;
			//int lastId = 0
			long lastUsrId = 0;

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1 FROM Usuarios WHERE email = @email";

				//string sql2 = "INSERT INTO Usuarios(nombreUsuario,pass,idTipoUsuario, nombre, apellido, email, pais, fechaNacimiento) " +
				//			"VALUES (@nomU,@pass,@idTusr, @nom, @ape, @em, @idPa, @fecNac); SELECT LAST_INSERT_ID()";

				string sql2 = "INSERT INTO Usuarios(pass,idTipoUsuario, email, idProvincia) " +
							"VALUES (@pass,@idTusr, @em, @idProv); SELECT LAST_INSERT_ID()";

				//var row = (IDictionary<string, object>)conect.Query(verificar, new { nom_u = u.NomUsr, correo = u.Correo }).FirstOrDefault();

				if (!conect.Query<bool>(verificar, new { email = u.email }).FirstOrDefault())
				{
					//se guarda el ultimo Id insertado en la tabla Usuarios, en la variable LastId para hacer una verificacion
					// se inserta siempre 
					lastUsrId = conect.Query<int>(sql2, new
					{
						pass = u.pass,
						idTusr = 1,
						em = u.email,
						idProv = u.idProvincia
					}).FirstOrDefault();

				}

			}

			return lastUsrId;
		}

		public List<Provincia> getListaProvincias()
		{
			List<Provincia> lst = new List<Provincia>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT * FROM Provincias";

				lst = conect.Query<Provincia>(sql).ToList(); //se llena la lista automaticamente con todas las provincias
			}

			return lst;
		}

		public Usuario getLoginUsuario(Usuario u)
		{
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1 as a, pass as b FROM Usuarios WHERE email = @em";

				//obtengo la contraseña y el valor 1 para verificar que exista el usuario
				var row = (IDictionary<string, object>)conect.Query(verificar, new { em = u.email }).FirstOrDefault();

				//chequeo que el objeto tenga datos
				if (row != null)
				{
					//verifico que me haya devuelto el valor 1
					if (Convert.ToBoolean(row["a"]))
					{
						//guardo la contraseña encriptada de la base de datos
						var passHashed = row["b"].ToString();

						//comparo la pass de la bd con la ingresada por el usuario
						if (Crypto.VerifyHashedPassword(passHashed, u.pass))
						{
							u.logueado = true;
						}
					}
				}
			}
			return u;
		}
	}
}