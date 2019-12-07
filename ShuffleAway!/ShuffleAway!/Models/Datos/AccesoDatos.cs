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
			long lastUsrId = 0;

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string verificar = "SELECT 1 FROM Usuarios WHERE email = @email";

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


				conect.Execute(sql, obj);

			}

			return u;
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

		public List<Sorteo> getListaSorteosActivos(long idUsuario, EstadosEnum estado)
		{
			List<Sorteo> lst = new List<Sorteo>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT * FROM Sorteos s where ";
				string sql2 = "SELECT e.idEntrada, e.tipoEntrada, es.url FROM Entradas e, EntradasXSorteo es WHERE e.idEntrada=es.idEntrada AND es.idSorteo = @idS";
				if (idUsuario > 0)
				{
					sql += "id_usuario=@idu AND ";
				}
				if ((int)estado == 1)
				{
					sql += "s.estado = 1";
				}
				else
				{
					sql += "s.estado <> 1";
				}


				lst = conect.Query<Sorteo>(sql, new { idU = idUsuario }).ToList(); //se llena la lista automaticamente con todos los sorteos

				foreach (var s in lst)
				{
					s.lstEntradas = conect.Query<Entrada>(sql2, new { idS = s.idSorteo }).ToList();
				}
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

				string sqlNotificaciones = "SELECT n.id 'id', n.detalle 'detalle', un.fechaNotificacion 'fechaNotificacion', " +
					"un.estado 'estado' FROM Notificaciones n, UsuariosXnotificacion un WHERE n.id = un.idNotificacion " +
					"AND un.idUsuario = @idU ";

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
							u.lstNotificaciones = conect.Query<Notificacion>(sqlNotificaciones, new { idU = u.idUsuario }).ToList();
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
					"numeroGanadores, idProvincia, idPlataforma, id_usuario, estado) " +
					" VALUES (" +
					"@nomSor, @termCon, @edadMin, @fecIn, @fecFin, @prem, " +
					"@descPrem, @numGan, @idProv, @idPlat, @idU, 1); SELECT LAST_INSERT_ID(); ";

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
					idU = sorteo.id_usuario,
                    est = sorteo.estado
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

		public bool EliminarSorteoActivo(long id)
		{
			bool cargado = false;
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "DELETE FROM ProvinciasXSorteo WHERE idSorteo=@id;" +
								"DELETE FROM EntradasXSorteo WHERE idSorteo=@id;" +
								"UPDATE Sorteos SET estado = 3 WHERE idSorteo=@id;";

				if (conect.Execute(sql, new { id = id }) > 0)
				{
					cargado = true;
				}

			}
			return cargado;
		}

		public bool RegistrarInscripcion(Inscripciones i)
		{
			bool cargado = false;
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string validar = "SELECT 1 FROM Inscripciones WHERE idSorteo = @idS AND idUsuario = @idU";
				string sql = "INSERT INTO Inscripciones (idSorteo, idUsuario, fechaInscripcion, estado) VALUES (@idS, @idU, @fec, 1)";

				if (!conect.Query<bool>(validar, new { idS = i.idSorteo, idU = i.idUsuario }).FirstOrDefault())
				{
					if (conect.Execute(sql, new { idS = i.idSorteo, idU = i.idUsuario, fec = i.fechaInscripcion }) > 0)
					{
						cargado = true;
					}
				}

			}
			return cargado;
		}

		public List<Inscripciones> getListaInscripcionesUsuario(long idUsuario, EstadosEnum estado)
		{
			List<Inscripciones> lst = new List<Inscripciones>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT i.idInscripcion as idInscripcion, nombreSorteo, fechaFin, fechaInscripcion, i.estado FROM Sorteos s, Inscripciones i WHERE s.idSorteo =  i.idSorteo AND i.idUsuario = @idU AND i.estado = @e";
				
				lst = conect.Query<Inscripciones>(sql, new { idU = idUsuario, e = (int)estado }).ToList(); //se llena la lista automaticamente con todos las inscripciones
				
			}

			return lst;
		}


		public List<Ganador> getListaUsuariosMasGanadores()
		{
			List<Ganador> lst = new List<Ganador>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT nombreUsuario, COUNT(g.idUsuario) AS sorteosGanados " +
					"FROM Ganadores g, Usuarios u WHERE g.idUsuario =  u.idUsuario GROUP BY nombreUsuario ORDER BY sorteosGanados desc";

				lst = conect.Query<Ganador>(sql).ToList(); //se llena la lista automaticamente con todos los ganadores

			}

			return lst;
		}

		public List<Ganador> getListaUsuariosMasGanadoresFiltrado(int filtro)
		{
			List<Ganador> lst = new List<Ganador>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string fil = "";
				switch (filtro)
				{
					case 1:
						fil = "AND MONTH(g.fechaGanador) = MONTH(NOW())";
						break;

					default:
						break;
				}

				string sql = "SELECT nombreUsuario, COUNT(g.idUsuario) AS sorteosGanados " +
					"FROM Ganadores g, Usuarios u WHERE g.idUsuario =  u.idUsuario " +
					fil +
					" GROUP BY nombreUsuario ORDER BY sorteosGanados desc LIMIT 10";

				lst = conect.Query<Ganador>(sql).ToList(); //se llena la lista automaticamente con todos los ganadores

			}

			return lst;
		}



		public List<Inscripciones> getListaUsuariosMasParticipativos()
		{
			List<Inscripciones> lst = new List<Inscripciones>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT nombreUsuario, count(idInscripcion) as cantidadInscripciones " +
					"FROM Inscripciones i, Usuarios u WHERE i.idUsuario = u.idUsuario and i.estado <> 3 " +
					"GROUP BY nombreUsuario ORDER BY cantidadInscripciones desc LIMIT 10";

				lst = conect.Query<Inscripciones>(sql).ToList(); //se llena la lista automaticamente con todos los inscriptos

			}

			return lst;
		}

		public List<Inscripciones> getListaUsuariosMasParticipativosFiltrado(int filtro)
		{
			List<Inscripciones> lst = new List<Inscripciones>();
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string fil = "";
				switch (filtro)
				{
					case 1:
						fil = "AND MONTH(i.fechaInscripcion) = MONTH(NOW())";
						break;
						
					default:
						break;
				}
				string sql = "SELECT nombreUsuario, count(idInscripcion) as cantidadInscripciones " +
					"FROM Inscripciones i, Usuarios u WHERE i.idUsuario = u.idUsuario and i.estado <> 3 " +
					 fil +
					" GROUP BY nombreUsuario ORDER BY cantidadInscripciones desc";

				lst = conect.Query<Inscripciones>(sql).ToList(); //se llena la lista automaticamente con todos los inscriptos

			}

			return lst;
		}



		public bool EliminarInscripcionActiva(long id)
		{
			bool cargado = false;
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "UPDATE Inscripciones SET estado = 3 WHERE idInscripcion=@id";

				if (conect.Execute(sql, new { id = id }) > 0)
				{
					cargado = true;
				}

			}
			return cargado;
		}

		public int obtenerCantidadGanadores(long idSorteo)
		{
			int cargado = 0;
			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT numeroGanadores FROM Sorteos s WHERE s.idSorteo = @idS";

				cargado = conect.Query<int>(sql, new { idS = idSorteo }).FirstOrDefault();


			}
			return cargado;
		}

		public object[] sortearGanadores(long idSorteo, int limit)
		{
			List<Ganador> lstGanadores = new List<Ganador>();
			object[] resultado = new object[2];

			using (var conect = new MySqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString))
			{
				string sql = "SELECT nombreUsuario, i.idUsuario FROM Inscripciones i, Usuarios u " +
							"WHERE i.idUsuario = u.idUsuario AND idSorteo = @idS " +
							"ORDER BY RAND() " +
							"LIMIT @limit; ";

				string sqlGanadores = "INSERT INTO Ganadores (idUsuario, idSorteo, fechaGanador) VALUES (@idU, @idS, @fec)";

				string sqlCambiarEstadoSorteo = "UPDATE Sorteos SET estado = 2 WHERE idSorteo = @idS";

				string sqlNombreSorteo = "SELECT nombreSorteo FROM sorteos s WHERE s.idSorteo = @idS";
				string sqlNotificacion = "INSERT INTO Notificaciones (detalle) VALUES (@det); SELECT LAST_INSERT_ID();";
				string sqlNotificacionUsuario = "INSERT INTO UsuariosXnotificacion (idUsuario, idNotificacion, fechaNotificacion, estado) " +
					"VALUES (@idU, @idN, @fec, @est)";
				
				lstGanadores = conect.Query<Ganador>(sql, new { idS = idSorteo, limit = limit }).ToList();

				// hace los insert de los ganadores en la tabla Ganadores

				int c = 0; // contador para validar que se haga los insert en ganadores
				foreach (var g in lstGanadores)
				{
					//hace el insert
					g.fechaGanador = DateTime.Now;
					conect.Query<bool>(sqlGanadores, new { idU = g.idUsuario, idS = idSorteo, fec = g.fechaGanador }).FirstOrDefault();
					var nombreSorteo = conect.Query<string>(sqlNombreSorteo, new { idSorteo }).FirstOrDefault();
					var detalle = "Felicitaciones! Ganaste el sorteo " + nombreSorteo;
					var idNotificacion = conect.Query<long>(sqlNotificacion, new { det = detalle }).FirstOrDefault();
					conect.Query<bool>(sqlNotificacionUsuario, new {
						idU = g.idUsuario,
						idN = idNotificacion,
						fec = DateTime.Now,
						est = 0
					}).FirstOrDefault();
					c++;
				}

				if (c > 0) // validacion para cambiar estado a sorteo
				{
					if(conect.Execute(sqlCambiarEstadoSorteo, new { idS = idSorteo }) > 0){
						resultado[0] = lstGanadores;
						resultado[1] = true;
					}
				}
				
			}
			return resultado;
		}



	}
}