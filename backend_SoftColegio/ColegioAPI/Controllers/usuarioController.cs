using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class usuarioController : ApiController
    {
        tdUsuario itdUsuario;

        [HttpGet]
        public int wsInsertarCuenta(int widnivel, int widgrado, int widsede, string wnombres, string wamaterno, string wapaterno, string wgenero
                                    , string wcorreo, Int16 westado, string wfechaRegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(wfechaRegistro);
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdInsertarCuenta(widnivel, widgrado, widsede, wnombres, wamaterno, wapaterno, wgenero
                                                        , wcorreo, westado, wsfechaRegistro);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsInsertarUsuario(int widusuario, int wtipousuario, string wusuario, string wclave, string wtoken, Int16 westado, string wfechaRegistro)
        {
            int iresultado = -1;
            try
            {
                DateTime wsfechaRegistro = DateTime.Parse(wfechaRegistro);
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdInsertarUsuario(widusuario, wtipousuario, wusuario, wclave, wtoken, westado, wsfechaRegistro);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsObtenerAcceso(string wusuario, string wclave)
        {
            int iresultado = -1;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdObtenerAcceso(wusuario, wclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsObtenerUsuario(int wsidusuario)
        {
            edUsuario enUsuario = new edUsuario();
            try
            {

                itdUsuario = new tdUsuario();
                enUsuario = itdUsuario.tdObtenerUsuario(wsidusuario);
                return JsonConvert.SerializeObject(enUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(enUsuario);
            }
        }
    }
}