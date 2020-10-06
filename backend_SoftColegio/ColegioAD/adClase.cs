using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adClase : ad_aglobal
    {
        public adClase(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarClase(int adidgrado, string adnombre, string addescripcion, string adrutaenlace, string adrutavideo, int adcategoria
                                , string adimagen, string adimagenruta, int adorden, Int16 adestado, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("_descripcion", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_rutaenlace", MySqlDbType.VarChar, 500).Value = adrutaenlace;
                cmd.Parameters.Add("_rutavideo", MySqlDbType.VarChar, 500).Value = adrutavideo;
                cmd.Parameters.Add("_categoria", MySqlDbType.Int32).Value = adcategoria;
                cmd.Parameters.Add("_imagen", MySqlDbType.Blob).Value = adimagen;
                cmd.Parameters.Add("_imagenruta", MySqlDbType.VarChar, 500).Value = adimagenruta;
                cmd.Parameters.Add("_orden", MySqlDbType.Int32).Value = adorden;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adestado;
                cmd.Parameters.Add("_fecharegistro", MySqlDbType.DateTime).Value = adfecharegistro;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adActualizarClase(int adidclase, int adidgrado, string adnombre, string addescripcion, string adrutaenlace, string adrutavideo, int adcategoria
                              , string adimagen, string adimagenruta, int adorden, Int16 adestado, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;
                cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("_descripcion", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_rutaenlace", MySqlDbType.VarChar, 500).Value = adrutaenlace;
                cmd.Parameters.Add("_rutavideo", MySqlDbType.VarChar, 500).Value = adrutavideo;
                cmd.Parameters.Add("_categoria", MySqlDbType.Int32).Value = adcategoria;
                cmd.Parameters.Add("_imagen", MySqlDbType.Blob).Value = adimagen;
                cmd.Parameters.Add("_imagenruta", MySqlDbType.VarChar, 500).Value = adimagenruta;
                cmd.Parameters.Add("_orden", MySqlDbType.Int32).Value = adorden;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adestado;
                cmd.Parameters.Add("_fecharegistro", MySqlDbType.DateTime).Value = adfecharegistro;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adEliminarClase(int adidclase)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_eliminar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edClase> adListarClaseCurso(int adidcurso, int adtipousuario)
        {
            try
            {
                List<edClase> slClase = new List<edClase>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_clase_curso", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                    cmd.Parameters.Add("_tipo_usuario", MySqlDbType.Int32).Value = adtipousuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edClase sClase = null;
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");
                            int pos_vnombrecurso = mdrd.GetOrdinal("nombrecurso");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_vrutaenlace = mdrd.GetOrdinal("v_ruta_enlace");
                            int pos_vrutavideo = mdrd.GetOrdinal("v_ruta_video");
                            int pos_icategoria = mdrd.GetOrdinal("i_categoria");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");

                            while (mdrd.Read())
                            {
                                sClase = new edClase();
                                sClase.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                sClase.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                sClase.Snombre = (mdrd.IsDBNull(pos_vnombrecurso) ? "-" : mdrd.GetString(pos_vnombrecurso));
                                sClase.Snombrecurso = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                sClase.Sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                sClase.Srutaenlace = (mdrd.IsDBNull(pos_vrutaenlace) ? "-" : mdrd.GetString(pos_vrutaenlace));
                                sClase.SrutaVideo = (mdrd.IsDBNull(pos_vrutavideo) ? "-" : mdrd.GetString(pos_vrutavideo));
                                sClase.Icategoria = (mdrd.IsDBNull(pos_icategoria) ? 0 : mdrd.GetInt32(pos_icategoria));
                                sClase.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                sClase.SimagenRuta = (mdrd.IsDBNull(pos_vimagenruta) ? "-" : mdrd.GetString(pos_vimagenruta));
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
