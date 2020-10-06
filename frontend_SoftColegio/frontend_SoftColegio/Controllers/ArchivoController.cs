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
    public class ArchivoController : Controller
    {
        // admin y profesor gestion
        public ActionResult ArchivoGestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> InsertarArchivoGestion(int idgrado
            , string nombre, string descripcion, string rutaenlace, string rutavideo
            , int categoria, string imagen, string imagenruta, int orden)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsInsertarArchivo?widgrado=" + idgrado
                        + "&widusuario=" + idusuario + "&wnombre=" + nombre + "&wdescripcion=" + descripcion
                        + "&wrutaenlace=" + rutaenlace + "&wrutavideo=" + rutavideo
                        + "&wcategoria=" + categoria + "&wimagen=" + imagen + "&wimagenruta=" + imagenruta
                        + "&worden=" + orden + "&westado=" + estado + "&wfecharegistro=" + wfechaRegistro);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
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

        [HttpPost]
        public async Task<JsonResult> ListarArchivoGeneral(int idclase)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsObtenerArchivo?widclase=" + idclase);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenArchivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenArchivo
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public async Task<JsonResult> ActualizarArchivoAlumno(int idarchivodetalle, int nota
                                         , string observacion, int tiponota)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsActualizarArchivoDetalle?widarchivodetalle=" + idarchivodetalle
                        + "&wnota=" + nota + "&wobservacion=" + observacion + "&widusuario=" + idusuario
                        + "&wtiponota=" + tiponota + "&westado=" + estado + "&wfecharegistro=" + wfechaRegistro);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
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

        [HttpPost]
        public async Task<JsonResult> InsertarArchivoAlumno(int idarchivo
                            , string imagen, int nota, string observacion, string enlace)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsInsertarArchivoDetalle?widarchivo=" + idarchivo
                        + "&widusuario=" + idusuario + "&wimagen=" + imagen + "&wnota=" + nota + "&wobservacion=" + observacion
                        + "wenlace" + enlace + "wfecharegistro" + wfechaRegistro);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
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

        [HttpPost]
        public async Task<JsonResult> ListarArchivoAlumno(int idarchivo)
        {
            try
            {
                var objResultado = new object();
                int IdUsuario = UtlAuditoria.ObtenerIdUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsListarArchivoDetalle?widarchivo=" + idarchivo + "&widusuario=" + IdUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenArchivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenArchivo
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