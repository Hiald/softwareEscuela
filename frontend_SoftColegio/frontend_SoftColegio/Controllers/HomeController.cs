using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using frontend_SoftColegio.Filters;
using frontendED;
using frontendUtil;
using Newtonsoft.Json;

namespace frontend_SoftColegio.Controllers
{
    public class HomeController : Controller
    {
        [SecuritySession]
        public async Task<ActionResult> Index()
        {
            try
            {
                var objResultado = new object();
                ViewBag.MenuPrincipal = "active";
                int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
                string susuario = UtlAuditoria.ObtenerNombre()
                        + " " + UtlAuditoria.ObtenerApellidoPaterno() + " " + UtlAuditoria.ObtenerApellidoMaterno();
                ViewBag.GrolUsuario = irolusuario;
                ViewBag.GNombreUsuario = susuario;
                ViewBag.GGradoUsuario = UtlAuditoria.ObtenerIdGrado();
                int idnivel = UtlAuditoria.ObtenerIdNivel();
                int idgrado = UtlAuditoria.ObtenerIdGrado();
                int idtipousuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/clase/wsListarClaseVivo?widnivel=" + idnivel
                        + "&widgrado=" + idgrado + "&widtipousuario=" + idtipousuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);

                    }
                }
                ViewBag.Lista = loenClase;
                return View();
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }


        //[SecuritySession]
        //public ActionResult Index()
        //{
        //    ViewBag.MenuPrincipal = "active";
        //    int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
        //    string susuario = UtlAuditoria.ObtenerNombre()
        //            + " " + UtlAuditoria.ObtenerApellidoPaterno() + " " + UtlAuditoria.ObtenerApellidoMaterno();
        //    ViewBag.GrolUsuario = irolusuario;
        //    ViewBag.GNombreUsuario = susuario;
        //    ViewBag.GGradoUsuario = UtlAuditoria.ObtenerIdGrado();
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}