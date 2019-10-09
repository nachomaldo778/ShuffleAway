using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShuffleAway_.Controllers
{
	public class PlataformaController : Controller
	{
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
			AccesoDatos datos = new AccesoDatos();
			
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

			TempData["err"] = "err";
			return View(mvc);
		}
        public ActionResult Ajustes()
        {
            return View();
        }
        public ActionResult MisSorteosActivos()
        {
            return View();
        }
    }
}