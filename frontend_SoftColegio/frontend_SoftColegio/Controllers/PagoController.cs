using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using frontendED;
using frontendUtil;
using frontend_SoftColegio.Filters;
using System.Text;
using System.Web;
using System.Linq;

namespace frontend_SoftColegio.Controllers
{
    public class PagoController : Controller
    {
        // alumno
        [SecuritySession]
        public ActionResult pago()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.Gidusuario = idusuario;
            return View();
        }

        // admin
        [SecuritySession]
        public ActionResult pagoGestion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        public string CrearPassword(int longitud)
        {
            //string caracteres = "abcdefghjkmnopqrstuvwxyzABCDEFGHJKMNOPQRSTUVWXYZ1234567890";
            string caracteres = "abcdefghjkmnopqrstuvwxyz1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        //ACTIVO : actualiza la clave del usuario seleccionado
        [HttpPost]
        public async Task<JsonResult> ActualizarCursoClave(int widusuario, string wnuevaclave)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsActualizarUsuarioClave?wsidusuario=" + widusuario
                        + "&wsnuevaclave=" + wnuevaclave);

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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> ListarPago(string wcuenta, int widusuario, int widnivel, int widgrado
                    , int widcurso, Int16 wivigente, string wnombre, string wfechaini, string wfechafin
                    , int wimes, int wianio)
        {
            try
            {
                var objResultado = new object();
                List<edCurso> loenClase = new List<edCurso>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/pago/wsListarPago?wcuenta=" + wcuenta
                        + "&widusuario=" + widusuario + "&widnivel=" + widnivel + "&widgrado=" + widgrado
                        + "&widcurso=" + widcurso + "&wivigente=" + wivigente + "&wnombre=" + wnombre
                        + "&wfechaini=" + wfechaini + "&wfechafin=" + wfechafin + "&wimes=" + wimes + "&wianio=" + wianio);
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

        //ACTIVO : registra los pagos tanto como admin y como usuario
        [HttpPost]
        public async Task<JsonResult> RegistrarPago(int widusuario, int widnivel, int widgrado
                , int widcurso, string woperacion, int wtipopago, int wtipomoneda
                , string wdescripcion, int wdia, int wmes, int wanio
                , string whora, decimal wmonto, decimal wigv
                , IEnumerable<HttpPostedFileBase> FRutaImagenes
                , string wfinip, string wffinp, string wfr, string wff)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                Int16 estado = 1;

                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                foreach (var file in FRutaImagenes)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/Content/imgpago/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/Content/imgpago/" + sRandom + "." + sTipoImagen;
                        // sRuta es para la bd                        
                        file.SaveAs(sRutaLocal);
                        releaseUris.Add(sRutaServidor);
                    }
                }
                string valorimg1 = "/Content/imgpago/vacio.png";
                string valorimg2 = "/Content/imgpago/vacio.png";
                if (releaseUris.Count == 1)
                {
                    valorimg1 = releaseUris[0];
                    if (releaseUris.Count == 2)
                    {
                        valorimg2 = releaseUris[1];
                    }
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.
                    GetAsync("api/pago/wsRegistrarPago?widusuario=" + widusuario + "&widnivel=" + widnivel
                    + "&widgrado=" + widgrado + "&widcurso=" + widcurso + "&woperacion=" + woperacion
                    + "&wtipopago=" + wtipopago + "&wtipomoneda=" + wtipomoneda + "&wdescripcion=" + wdescripcion
                    + "&wdia=" + wdia + "&wmes=" + wmes + "&wanio=" + wanio
                    + "&whora=" + whora + "&wmonto=" + wmonto + "&wigv=" + wigv
                    + "&wimgr1=" + valorimg1 + "&wimgr2=" + valorimg2
                    + "&wfinip=" + wfinip + "&wffinp=" + wffinp + "&wfr=" + wfr + "&wff=" + wff);

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

        //ACTIVO : lista un valor para el modo usuario si faltan pocos tiempo al dia de pago
        [HttpPost]
        public async Task<JsonResult> NotificarPago(int widusuario, string wfechaacceso
                                                , string wfechavalidar)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsNotificarPago?widusuario=" + widusuario
                        + "&wfechaacceso=" + wfechaacceso + "&wfechavalidar=" + wfechavalidar);

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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> RptListarSumPago(int widnivel, int widgrado, int widcurso
                            , string wfechaini, string wfechafin, int wmes, int wanio)
        {
            try
            {
                var objResultado = new object();
                List<edCurso> loenClase = new List<edCurso>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/pago/wsRptListarSumaPagos?widnivel=" + widnivel
                        + "&widgrado=" + widgrado + "&widcurso=" + widcurso
                        + "&wfechaini=" + wfechaini + "&wfechafin=" + wfechafin
                        + "&wmes=" + wmes + "&wanio=" + wanio);
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

        //ACTIVO : lista los usuarios totales para el modo admin
        [HttpPost]
        public async Task<JsonResult> ListarUsuarioTotal(int widnivel, int widgrado, int widcurso)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsListarUsuarioTotal?widnivel=" + widnivel
                        + "&widgrado=" + widgrado + "&widcurso=" + widcurso);

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
