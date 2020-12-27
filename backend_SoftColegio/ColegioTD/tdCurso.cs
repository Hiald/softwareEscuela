using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdCurso : td_aglobal
    {
        adCurso iadCurso;

        public int tdInsertarCursoDocente(int tdidusuario, int tdidnivel, int tdidgrado, int tdidcurso, Int16 tdiestado)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadCurso = new adCurso(con);
                        iRespuesta = iadCurso.adInsertarCursoDocente(tdidusuario, tdidnivel, tdidgrado, tdidcurso, tdiestado);
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

        public List<edCurso> tdListarCurso(int tditipousuario)
        {
            List<edCurso> renClase = new List<edCurso>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadCurso = new adCurso(con);
                        renClase = iadCurso.adListarCurso(tditipousuario);
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
