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
    public class AlumnoController : Controller
    {
         //GET: Alumno
        [SecuritySession]
        public ActionResult Asistencia()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        ////LISTAR ASISTENCIA

        [HttpPost]
        public async Task<JsonResult> ListarAsistencia()
        {
            try
            {
                int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                ViewBag.GrolUsuario = irolusuario;
                ViewBag.GIDUsuario = idusuario;
                var objResultado = new object();
                List<edAsistencia> lenUsuario = new List<edAsistencia>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/asistencia/wsListarAsistenciaAlumno?widalumno=" + idusuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        lenUsuario = JsonConvert.DeserializeObject<List<edAsistencia>>(rwsapilu);
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