using Dapper;
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

		public Usuario ActualizarUsuario(Usuario u) //método para registrar un nuevo usuario
		{
			Usuario usr = null;
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "UPDATE Usuarios SET " +
					"nombreUsuario = @nombreUsuario, nombre = @nombre, apellido = @apellido, " +
					"email = @email, idProvincia = @idProvincia, fechaNacimiento = @fechaNacimiento " +
					"WHERE idUsuario = @idUsuario";

				// array con datos del usuario para actualizar
				var obj = new
				{
					nombreUsuario = u.nombreUsuario,
					nombre = u.nombre,
					apellido = u.apellido,
					email = u.email,
					idProvincia = u.idProvincia,
					fechaNacimiento = u.fechaNacimiento,
					idUsuario = u.idUsuario
				};

				
				if (conect.Execute(sql, obj) > 0)
				{
					usr = u;
				}

			}

			return usr;
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

		public List<Plataforma> getListaPlataformas()
		{
			List<Plataforma> lst = new List<Plataforma>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT * FROM Plataformas";

				lst = conect.Query<Plataforma>(sql).ToList(); //se llena la lista automaticamente con todas las provincias
			}

			return lst;
		}

		public List<Entrada> getListaEntradas()
		{
			List<Entrada> lst = new List<Entrada>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT * FROM Entradas";

				lst = conect.Query<Entrada>(sql).ToList(); //se llena la lista automaticamente con todas las provincias
			}

			return lst;
		}

		public List<Sorteo> getListaSorteosFiltrado(long idUsuario)
		{
			List<Sorteo> lst = new List<Sorteo>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT * FROM Sorteos WHERE id_usuario=@idu";

				lst = conect.Query<Sorteo>(sql, new { idU = idUsuario }).ToList(); //se llena la lista automaticamente con todas las provincias
			}

			return lst;
		}


		public Usuario getLoginUsuario(Usuario u)
		{
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1 as a, pass as b FROM Usuarios WHERE email = @em";
				string sql = "SELECT idUsuario, nombreUsuario, idTipoUsuario, nombre, apellido, email, idProvincia, fechaNacimiento " +
					"FROM Usuarios WHERE email = @em";

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
							u = conect.Query<Usuario>(sql, new { em = u.email }).FirstOrDefault();
							u.strFechaNac = new Conversor().fechaATxt(u.fechaNacimiento);
							u.logueado = true;
						}
					}
				}
			}
			return u;
		}

		public int RegistrarSorteo(Sorteo sorteo) //método para crear un sorteo
		{
			int idSorteo = 0;

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{

				string sql = "INSERT INTO Sorteos (nombreSorteo, terminosCondiciones, " +
					"edadMinima, fechaInicio, fechaFin, premio, descripcionpremio, " +
					"numeroGanadores, idProvincia, idPlataforma, id_usuario) " +
					" VALUES (" +
					"@nomSor, @termCon, @edadMin, @fecIn, @fecFin, @prem, " +
					"@descPrem, @numGan, @idProv, @idPlat, @idU); SELECT LAST_INSERT_ID(); ";

				string sql2 = "INSERT INTO EntradasXSorteo (idEntrada, idSorteo, url) VALUES " +
					"(@idE, @idS, @url)";

				string sql3 = "INSERT INTO ProvinciasXSorteo (idProvincia, idSorteo) VALUES (@idP, @idS)";

				

				// preparacion de los parametros para usar el array en el primer insert
				var obj = new
				{
					nomSor = sorteo.nombreSorteo,
					termCon = sorteo.terminosCondiciones,
					edadMin = sorteo.edadMinima,
					fecIn = sorteo.fechaInicio,
					fecFin = sorteo.fechaFin,
					prem = sorteo.premio,
					descPrem = sorteo.descripcionPremio,
					numGan = sorteo.numeroGanadores,
					idProv = sorteo.idProvincia,
					idPlat = sorteo.idPlataforma,
					idU = sorteo.id_usuario
				};

				// se hace el primer insert en Sorteos y se obtiene el nuevo ID del sorteo
				idSorteo = conect.Query<int>(sql, obj).FirstOrDefault();

				// si el ID es mayor a 0, se realizó correctamente el insert en Sorteos
				if (idSorteo > 0)
				{
					for (int i = 0; i < sorteo.lstIdEntradas.Where(x => x > 0).ToList().Count; i++) //recorre la lista para aquellos items mayores a 0
					{
						for (int e = 0; e < sorteo.lstStrEntradas.Where(x => x != "").ToList().Count; e++)
						{
							if (i == e)
							{
								//hace el insert en EntradasXSorteo
								conect.Query<bool>(sql2, new { idE = sorteo.lstIdEntradas[i], idS = idSorteo, url = sorteo.lstStrEntradas[e] }).FirstOrDefault();
							}
							
						}
						
					}

					foreach (var o in sorteo.lstIdProvincias.Where(x => x > 0))
					{
						//hace el insert en ProvinciasXSorteo
						conect.Query<bool>(sql3, new { idP = o, idS = idSorteo }).FirstOrDefault();
					}
				}

			}

			return idSorteo;
		}
	}
}