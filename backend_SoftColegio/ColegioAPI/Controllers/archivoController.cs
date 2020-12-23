using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class archivoController : ApiController
    {
        tdArchivo itdArchivo;

        [HttpGet]
        public int wsInsertarArchivo(int widclase, int widusuario, string wnombre
                               , string wrutaenlace, int wtipoarchivo, string wfechainicio
                               , string wfechafin, string wrutavideo)
        {
            int iresultado = -1;
            try
            {

                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivo(widclase, widusuario, wnombre
                                                       , wrutaenlace, wtipoarchivo, wfechainicio
                                                       , wfechafin, wrutavideo);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsObtenerArchivo(int widclase)
        {
            List<edArchivo> enUsuario = new List<edArchivo>();
            try
            {

                itdArchivo = new tdArchivo();
                enUsuario = itdArchivo.tdObtenerArchivo(widclase);
                return JsonConvert.SerializeObject(enUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(enUsuario);
            }
        }

        [HttpGet]
        public int wsActualizarArchivoDetalle(int widarchivodetalle, int wnota, string wobservacion, int widusuario
                                        , int wtiponota, Int16 westado, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {

                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdActualizarArchivoDetalle(widarchivodetalle, wnota, wobservacion, widusuario
                                                                    , wtiponota, westado, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsInsertarArchivoDetalle(int widarchivo, int widusuario, string wimagen, int wnota, string wobservacion
                                        , string wenlace, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivoDetalle(widarchivo, widusuario, wimagen, wnota, wobservacion
                                                                , wenlace, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarArchivoDetalle(int widarchivo, int widusuario)
        {
            List<edArchivo> wsenUsuario = new List<edArchivo>();
            try
            {
                itdArchivo = new tdArchivo();
                wsenUsuario = itdArchivo.tdListarArchivoDetalle(widarchivo, widusuario);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsListarArchivoGeneral(int wsidclasela)
        {
            List<edArchivo> wsenUsuario = new List<edArchivo>();
            try
            {
                itdArchivo = new tdArchivo();
                wsenUsuario = itdArchivo.tdListarArchivo(wsidclasela);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}