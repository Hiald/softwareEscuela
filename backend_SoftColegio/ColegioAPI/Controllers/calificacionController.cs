using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class calificacionController : ApiController
    {
        tdCalificacion itdCalificacion;

        [HttpGet]
        public string wsListarCalificacion(string widarchivo, int widusuario)
        {
            List<edCalificacion> wsenCalificacion = new List<edCalificacion>();
            try
            {
                itdCalificacion = new tdCalificacion();
                wsenCalificacion = itdCalificacion.tdListarCalificacion(widarchivo, widusuario);
                return JsonConvert.SerializeObject(wsenCalificacion);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}