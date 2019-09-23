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
					// enviador de email desde el servidor
					//string dominio = "http://playgarden.com.ar";
					//StringBuilder body = new StringBuilder();
					//body.AppendFormat("<html>");
					//body.AppendFormat("<body style=\"background-image: linear-gradient(to right top, #ff7a7a, #f8a37e, #efc599, #ebe2c4, #f7f8f4);text-align: center;\">");
					//body.AppendFormat("<div style=\"padding: 10px;width: 50%; margin: auto;\">");
					//body.AppendFormat("<img width=\"300\" src=\"http://playgarden.com.ar/Content/img/logo.png\"/>");
					//body.AppendFormat("<hr style=\"border:0px none white; border-top:1px solid lightgrey;\"></div>");
					//body.AppendFormat("<br/>");
					//body.AppendFormat("<div>");
					//body.AppendFormat("<h5 style='color: white; font-size: 18px;'>");
					//body.AppendFormat("Haz click en el siguiente boton para activar tu cuenta:");
					//body.AppendFormat("</h5>");
					//body.AppendFormat("</div>");
					//body.AppendFormat("<div style=\"width: 20%; margin: auto;  border-radius: 20px;background-color: #FF8800;\" >");
					//body.AppendFormat(@"<a style='text-decoration: none;color: white;font-size: 17px;' href='{0}/Home/ActivarCuenta?idUsuario={1}' >Activar</a>", dominio, lastId.ToString());
					//body.AppendFormat("</div><br>");
					//body.AppendFormat("</body>");
					//body.AppendFormat("</html>");


					//const string SERVER = "relay-hosting.secureserver.net";
					//string desde = "pgardenjardin@gmail.com";

					//MailMessage oMail = new MailMessage();
					//oMail.From = desde.ToString();
					//oMail.To = "pgardenjardin@gmail.com";
					//oMail.Subject = "Nuevo mail para Fprog Developing";
					//oMail.BodyFormat = MailFormat.Html;
					//oMail.Priority = MailPriority.High;
					//oMail.BodyEncoding = Encoding.UTF8;
					//oMail.Body = body.ToString();


					//SmtpMail.SmtpServer = SERVER;
					//SmtpMail.Send(oMail);
					//oMail = null;


					///*-------------------------------*/

					//TempData["cargado"] = "cargado";
					//MvcModel mvcn = new MvcModel();
					//mvcn.Msj = new Mensaje();
					//mvcn.UsrLog = new UsuarioLogin();
					//mvcn.UsrReg = new Usuario();
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
				return View(mvc);
			}

		}

		
	}
}