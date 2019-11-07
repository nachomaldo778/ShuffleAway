﻿using ShuffleAway_.Models;
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
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult PlanPrecios()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult About()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult PrivacidadYCondiciones()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult ComoParticipar()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult ComoCrear()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
		public ActionResult Demostracion()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}

		public ActionResult CheckoutUsr()
		{
			MvcModel mvc = new MvcModel();
			if (Session["usuario"] != null)
			{
				mvc.usuario = (Usuario)Session["usuario"];
			}
			mvc.lstProvincias = getListProvincias();
			return View(mvc);
		}
	}
}