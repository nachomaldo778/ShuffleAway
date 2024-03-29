﻿using System.Web;
using System.Web.Optimization;

namespace ShuffleAway_
{
	public class BundleConfig
	{
		// Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/mdb.js",
						"~/Scripts/popper.min.js," +
						"~/Scripts/shuffleScript.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
			// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));

			//bundles.Add(new StyleBundle("~/Content/css").Include(
			//		  "~/Content/bootstrap.css",
			//		  "~/Content/site.css"));

			//string mdb = @"~/Content/mdb.css";
			string bootstrapLib = @"~/Content/bootstrap.css";
			string siteCss = @"~/Content/site.css";

			//bundles.Add(new StyleBundle("~/Content/MdBootstrap").Include(mdb));
			bundles.Add(new StyleBundle("~/Content/bootstrapCore").Include(bootstrapLib, siteCss));
		}
	}
}
