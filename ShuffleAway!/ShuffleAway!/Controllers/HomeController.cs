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
        public ActionResult PlanPrecios()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult About()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult PrivacidadYCondiciones()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult SorteosActivos()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult ComoParticipar()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult ComoCrear()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult Demostracion()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult Contacto()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }
        public ActionResult Faq()
        {
            MvcModel mvc = new MvcModel();
            mvc.lstProvincias = getListProvincias();
            return View(mvc);
        }

		public ActionResult CheckoutUsr()
		{
			MvcModel mvc = new MvcModel();
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
    }
}