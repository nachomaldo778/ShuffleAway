using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShuffleAway_.Controllers
{
    public class InscripcionController : Controller
    {
        // GET: Inscripcion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InscripcionSorteo()
        {
            return View();
        }
        public ActionResult Ajustes()
        {
            return View();
        }

        public ActionResult MisInscripciones()
        {
            return View();
        }

		public ActionResult PruebaFb()
		{
			var token = "EAAHb8YzCPAQBANS3whwaPVCSg6EqYmZCJpPFKocI5ztvZCTfmSXnrR1L9uPSQKwUWkJoNCZB6GOxslSBZBk9hJ8QIdRMdmok3zPJiEe5BjsZAuCnNd02xAubHXs6ZCK7U6GeehZBynJxOrn4bXfPPRE3HZB7ZAZACgUvIFZCHlGvZAtaYV3h8W4zSZB57";
			
			var client = new FacebookClient(token);

			dynamic me = client.Get("me");
			string firstName = me.first_name;
			string lastName = me.last_name;

			return View();
		}
    }
}