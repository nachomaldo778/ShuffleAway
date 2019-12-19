using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ShuffleAway_.Controllers
{
	public class PlataformaController : Controller
	{
		private static Conversor conversor = new Conversor();
		// GET: Plataforma
		public ActionResult SorteosActivos()
		{
			// creo el modelo MvcModel para despues 
			// recibir el usuario que se logueó desde el Home
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			if (mvc.usuario != null)
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();
				mvc.lstPlataformas = datos.getListaPlataformas();
				mvc.lstProvincias = datos.getListaProvincias();
				mvc.lstEntradas = datos.getListaEntradas();
				mvc.lstSorteos = datos.getListaSorteosActivos(0, EstadosEnum.En_Curso); // 0 porque no se requiere un filtro
				Session["lstSorteos"] = mvc.lstSorteos;

                if (string.IsNullOrEmpty(mvc.usuario.nombre) && 
                    string.IsNullOrEmpty(mvc.usuario.apellido) &&
                    string.IsNullOrEmpty(mvc.usuario.nombreUsuario))
                {
                    TempData["usuarioIncompleto"] = "error";
                }

				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult CrearSorteo()
		{
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session
			if (mvc.usuario != null)
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();
				mvc.lstPlataformas = datos.getListaPlataformas();
				mvc.lstProvincias = datos.getListaProvincias();
				mvc.lstEntradas = datos.getListaEntradas();

				if (string.IsNullOrEmpty(mvc.usuario.apellido) ||
					string.IsNullOrEmpty(mvc.usuario.nombre) ||
					mvc.usuario.fechaNacimiento == null ||
					string.IsNullOrEmpty(mvc.usuario.nombreUsuario))
				{
					TempData["validar"] = "error";
				}

				return View(mvc);
			}
			return RedirectToAction("Index", "Home");

		}

		[HttpPost]
		public ActionResult CrearSorteo(MvcModel mvc)
		{
			try
			{
				AccesoDatos datos = new AccesoDatos();

				//seteo las fechas convertidas
				mvc.sorteo.fechaInicio = conversor.txtAFecha(mvc.sorteo.strFechaIn);
				mvc.sorteo.fechaFin = conversor.txtAFecha(mvc.sorteo.strFechaFn);
				mvc.usuario = (Usuario)Session["usuario"];
				mvc.sorteo.id_usuario = mvc.usuario.idUsuario;

				if (ModelState.IsValid && (mvc.sorteo.lstIdEntradas.Count > 0 && mvc.sorteo.lstIdProvincias.Count > 0))
				{
					if (datos.RegistrarSorteo(mvc.sorteo) > 0)
					{
						TempData["okCrearSorteo"] = "exito";
						ModelState.Clear();

						//se llenan las listas para rellenar los combos
						mvc.lstPlataformas = datos.getListaPlataformas();
						mvc.lstProvincias = datos.getListaProvincias();
						mvc.lstEntradas = datos.getListaEntradas();
						return RedirectToAction("MisSorteosActivos", "Plataforma", mvc);
					}
				}
				else
				{
					mvc.lstErrores = new List<string>();
					foreach (var modelStateVal in ViewData.ModelState.Values)
					{
						foreach (var item in modelStateVal.Errors)
						{
							mvc.lstErrores.Add(item.ErrorMessage);
						}
					}
				}
				mvc.lstPlataformas = datos.getListaPlataformas();
				mvc.lstProvincias = datos.getListaProvincias();
				mvc.lstEntradas = datos.getListaEntradas();
				TempData["errorCrearSorteo"] = "error";
				return View("Index", mvc);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		[HttpPost]
		public JsonResult DevolverSorteoConfirmacion(Sorteo sorteo)
		{
			return Json(new { success = true, model = sorteo }, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Ajustes()
		{
			// creo el modelo MvcModel para despues 
			// recibir el usuario que se logueó desde el Home
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session
			mvc.lstProvincias = new AccesoDatos().getListaProvincias();

			return View(mvc);
		}

		[HttpPost]
		public ActionResult Ajustes(MvcModel m)
		{
			try
			{
				AccesoDatos datos = new AccesoDatos();
				MvcModel mvc = new MvcModel();
				mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session
				m.usuario.idUsuario = mvc.usuario.idUsuario; // tomo solo el id del usuario de la Session
				m.usuario.idTipoUsuario = mvc.usuario.idTipoUsuario;
				m.lstProvincias = new AccesoDatos().getListaProvincias();
				m.usuario.fechaNacimiento = conversor.txtAFecha(m.usuario.strFechaNac);

				m.usuario = datos.ActualizarUsuario(m.usuario);
				if (m.usuario != null)
				{
                    //m.usuario.logueado = true;
                    Session["usuario"] = m.usuario;
                    TempData["ok-upd-ajustes"] = "ok";
					return View(m);
				}
				TempData["error"] = "error";


				return View(mvc);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public ActionResult MisSorteosActivos()
		{
			try
			{
				// creo el modelo MvcModel para despues 
				// recibir el usuario que se logueó desde el Home
				MvcModel mvc = new MvcModel();
				AccesoDatos datos = new AccesoDatos();
				mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

				mvc.lstSorteos = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.En_Curso);
				mvc.lstSorteosHistorial = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.Cancelado);

				return View(mvc);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public ActionResult MisInscripciones()
		{
			// creo el modelo MvcModel para despues 
			// recibir el usuario que se logueó desde el Home
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			if (mvc.usuario != null)
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();
				mvc.lstPlataformas = datos.getListaPlataformas();
				mvc.lstProvincias = datos.getListaProvincias();
				mvc.lstEntradas = datos.getListaEntradas();
				mvc.lstInscripciones = datos.getListaInscripcionesUsuario(mvc.usuario.idUsuario, EstadosEnum.En_Curso, 0);
                mvc.lstInscripcionesHistorial = datos.getListaInscripcionesUsuario(mvc.usuario.idUsuario, EstadosEnum.Cancelado, EstadosEnum.Finalizado);

                return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult UsuariosMasGanadores(int filtro = 0)
		{
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"];

			if (mvc.usuario != null)
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();
				// evalua si el filtro tiene un numero mayor a 0, guarda la lista filtrada sino la lista historica 
				mvc.lstGanadores = (filtro == 0) ? datos.getListaUsuariosMasGanadores() : datos.getListaUsuariosMasGanadoresFiltrado(filtro);


				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult UsuariosMasParticipativos(int filtro = 0)
		{
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"];

			if (mvc.usuario != null)
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();

				// evalua si el filtro tiene un numero mayor a 0, guarda la lista filtrada sino la lista historica 
				mvc.lstInscripciones = (filtro == 0) ? datos.getListaUsuariosMasParticipativos() : datos.getListaUsuariosMasParticipativosFiltrado(filtro);

				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}
		

		public ActionResult InscripcionSorteo(long idSorteo, int edadMinima)
		{
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			if (edadMinima <= conversor.calcularEdad(mvc.usuario.fechaNacimiento))
			{
				//se guardan los ids para hacer el insert
				Session["idUsuario"] = mvc.usuario.idUsuario;
				Session["idSorteo"] = idSorteo;

				if (mvc.usuario.logueado)
				{
					List<Sorteo> lstSorteos = (List<Sorteo>)Session["lstSorteos"];
					if (lstSorteos.Count > 0 && lstSorteos != null) // se valida que la lista de sorteos no este vacia
					{
						Sorteo s = lstSorteos.Where(x => x.idSorteo == idSorteo).FirstOrDefault();
						mvc.sorteo = s;

						return View(mvc);
					}


					if (mvc.usuario != null)
					{
						//se llenan las listas para rellenar los combos
						AccesoDatos datos = new AccesoDatos();
						mvc.lstPlataformas = datos.getListaPlataformas();
						mvc.lstProvincias = datos.getListaProvincias();
						mvc.lstEntradas = datos.getListaEntradas();
						mvc.lstSorteos = datos.getListaSorteosActivos(0, EstadosEnum.En_Curso); // 0 porque no se requiere un filtro
						Session["lstSorteos"] = mvc.lstSorteos;

						return View("SorteosActivos", mvc);
					}
				}
			}
			else
			{
				//se llenan las listas para rellenar los combos
				AccesoDatos datos = new AccesoDatos();
				mvc.lstPlataformas = datos.getListaPlataformas();
				mvc.lstProvincias = datos.getListaProvincias();
				mvc.lstEntradas = datos.getListaEntradas();
				mvc.lstSorteos = datos.getListaSorteosActivos(0, EstadosEnum.En_Curso); // 0 porque no se requiere un filtro
				Session["lstSorteos"] = mvc.lstSorteos;
				TempData["error-inscribirse"] = "error";

				return View("SorteosActivos", mvc);
			}




			return RedirectToAction("Index", "Home");
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			MvcModel mvc = new MvcModel();
			Session.Abandon();

			return RedirectToAction("Index", "Home", mvc);
		}

		public ActionResult EliminarSorteoActivo(long id)
		{
			MvcModel mvc = new MvcModel();
			AccesoDatos datos = new AccesoDatos();
			mvc.usuario = (Usuario)Session["usuario"];


			if (new AccesoDatos().EliminarSorteoActivo(id))
			{
				mvc.lstSorteos = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.En_Curso);
				mvc.lstSorteosHistorial = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.Cancelado);
				TempData["exitoEliminarSorteo"] = "exito";
			}
			else
			{
				TempData["errorEliminarSorteo"] = "error";
			}


			return RedirectToAction("MisSorteosActivos", "Plataforma", mvc);
		}

		[HttpPost]
		public ActionResult InscribirASorteo()
		{

			AccesoDatos datos = new AccesoDatos();

			Inscripciones i = new Inscripciones()
			{
				idSorteo = (long)Session["idSorteo"],
				idUsuario = (long)Session["idUsuario"],
				fechaInscripcion = DateTime.Now
			};

			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			if (datos.RegistrarInscripcion(i))
			{
				TempData["insc-exito"] = "ok";
			}
			else
			{
				TempData["insc-error"] = "error";
			}
			//se llenan las listas para rellenar los combos

			mvc.lstPlataformas = datos.getListaPlataformas();
			mvc.lstProvincias = datos.getListaProvincias();
			mvc.lstEntradas = datos.getListaEntradas();
			mvc.lstInscripciones = datos.getListaInscripcionesUsuario(mvc.usuario.idUsuario, EstadosEnum.En_Curso, 0); // 1 es estado En Curso
			mvc.lstInscripcionesHistorial = datos.getListaInscripcionesUsuario(mvc.usuario.idUsuario, EstadosEnum.Cancelado, EstadosEnum.Finalizado);

			return View("MisInscripciones", mvc);
		}

		public ActionResult CambiarEstadoInscripcion(long id, long estado)
		{
			MvcModel mvc = new MvcModel();
			AccesoDatos datos = new AccesoDatos();
			mvc.usuario = (Usuario)Session["usuario"];

			if (datos.EliminarInscripcionActiva(id, (EstadosEnum)estado))
			{
				mvc.lstInscripcionesHistorial = datos.getListaInscripcionesUsuario(mvc.usuario.idUsuario, EstadosEnum.Cancelado, EstadosEnum.Finalizado);
				TempData["exitoEliminarInscripcion"] = "exito";
			}
			else
			{
				TempData["errorEliminarInscripcion"] = "error";
			}


			return RedirectToAction("MisInscripciones", "Plataforma", mvc);
		}

		public ActionResult GenerarSorteo(long idSorteo)
		{
			MvcModel mvc = new MvcModel();
			AccesoDatos datos = new AccesoDatos();
			mvc.usuario = (Usuario)Session["usuario"];

			int cantidadGanadores = datos.obtenerCantidadGanadores(idSorteo);
			if (cantidadGanadores > 0)
			{
				mvc.lstGanadores = (List<Ganador>)datos.sortearGanadores(idSorteo, cantidadGanadores)[0];
				var idInscripcion = datos.GetIdInscripcion(idSorteo);
				datos.EliminarInscripcionActiva(idInscripcion, EstadosEnum.Finalizado);
				TempData["exito-sorteo"] = "ok";
			}
			else
			{
				TempData["error-sorteo"] = "error";
			}

			mvc.lstSorteos = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.En_Curso);
			mvc.lstSorteosHistorial = datos.getListaSorteosActivos(mvc.usuario.idUsuario, EstadosEnum.Cancelado);

			return View("MisSorteosActivos", mvc);
		}

        public ActionResult Redirigir(string accion, string controlador)
        {
            var usrSession = Session["usuario"] as Usuario;
            Usuario u = new AccesoDatos().getLoginUsuario(usrSession, false);
            return RedirectToAction(accion, controlador, u);
        }


	}
}