using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using frontendED;
using frontendUtil;

namespace frontend_SoftColegio.Controllers
{
    public class cursoController : Controller
    {
        // GET: curso
        public ActionResult Index()
        {
            return View();
        }

        //ACTIVO : inserta al docente asignando los cursos
        [HttpPost]
        public async Task<JsonResult> InsertarCursoDocente(int idusuario, int idnivel, int idgrado, int idcurso)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/curso/wsInsertarCursoDocente?wsidusuario=" + idusuario
                        + "&wsidnivel=" + idnivel + "&wsidgrado=" + idgrado + "&wsidcurso=" + idcurso
                        + "&wsiestado=" + 1);

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

        //ACTIVO : 
        [HttpPost]
        public async Task<JsonResult> ListarClaseGeneral()
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edCurso> loenClase = new List<edCurso>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/curso/wsListarCurso?wsitipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edCurso>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenClase.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenClase
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