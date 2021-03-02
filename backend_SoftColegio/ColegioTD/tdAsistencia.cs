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
    public class tdAsistencia : td_aglobal
    {
        adAsistencia iadAsistencia;

        public int tdInsertarAsistencia(int tdidclase, int tdiddocente,
                                        int tdidalumno, int tdidtipoasistencia, string tdfechaingreso)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadAsistencia = new adAsistencia(con);
                        iRespuesta = iadAsistencia.adInsertarAsistencia(tdidclase, tdiddocente,
                                        tdidalumno, tdidtipoasistencia, tdfechaingreso);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edAsistencia> tdListarAsistenciaAlumno(int tdidalumno)
        {
            List<edAsistencia> renClase = new List<edAsistencia>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadAsistencia = new adAsistencia(con);
                        renClase = iadAsistencia.adListarAsistenciaAlumno(tdidalumno);
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
