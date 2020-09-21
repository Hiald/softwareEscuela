using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace frontendUtil
{
    public class UtlAuditoria
    {
        private const string SESSION_IDALUMNO = "SESSION_IDALUMNO";
        private const string SESSION_SNOMBRE = "SESSION_SNOMBRE";
        private const string SESSION_SEMAIL = "SESSION_SEMAIL";
        private const string SESSION_IETAPAESCOLAR = "SESSION_IETAPAESCOLAR";
        private const string SESSION_IGRADO = "SESSION_IGRADO";
        private const string SESSION_ISECCION = "SESSION_ISECCION";
        private const string SESSION_ITIPODOC = "SESSION_ITIPODOC";
        private const string SESSION_SDOCUMENTO = "SESSION_SDOCUMENTO";
        private const string SESSION_ISUSCRIPCIONESTADO = "SESSION_ISUSCRIPCIONESTADO";
        private const string SESSION_IACTIVO = "SESSION_IACTIVO";
        private const string SESSION_IMATRICULADO = "SESSION_IMATRICULADO";

        #region "Obtiene Datos del Usuario"

        public static int ObtenerIdUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IDALUMNO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDALUMNO].ToString()));
        }
        public static string ObtenerNombre()
        {
            return ((HttpContext.Current.Session[SESSION_SNOMBRE] == null) ? "" : HttpContext.Current.Session[SESSION_SNOMBRE].ToString());
        }
        public static string ObtenerCorreo()
        {
            return ((HttpContext.Current.Session[SESSION_SEMAIL] == null) ? "-" : HttpContext.Current.Session[SESSION_SEMAIL].ToString());
        }
        public static int ObtenerEtapaEscolar()
        {
            return ((HttpContext.Current.Session[SESSION_IETAPAESCOLAR] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IETAPAESCOLAR].ToString()));
        }
        public static int ObtenerGrado()
        {
            return ((HttpContext.Current.Session[SESSION_IGRADO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IGRADO].ToString()));
        }
        public static int ObtenerSeccion()
        {
            return ((HttpContext.Current.Session[SESSION_ISECCION] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_ISECCION].ToString()));
        }
        public static int ObtenerTipoDoc()
        {
            return ((HttpContext.Current.Session[SESSION_ITIPODOC] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_ITIPODOC].ToString()));
        }
        public static string ObtenerDocumento()
        {
            return ((HttpContext.Current.Session[SESSION_SDOCUMENTO] == null) ? "-" : HttpContext.Current.Session[SESSION_SDOCUMENTO].ToString());
        }
        public static int ObtenerSuscripcionEstado()
        {
            return ((HttpContext.Current.Session[SESSION_ISUSCRIPCIONESTADO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_ISUSCRIPCIONESTADO].ToString()));
        }
        public static int ObtenerActivo()
        {
            return ((HttpContext.Current.Session[SESSION_IACTIVO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IACTIVO].ToString()));
        }
        public static int ObtenerMatriculado()
        {
            return ((HttpContext.Current.Session[SESSION_IMATRICULADO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IMATRICULADO].ToString()));
        }
        public static string ObtenerFechaSistema()
        {
            return DateTime.Now.ToShortDateString();
        }

        public static void SetSessionValues(Dictionary<string, string> DVariables)
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDALUMNO] = DVariables["IdAlumno"];
                HttpContext.Current.Session[SESSION_SNOMBRE] = DVariables["Snombre"];
                HttpContext.Current.Session[SESSION_SEMAIL] = DVariables["Semail"];
                HttpContext.Current.Session[SESSION_IETAPAESCOLAR] = DVariables["IEtapaEscolar"];
                HttpContext.Current.Session[SESSION_IGRADO] = DVariables["IGrado"];
                HttpContext.Current.Session[SESSION_ISECCION] = DVariables["ISeccion"];
                HttpContext.Current.Session[SESSION_ITIPODOC] = DVariables["ITipodoc"];
                HttpContext.Current.Session[SESSION_SDOCUMENTO] = DVariables["Sdocumento"];
                HttpContext.Current.Session[SESSION_ISUSCRIPCIONESTADO] = DVariables["Isuscripcionestado"];
                HttpContext.Current.Session[SESSION_IACTIVO] = DVariables["Iactivo"];
                HttpContext.Current.Session[SESSION_IMATRICULADO] = DVariables["Imatriculado"];
            }
            catch (ArgumentOutOfRangeException kfe)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", kfe.StackTrace.ToString(), kfe.Message.ToString());
                throw new ArgumentOutOfRangeException("Se requiere que se tenga todas las llaves :  idUsuario, sCodUsuario , sNombreCompleto, sNombres, sCorreo, sMenu", kfe);
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        #endregion

        #region "Obtiene la dirección IP del Usuario"

        /// <summary>
        /// Obtiene la dirección IP del cliente, desde donde está conectado al sistema
        /// </summary>
        /// <returns>Devuelve la dirección IP</returns>
        public static string ObtenerDireccionIP()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            //ADD IPLAN;
            if (ipAddress == "::1")
            {
                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        ipAddress = IPA.ToString();
                        break;
                    }
                }
            }

            return ipAddress;
        }

        #endregion

        #region "Obtiene la dirección MAC del Usuario"

        /// <summary>
        /// Obtiene la dirección MAC del cliente, desde donde está conectado el sistema
        /// </summary>
        /// <returns>Devuelve la dirección MAC</returns>
        public static string ObtenerDireccionMAC()
        {
            string strClientIP = ObtenerDireccionIP();
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (mac_dest);
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        #endregion

        public static bool ValidarSession()
        {
            try
            {
                if (HttpContext.Current.Session[SESSION_IDALUMNO] == null ||
                HttpContext.Current.Session[SESSION_IETAPAESCOLAR] == null ||
                HttpContext.Current.Session[SESSION_IGRADO] == null ||
                HttpContext.Current.Session[SESSION_SNOMBRE] == null ||
                HttpContext.Current.Session[SESSION_SEMAIL] == null ||
                HttpContext.Current.Session[SESSION_IMATRICULADO] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }

        }

        public static bool ValidarMenu()
        {
            //string sMenu = UtlAuditoria.ObtenerMenu();
            return true;
        }

        public static bool CerrarSession()
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDALUMNO] = null;
                HttpContext.Current.Session[SESSION_SNOMBRE] = null;
                HttpContext.Current.Session[SESSION_SEMAIL] = null;
                HttpContext.Current.Session[SESSION_IETAPAESCOLAR] = null;
                HttpContext.Current.Session[SESSION_IGRADO] = null;
                HttpContext.Current.Session[SESSION_ISECCION] = null;
                HttpContext.Current.Session[SESSION_ITIPODOC] = null;
                HttpContext.Current.Session[SESSION_SDOCUMENTO] = null;
                HttpContext.Current.Session[SESSION_ISUSCRIPCIONESTADO] = null;
                HttpContext.Current.Session[SESSION_IACTIVO] = null;
                HttpContext.Current.Session[SESSION_IMATRICULADO] = null;

                return true;
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }


        }
    }
}
