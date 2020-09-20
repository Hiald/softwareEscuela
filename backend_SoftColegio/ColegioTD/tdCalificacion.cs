using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ColegioTD
{
    public class tdCalificacion: td_aglobal
    {
        adCalificacion iadCalificacion;

        public List<edCalificacion> tdListarCalificacion(string tdidarchivo, int tdidusuario)
        {
            List<edCalificacion> renUsuario = new List<edCalificacion>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadCalificacion = new adCalificacion(con);
                        renUsuario = iadCalificacion.adListarCalificacion(tdidarchivo, tdidusuario);
                        scope.Commit();
                    }
                }

                return (renUsuario);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

    }
}
