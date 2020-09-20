using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adCalificacion: ad_aglobal
    {
        public adCalificacion(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public List<edCalificacion> adListarCalificacion(string adidarchivo, int adidusuario)
        {
            try
            {
                List<edCalificacion> loenusuario = new List<edCalificacion>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_calificacion", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidarchivo;
                    cmd.Parameters.Add("_tiponota", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_idnota", MySqlDbType.Int32).Value = adidusuario;
                    
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCalificacion senUsuario = null;
                            int pos_idcalificacion = mdrd.GetOrdinal("idcalificacion");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_itiponota = mdrd.GetOrdinal("i_tipo_nota");
                            int pos_idnota = mdrd.GetOrdinal("idnota");

                            while (mdrd.Read())
                            {
                                senUsuario = new edCalificacion();
                                senUsuario.idcalificacion = (mdrd.IsDBNull(pos_idcalificacion) ? 0 : mdrd.GetInt32(pos_idcalificacion));
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.Itiponota = (mdrd.IsDBNull(pos_itiponota) ? 0 : mdrd.GetInt32(pos_itiponota));
                                senUsuario.idnota = (mdrd.IsDBNull(pos_idnota) ? 0 : mdrd.GetInt32(pos_idnota));

                                loenusuario.Add(senUsuario);
                            }
                        }
                    }
                    return loenusuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

    }
}
