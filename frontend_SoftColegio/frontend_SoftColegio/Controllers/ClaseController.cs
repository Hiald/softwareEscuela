﻿using Newtonsoft.Json;
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
    public class ClaseController : Controller
    {
        //CRUD para administrar
        public ActionResult claseGestion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        //public ActionResult Curso()
        //{
        //    int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
        //    ViewBag.GrolUsuario = irolusuario;
        //    //ViewBag.Lista = ...;
        //    return View();
        //}

        public ActionResult Clase()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        //ACTIVO : inserta la clase : admin
        [HttpPost]
        public async Task<JsonResult> InsertarClaseGestion(int idgrado, int isemana, string nombre, string descripcion
            , string rutaenlace, string rutavideo, int categoria, string imagen, string imagenruta
            , int orden)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsInsertarClase?wsidcurso=" + idgrado
                        + "&wssemana=" + isemana + "&wsnombre=" + nombre + "&wsdescripcion=" + descripcion
                        + "&wsrutaenlace=" + rutaenlace + "&wsrutavideo=" + rutavideo + "&wscategoria=" + categoria
                        + "&wsimagen=" + imagen + "&wsimagenruta=" + imagenruta + "&wsorden=" + orden
                        + "&wsestado=" + estado + "&wfecharegistro=" + wfechaRegistro);

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

        [HttpPost]
        public async Task<JsonResult> ActualizarClaseGestion(int widclase, int wsidgrado, string wsnombre, string wsdescripcion
                , string wsrutaenlace, string wsrutavideo, int wscategoria, string wsimagen, string wsimagenruta
                , int wsorden)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                Int16 wsestado = 1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsActualizarClase?widclase=" + widclase
                        + "&wsidgrado=" + wsidgrado + "&wsnombre=" + wsnombre + "&wsdescripcion=" + wsdescripcion
                        + "&wsrutaenlace=" + wsrutaenlace + "&wsrutavideo=" + wsrutavideo + "&wscategoria=" + wscategoria
                        + "&wsimagen=" + wsimagen + "&wsimagenruta=" + wsimagenruta
                        + "&wsorden=" + wsorden + "&wsestado=" + wsestado + "&fecharegistro=" + wfechaRegistro);

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

        [HttpPost]
        public async Task<JsonResult> EliminarClaseGestion(int widclase)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsEliminarClase?wsidclase=" + widclase);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1)
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

        //ACTIVO : lista las clases por cada curso : alumno, admin
        [HttpPost]
        public async Task<JsonResult> ListarClaseGeneral(int idcurso)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/clase/wsListarClaseCurso?widcurso=" + idcurso + "&wtipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);
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

        //ACTIVO : muestra la vista y obtiene los cursos por cada grado y nivel : alumno
        public async Task<ActionResult> Curso()
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int idgrado = UtlAuditoria.ObtenerIdGrado();
                int idnivel = UtlAuditoria.ObtenerIdNivel();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/clase/wsListarCurso?wsidgrado=" + idgrado + "&wsidnivel=" + idnivel + "&wstipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);

                    }
                }
                ViewBag.GrolUsuario = ItipoUsuario;
                ViewBag.Lista = loenClase;
                return View();
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

    }
}