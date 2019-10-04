using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ShuffleAway_.Controllers
{
    public class UsuarioController : Controller
    {

		public ActionResult Registrar()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Registrar(MvcModel mvc)
		{

			if (ModelState.IsValid)
			{
				//objeto para traer datos de la BD
				AccesoDatos datos = new AccesoDatos();

				//hash para encriptar la contraseña
				mvc.usuario.pass = Crypto.HashPassword(mvc.usuario.pass);
				
				//se registra el usuario en la base de datos y obtiene el nuevo ID generado
				long lastId = datos.RegistrarUsuario(mvc.usuario);

				//verifica si se guardó el usuario 
				if (lastId > 0)
				{
					mvc = new MvcModel();
					return RedirectToAction("Index", "Home", mvc);
				}
				else
				{
					TempData["existe"] = "existe";
				}
			}
			else
			{
				List<string> list = new List<string>();
				foreach (var modelStateKey in ViewData.ModelState.Keys)
				{
					var modelStateVal = ViewData.ModelState[modelStateKey];
					foreach (var item in modelStateVal.Errors)
					{
						var key = modelStateKey;
						list.Add(key);
					}
				}
				TempData["errorReg"] = "error";
				TempData["cd-div"] = "cd-signup";
				mvc.ListErrores = list;
			}


			return View("Index", mvc);
		}

		public List<Provincia> getListProvincias(AccesoDatos datos)
		{
			return datos.getListaProvincias();
		}

		public ActionResult Login(MvcModel mvc)
		{
			//objeto para traer datos de la BD
			AccesoDatos datos = new AccesoDatos();
			//guardo el usuario de la base de datos
			mvc.usuario = datos.getLoginUsuario(mvc.usuario);
			//pregunto si esta logueado para redireccionarlo a la plataforma
			if (mvc.usuario.logueado)
			{
				//guardo el usuario en la sesión
				Session["usuario"] = mvc.usuario;
				//se guarda el usuario en las cookies
				FormsAuthentication.SetAuthCookie(mvc.usuario.email, false);
				return RedirectToAction("Index", "Plataforma");
			}
			else
			{
				TempData["error"] = "error";
				mvc = new MvcModel();
				mvc.lstProvincias = getListProvincias(datos);
				return RedirectToAction("Index", "Home");
			}

		}

		
	}
}