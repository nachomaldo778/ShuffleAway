using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using ShuffleAway_.Models;
using ShuffleAway_.Models.Datos;
using System.Collections.Generic;
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
				mvc.lstErrores = list;
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
				mvc.lstProvincias = datos.getListaProvincias();
				Session["usuario"] = mvc.usuario;
				//se guarda el usuario en las cookies
				FormsAuthentication.SetAuthCookie(mvc.usuario.email, false);
				return RedirectToAction("Index", "Home", new { mvc = mvc });
				//return View("/Views/Home/Index.cshtml", mvc);
			}
			else
			{
				TempData["error-login"] = "error";
				mvc = new MvcModel();
				mvc.lstProvincias = getListProvincias(datos);
				return View("/Views/Home/Index.cshtml", mvc);
			}

		}

		[AllowAnonymous]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			MvcModel mvc = new MvcModel();
			Session.Abandon();

			return RedirectToAction("Index", "Home", mvc);
		}

		public ActionResult UpgradeUsr() { return View(); }

		[HttpPost]
		public ActionResult UpgradeUsr(string correo)
		{
			AccesoDatos datos = new AccesoDatos();

			/*MercadoPago*/
			MercadoPago.SDK.ClientId = "6391733921836603";
			MercadoPago.SDK.ClientSecret = "zLNcdYwlDIFIEFzU6m7Jl414RRpkBUVV";


			decimal precioUp = 1; // cambiar valor a cobrar!!!

			Preference preference = new Preference();
			preference.Items.Add(
			  new Item()
			  {
				  Id = "1", // crear tabla en base de datos y luego obtener el ultimo ID y sumarle 1
				  Title = "Shuffle Away!",
				  Quantity = 1,
				  CurrencyId = 0,
				  UnitPrice = precioUp,
			  }
			);
			// Setting a payer object as value for Payer property
			preference.Payer = new Payer()
			{
				Email = correo
			};

			preference.BackUrls = new BackUrls()
			{
				Success = "https://www.google.com.ar",
				Failure = "https://www.google.com.ar",
				Pending = "https://www.google.com.ar"
			};

			preference.AutoReturn = AutoReturnType.approved;
			// Save and posting preference
			preference.Save();

			var url = preference.InitPoint;

			return Json(new { url = url });

		}

	}
}