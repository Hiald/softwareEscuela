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
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> InsertarAsistencia(int widclase, int widdocente, int widalumno, int widtipoasistencia)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                Int16 estado = 1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/asistencia/wsInsertarAsistencia?widclase=" + widclase
                        + "&widdocente=" + widdocente + "&widalumno=" + widalumno + "&widtipoasistencia=" + widtipoasistencia + "&wfechaingreso=" + wfechaRegistro);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1 || idGenerado == 0)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = -1,
                                iResultadoIns = "Ha ocurrido un error, intentalo nuevamente. Error: BCK"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Registrado correctamente"
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