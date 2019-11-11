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
				mvc.lstSorteos = datos.getListaSorteosActivos(0); // 0 porque no se requiere un filtro
				Session["lstSorteos"] = mvc.lstSorteos;

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
						TempData["okCrearSorteo"] = "ok";
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
				TempData["err-crear-sorteo"] = "err";
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
				m.lstProvincias = new AccesoDatos().getListaProvincias();
				m.usuario.fechaNacimiento = conversor.txtAFecha(m.usuario.strFechaNac);

				m.usuario = datos.ActualizarUsuario(m.usuario);
				if (m.usuario != null)
				{
					TempData["ok-upd-ajustes"] = "ok";
					//return RedirectToAction("/Views/Plataforma/Ajustes.cshtml", mvc);
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
				mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

				mvc.lstSorteos = new AccesoDatos().getListaSorteosActivos(mvc.usuario.idUsuario);

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


				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult UsuariosMasGanadores()
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


				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult UsuariosMasCreadores()
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


				return View(mvc);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult InscripcionSorteo(long idSorteo)
		{
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

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
					mvc.lstSorteos = datos.getListaSorteosActivos(0); // 0 porque no se requiere un filtro
					Session["lstSorteos"] = mvc.lstSorteos;

					return View("SorteosActivos", mvc);
				}
			}

			return RedirectToAction("Index", "Home");
			// creo el modelo MvcModel para despues 
			// recibir el usuario que se logueó desde el Home
			//MvcModel mvc = new MvcModel();
			//mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			//if (mvc.usuario != null)
			//{
			//	//se llenan las listas para rellenar los combos
			//	AccesoDatos datos = new AccesoDatos();
			//	mvc.lstPlataformas = datos.getListaPlataformas();
			//	mvc.lstProvincias = datos.getListaProvincias();
			//	mvc.lstEntradas = datos.getListaEntradas();


			//	return View(mvc);
			//}

			//return RedirectToAction("Index", "Home");
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
			// creo el modelo MvcModel para despues 
			// recibir el usuario que se logueó desde el Home
			MvcModel mvc = new MvcModel();
			mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

			mvc.lstSorteos = new AccesoDatos().getListaSorteosActivos(mvc.usuario.idUsuario);


			if (new AccesoDatos().EliminarSorteoActivo(id))
			{
				TempData["exitoEliminarSorteo"] = "exito";
			}
			else
			{
				TempData["errorEliminarSorteo"] = "error";
			}


			return RedirectToAction("MisSorteosActivos", "Plataforma", mvc);
		}
	}
}