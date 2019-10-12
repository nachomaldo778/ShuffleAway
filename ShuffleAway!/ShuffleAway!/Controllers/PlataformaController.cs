using Newtonsoft.Json;
using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShuffleAway_.Controllers
{
	public class PlataformaController : Controller
	{
		private static Conversor conversor = new Conversor();
		// GET: Plataforma
		public ActionResult Index()
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

		public ActionResult CrearSorteo() { return View(); }

		[HttpPost]
		public ActionResult CrearSorteo(MvcModel mvc)
		{
			try
			{
				AccesoDatos datos = new AccesoDatos();

				//seteo las fechas convertidas
				mvc.sorteo.fechaInicio = conversor.txtAFecha(mvc.sorteo.strFechaIn);
				mvc.sorteo.fechaFin = conversor.txtAFecha(mvc.sorteo.strFechaFn);


				if (ModelState.IsValid && (mvc.sorteo.lstIdEntradas.Count > 0 && mvc.sorteo.lstIdProvincias.Count > 0))
				{
					if (datos.RegistrarSorteo(mvc.sorteo) > 0)
					{
						TempData["ok"] = "ok";
						ModelState.Clear();

						//se llenan las listas para rellenar los combos
						mvc.lstPlataformas = datos.getListaPlataformas();
						mvc.lstProvincias = datos.getListaProvincias();
						mvc.lstEntradas = datos.getListaEntradas();
						return RedirectToAction("Index", "Plataforma", mvc);
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
				TempData["err"] = "err";
				return View("Index", mvc);
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		[HttpPost]
		public JsonResult DevolverSorteoConfirmacion(Sorteo sorteo)
		{
			return Json(new { success = true, model = sorteo }, JsonRequestBehavior.AllowGet);
		}
        public ActionResult Ajustes()
        {
            return View();
        }

		public ActionResult MisSorteosActivos()
        {
			try
			{
				// creo el modelo MvcModel para despues 
				// recibir el usuario que se logueó desde el Home
				MvcModel mvc = new MvcModel();
				mvc.usuario = (Usuario)Session["usuario"]; //recibe el usuario que viene del Home a traves de la Session

				mvc.lstSorteos = new AccesoDatos().getListaSorteosFiltrado(mvc.usuario.idUsuario);
				return View(mvc);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}