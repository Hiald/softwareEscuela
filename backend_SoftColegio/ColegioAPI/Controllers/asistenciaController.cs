using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class asistenciaController : ApiController
    {
        tdAsistencia itdAsistencia;

        [HttpGet]
        public int wsInsertarAsistencia(int widclase, int widdocente,
                                        int widalumno, int widtipoasistencia, string wfechaingreso)
        {
            int iresultado = -4;
            try
            {
                itdAsistencia = new tdAsistencia();
                iresultado = itdAsistencia.tdInsertarAsistencia(widclase, widdocente, widalumno, widtipoasistencia, wfechaingreso);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarAsistenciaAlumno(int widalumno)
        {
            List<edAsistencia> wsenClase = new List<edAsistencia>();
            try
            {
                itdAsistencia = new tdAsistencia();
                wsenClase = itdAsistencia.tdListarAsistenciaAlumno(widalumno);
                return JsonConvert.SerializeObject(wsenClase);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        

    }
}