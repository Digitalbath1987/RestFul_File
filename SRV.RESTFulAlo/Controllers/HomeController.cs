using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SRV.Entidades;
using SRV.Utilidades;

namespace SRV.RESTFulAlo.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// VISTA PRINCIPAL DEL HOME
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {


            return View();
        }


     
    }
}
