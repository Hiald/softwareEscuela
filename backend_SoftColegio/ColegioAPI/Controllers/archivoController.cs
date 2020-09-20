﻿using System;
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
        public int wsInsertarArchivo(int widgrado, int widusuario, string wnombre, string wdescripcion, string wrutaenlace, string wrutavideo
                                , int wcategoria, string wimagen, string wimagenruta, int worden, short westado, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(wfecharegistro);
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivo(widgrado, widusuario, wnombre, wdescripcion, wrutaenlace, wrutavideo
                                                            , wcategoria, wimagen, wimagenruta, worden, westado, wsfechaRegistro);
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
            edArchivo enUsuario = new edArchivo();
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
        public int wsInsertarArchivoDetalle(int widarchivo, int widusuario, string wimagen, int wnota, string wobservacion
                                        , string wenlace, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(wfecharegistro);
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivoDetalle(widarchivo, widusuario, wimagen, wnota, wobservacion
                                                                , wenlace, wsfechaRegistro);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarArchivoDetalle(string widarchivo, int widusuario)
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


    }
}