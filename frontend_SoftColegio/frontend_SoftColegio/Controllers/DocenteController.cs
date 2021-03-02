using frontend_SoftColegio.Filters;
using frontendUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontend_SoftColegio.Controllers
{
    public class DocenteController : Controller
    {
        // GET: Docente
        [SecuritySession]
        public ActionResult Asistencia()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GIDUsuario = idusuario;
            return View();
        }
    }
}