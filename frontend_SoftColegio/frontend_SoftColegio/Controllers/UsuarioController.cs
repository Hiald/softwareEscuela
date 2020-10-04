using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontend_SoftColegio.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Ejemplo(string p1, string p2,string p3 , string p4)
        {
            return View("Index");
        }
                
    }
}