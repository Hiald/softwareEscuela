using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class claseController : ApiController
    {
        tdClase itdClase;

        [HttpGet]
        public int wsInsertarClase(int widgrado, string wnombre, string wdescripcion, string wrutaenlace, string wrutavideo, int wcategoria
                                , string wimagen, string wimagenruta, int worden, Int16 westado, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(wfecharegistro);
                itdClase = new tdClase();
                iresultado = itdClase.tdInsertarClase(widgrado, wnombre, wdescripcion, wrutaenlace, wrutavideo, wcategoria
                                , wimagen, wimagenruta, worden, westado, wsfechaRegistro);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsActualizarClase(int widclase, int wsidgrado, string wsnombre, string wsdescripcion, string wsrutaenlace, string wsrutavideo
                                    , int wscategoria, string wsimagen, string wsimagenruta, int wsorden, Int16 wsestado, string fecharegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(fecharegistro);
                itdClase = new tdClase();
                iresultado = itdClase.tdActualizarClase(widclase, wsidgrado, wsnombre, wsdescripcion, wsrutaenlace, wsrutavideo, wscategoria
                                , wsimagen, wsimagenruta, wsorden, wsestado, wsfechaRegistro);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsEliminarClase(int wsidclase)
        {
            int iresultado = -1;
            try
            {
                itdClase = new tdClase();
                iresultado = itdClase.tdEliminarClase(wsidclase);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarClaseCurso(int widcurso, int wtipousuario)
        {
            List<edClase> wsenClase = new List<edClase>();
            try
            {
                itdClase = new tdClase();
                wsenClase = itdClase.tdListarClaseCurso(widcurso, wtipousuario);
                return JsonConvert.SerializeObject(wsenClase);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}