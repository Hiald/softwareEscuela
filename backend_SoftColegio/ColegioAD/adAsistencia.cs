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
    public class adAsistencia : ad_aglobal
    {

        public adAsistencia(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarAsistencia(int idclase, int iddocente, int idalumno, int idtipoasistencia,string fechaingreso)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_asistencia", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = idclase;
                cmd.Parameters.Add("_iddocente", MySqlDbType.Int32).Value = iddocente;
                cmd.Parameters.Add("_idalumno", MySqlDbType.Int32).Value = idalumno;
                cmd.Parameters.Add("_idtipoasistencia", MySqlDbType.Int32).Value = idtipoasistencia;
                cmd.Parameters.Add("_dt_fechaingreso", MySqlDbType.VarChar,10).Value = fechaingreso;
                cmd.Parameters.Add("_b_estado", MySqlDbType.Bit).Value = 1;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edAsistencia> adListarAsistenciaAlumno(int idalumno)
        {
            try
            {
                List<edAsistencia> slClase = new List<edAsistencia>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_asistencia_alumno", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idalumno", MySqlDbType.Int32).Value = idalumno;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edAsistencia sClase = null;
                            int pos_vnombreclase = mdrd.GetOrdinal("NOMBRE_CLASE");
                            int pos_vnombrecurso = mdrd.GetOrdinal("NOMBRE_CURSO");
                            int pos_vnombredocente = mdrd.GetOrdinal("APELLIDO_NOMBRES");
                            int pos_idtipoasistencia = mdrd.GetOrdinal("IDTIPOASISTENCIA");
                            int pos_sfechaingreso = mdrd.GetOrdinal("FECHAINGRESO");

                            while (mdrd.Read())
                            {
                                sClase = new edAsistencia();
                                sClase.Snombreclase = (mdrd.IsDBNull(pos_vnombreclase) ? "-" : mdrd.GetString(pos_vnombreclase));
                                sClase.Snombrecurso = (mdrd.IsDBNull(pos_vnombrecurso) ? "-" : mdrd.GetString(pos_vnombrecurso));
                                sClase.Snombredocente = (mdrd.IsDBNull(pos_vnombredocente) ? "-" : mdrd.GetString(pos_vnombredocente));
                                sClase.idtipoAsistencia = (mdrd.IsDBNull(pos_idtipoasistencia) ? 0 : mdrd.GetInt32(pos_idtipoasistencia));
                                sClase.SfechaIngreso = (mdrd.IsDBNull(pos_sfechaingreso) ? "-" : mdrd.GetString(pos_sfechaingreso));
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
