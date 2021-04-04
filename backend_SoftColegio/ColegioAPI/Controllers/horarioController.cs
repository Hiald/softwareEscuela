using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class horarioController : ApiController
    {
        tdHorario itdHorario;

        // GET: horario
       
        [HttpGet]
        public string wsListarHorario(int widnivel, int widgrado)
        {
            List<edHorario> wsenClase = new List<edHorario>();
            try
            {
                itdHorario = new tdHorario();
                wsenClase = itdHorario.tdListarHorario(widnivel, widgrado);
                return JsonConvert.SerializeObject(wsenClase);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }
    }
}