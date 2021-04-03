using ColegioED;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioAD
{
    public class adHorario : ad_aglobal
    {
        public adHorario(MySqlConnection cn)
        {
            cnMysql = cn;
        }
        public List<edHorario> adListarHorario(int idnivel, int idgrado)
        {
            try
            {
                List<edHorario> slClase = new List<edHorario>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_horario", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = idnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = idgrado;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edHorario sClase = null;
                            int pos_Nro = mdrd.GetOrdinal("Nro");
	                        int pos_horario = mdrd.GetOrdinal("horario");
                            int pos_lunes = mdrd.GetOrdinal("lunes");
                            int pos_martes = mdrd.GetOrdinal("martes");
                            int pos_miercoles = mdrd.GetOrdinal("miercoles");
                            int pos_jueves = mdrd.GetOrdinal("jueves");
                            int pos_viernes = mdrd.GetOrdinal("viernes");

                            while (mdrd.Read())
                            {
                                sClase = new edHorario();
                                sClase.Nro = (mdrd.IsDBNull(pos_Nro) ? 0 : mdrd.GetInt32(pos_Nro));
                                sClase.horario = (mdrd.IsDBNull(pos_horario) ? "-" : mdrd.GetString(pos_horario));
                                sClase.lunes = (mdrd.IsDBNull(pos_lunes) ? "-" : mdrd.GetString(pos_lunes));
                                sClase.martes = (mdrd.IsDBNull(pos_martes) ? "-" : mdrd.GetString(pos_martes));
                                sClase.miercoles = (mdrd.IsDBNull(pos_miercoles) ? "-" : mdrd.GetString(pos_miercoles));
                                sClase.jueves = (mdrd.IsDBNull(pos_jueves) ? "-" : mdrd.GetString(pos_jueves));
                                sClase.viernes = (mdrd.IsDBNull(pos_viernes) ? "-" : mdrd.GetString(pos_viernes));
                                slClase.Add(sClase);
                            }
                        }
                    }
                    return slClase;
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
