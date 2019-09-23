using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShuffleAway_.Controllers
{
	public class HomeController : Controller
	{
		private static AccesoDatos datos = new AccesoDatos();

		public List<Provincia> getListProvincias()
		{
			return datos.getListaProvincias();
		}
		public ActionResult Index()
		{
			MvcModel mvc = new MvcModel();
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}