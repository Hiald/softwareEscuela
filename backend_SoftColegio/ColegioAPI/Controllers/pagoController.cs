using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class pagoController : ApiController
    {
        tdPago itdPago;

        [HttpGet]
        public int wsActualizarUsuarioClave(int wsidusuario, string wsnuevaclave)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdActualizarUsuarioClave(wsidusuario, wsnuevaclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarPago(string wcuenta, int widusuario, int widnivel, int widgrado
                    , int widcurso, Int16 wivigente, string wnombre, string wfechaini, string wfechafin
                    , int wimes, int wianio)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdListarPago(wcuenta, widusuario, widnivel, widgrado
                    , widcurso, wivigente, wnombre, wfechaini, wfechafin, wimes, wianio);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsRegistrarPago(int widusuario, int widnivel, int widgrado, int widcurso, string woperacion
                , int wtipopago, int wtipomoneda, string wdescripcion, int wdia, int wmes, int wanio
                , string whora, decimal wmonto, decimal wigv, string wimgr1, string wimgr2
                , string wfinip, string wffinp, string wfr, string wff)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdRegistrarPago(widusuario, widnivel, widgrado, widcurso, woperacion
                , wtipopago, wtipomoneda, wdescripcion, wdia, wmes, wanio
                , whora, wmonto, wigv, wimgr1, wimgr2, wfinip, wffinp, wfr, wff);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsNotificarPago(int widusuario, string wfechaacceso, string wfechavalidar)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdNotificarPago(widusuario, wfechaacceso, wfechavalidar);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsRptListarSumaPagos(int widnivel, int widgrado, int widcurso
                            , string wfechaini, string wfechafin, int wmes, int wanio)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdRptListarSumaPagos(widnivel, widgrado, widcurso
                                                    , wfechaini, wfechafin, wmes, wanio);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsListarUsuarioTotal(int widnivel, int widgrado, int widcurso)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdListarUsuarioTotal(widnivel, widgrado, widcurso);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

    }
}