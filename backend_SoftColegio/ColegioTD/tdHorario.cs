using ColegioAD;
using ColegioED;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTD
{
    public class tdHorario: td_aglobal
    {
        adHorario iadHorario;

        public List<edHorario> tdListarHorario(int tdidnivel, int tdidgrado)
        {
            List<edHorario> renClase = new List<edHorario>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadHorario = new adHorario(con);
                        renClase = iadHorario.adListarHorario(tdidnivel, tdidgrado);
                        scope.Commit();
                    }
                }
                return (renClase);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }
    }
}
