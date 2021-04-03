using frontend_SoftColegio.Filters;
using frontendUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace frontend_SoftColegio.Controllers
{
    public class HorarioController : Controller
    {
        // GET: Horario
        [SecuritySession]
        //public ActionResult Index()
        //{
        //    int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
        //    int idGrado = UtlAuditoria.ObtenerIdGrado();
        //    int idNivel = UtlAuditoria.ObtenerIdNivel();
        //    ViewBag.GrolUsuario = irolusuario;
        //    ViewBag.GidNivel = idNivel;
        //    ViewBag.GidGrado = idGrado;
        //    return View();
        //}

        //ACTIVO : lista los cursos asignados: admin, docente
        //[HttpPost]
        //public async Task<JsonResult> Index()
        //{
        //    try
        //    {
        //        var objResultado = new object();
        //        int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
        //        int IidUsuario = UtlAuditoria.ObtenerIdUsuario();
        //        int idusuario = widusuario;
        //        if (ItipoUsuario != 1)
        //        {
        //            idusuario = IidUsuario;
        //        }
        //        List<edCurso> loenClase = new List<edCurso>();
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
        //            client.DefaultRequestHeaders.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage Reslistarusu = await client.GetAsync("api/curso/wsListarCurso?wsitipousuario=" + valoritipousuario
        //                + "&widusuario=" + idusuario + "&widnivel=" + idnivel
        //                + "&widgrado=" + idgrado + "&widcurso=" + idcurso);
        //            if (Reslistarusu.IsSuccessStatusCode)
        //            {
        //                var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
        //                loenClase = JsonConvert.DeserializeObject<List<edCurso>>(rwsapilu);
        //            }
        //        }

        //        objResultado = new
        //        {
        //            PageStart = 1,
        //            pageSize = 100,
        //            SearchText = string.Empty,
        //            ShowChildren = UtlConstantes.bValorTrue,
        //            iTotalRecords = loenClase.Count,
        //            iTotalDisplayRecords = 1,
        //            aaData = loenClase
        //        };
        //        return Json(objResultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
        //        return Json(ex);
        //    }
        //}


        [HttpPost]
        //public async Task<JsonResult> Index(int tiponota, int idnota, int isemana)
        //{
        //    try
        //    {
        //        var objResultado = new object();
        //        int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
        //        int IDUsuario = UtlAuditoria.ObtenerIdUsuario();
        //        int IvalorTipoUsuario = IDUsuario;
        //        if (ItipoUsuario == 1 || ItipoUsuario == 2)
        //        {
        //            IvalorTipoUsuario = 0;
        //        }
        //        List<edCalificacion> loenCalificacion = new List<edCalificacion>();
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
        //            client.DefaultRequestHeaders.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage ResWSApi = await client.
        //                GetAsync("api/Calificacion/wsListarCalificacion?widusuario="
        //                    + IvalorTipoUsuario + "&wtiponota=" + tiponota + "&wnota=" + idnota + "&wisemana=" + isemana);
        //            if (ResWSApi.IsSuccessStatusCode)
        //            {
        //                var rwsapilu = ResWSApi.Content.ReadAsAsync<string>().Result;
        //                loenCalificacion = JsonConvert.DeserializeObject<List<edCalificacion>>(rwsapilu);
        //            }
        //        }

        //        objResultado = new
        //        {
        //            PageStart = 1,
        //            pageSize = 100,
        //            SearchText = string.Empty,
        //            ShowChildren = UtlConstantes.bValorTrue,
        //            iTotalRecords = loenCalificacion.Count,
        //            iTotalDisplayRecords = 1,
        //            aaData = loenCalificacion
        //        };
        //        return Json(objResultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
        //        return Json(ex);
        //    }

        //}


        // GET: Horario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Horario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Horario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Horario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Horario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Horario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
