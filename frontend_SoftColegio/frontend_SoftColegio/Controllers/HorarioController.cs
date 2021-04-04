using frontend_SoftColegio.Filters;
using frontendED;
using frontendUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace frontend_SoftColegio.Controllers
{
    public class HorarioController : Controller
    {
        // GET: Horario
        [SecuritySession]
        public ActionResult Index()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idGrado = UtlAuditoria.ObtenerIdGrado();
            int idNivel = UtlAuditoria.ObtenerIdNivel();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GidNivel = idNivel;
            ViewBag.GidGrado = idGrado;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ListarHorario()
        {
            try
            {
                int idnivel = UtlAuditoria.ObtenerIdNivel();
                int idgrado = UtlAuditoria.ObtenerIdGrado();
                
                //ViewBag.GrolUsuario = irolusuario;
                //ViewBag.GIDUsuario = idusuario;
                var objResultado = new object();
                List<edHorario> lenUsuario = new List<edHorario>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/horario/wsListarHorario?widnivel=" + idnivel + "&widgrado=" + idgrado);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        lenUsuario = JsonConvert.DeserializeObject<List<edHorario>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = lenUsuario.Count,
                    iTotalDisplayRecords = 1,
                    aaData = lenUsuario
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

    }
}
