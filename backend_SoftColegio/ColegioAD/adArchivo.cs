﻿using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adArchivo : ad_aglobal
    {
        public adArchivo(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarArchivo(int adidgrado, int adidusuario, string adnombre, string addescripcion, string adrutaenlace, string adrutavideo, int adcategoria
                               , string adimagen, string adimagenruta, int adorden, short adestado, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_archivo", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 50).Value = adnombre;
                cmd.Parameters.Add("_enlace", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_tipoarchivo", MySqlDbType.Int32).Value = adrutaenlace;
                cmd.Parameters.Add("_puntajeminimo", MySqlDbType.Int32).Value = adrutavideo;
                cmd.Parameters.Add("_puntajemaximo", MySqlDbType.Int32).Value = adcategoria;
                cmd.Parameters.Add("_fechainicio", MySqlDbType.DateTime).Value = adimagen;
                cmd.Parameters.Add("_fechafin", MySqlDbType.DateTime).Value = adimagenruta;
                cmd.Parameters.Add("_imagen", MySqlDbType.Blob).Value = adorden;
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

        public edArchivo adObtenerArchivo(int adidclase)
        {
            try
            {
                edArchivo senUsuario = null;
                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_archivo", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_idarchivo = mdrd.GetOrdinal("idarchivo");
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_venlace = mdrd.GetOrdinal("v_enlace");
                            int pos_itipoarchivo = mdrd.GetOrdinal("i_tipo_archivo");
                            int pos_ipuntajeminimo = mdrd.GetOrdinal("i_puntaje_minimo");
                            int pos_ipuntajemaximo = mdrd.GetOrdinal("i_puntaje_maximo");
                            int pos_dfechainicio = mdrd.GetOrdinal("d_fecha_inicio");
                            int pos_dfechafin = mdrd.GetOrdinal("d_fecha_fin");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");

                            while (mdrd.Read())
                            {
                                senUsuario = new edArchivo();
                                senUsuario.idarchivo = (mdrd.IsDBNull(pos_idarchivo) ? 0 : mdrd.GetInt32(pos_idarchivo));
                                senUsuario.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                senUsuario.Senlace = (mdrd.IsDBNull(pos_venlace) ? "-" : mdrd.GetString(pos_venlace));
                                senUsuario.Itipoarchivo = (mdrd.IsDBNull(pos_itipoarchivo) ? 0 : mdrd.GetInt32(pos_itipoarchivo));
                                senUsuario.IPuntajeMinimo = (mdrd.IsDBNull(pos_ipuntajeminimo) ? 0 : mdrd.GetInt32(pos_ipuntajeminimo));
                                senUsuario.IpuntajeMaximo = (mdrd.IsDBNull(pos_ipuntajemaximo) ? 0 : mdrd.GetInt32(pos_ipuntajemaximo));
                                senUsuario.SfechaInicio = (mdrd.IsDBNull(pos_dfechainicio) ? "-" : mdrd.GetString(pos_dfechainicio));
                                senUsuario.SfechaFin = (mdrd.IsDBNull(pos_dfechafin) ? "-" : mdrd.GetString(pos_dfechafin));
                                senUsuario.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senUsuario.Bestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt16(pos_bestado));
                                senUsuario.SfechaRegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                            }
                        }
                    }
                    return senUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adInsertarArchivoDetalle(int adidarchivo, int adidusuario, string adimagen, int adnota, string adobservacion
                                        , string adenlace, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_archivo_detalle", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_imagen", MySqlDbType.VarChar, 50).Value = adimagen;
                cmd.Parameters.Add("_nota", MySqlDbType.VarChar, 500).Value = adnota;
                cmd.Parameters.Add("_observacion", MySqlDbType.Int32).Value = adobservacion;
                cmd.Parameters.Add("_enlace", MySqlDbType.Int32).Value = adenlace;
                cmd.Parameters.Add("_fecharegistro", MySqlDbType.Int32).Value = adfecharegistro;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edArchivo> adListarArchivoDetalle(string adidarchivo, int adidusuario)
        {
            try
            {
                List<edArchivo> loenusuario = new List<edArchivo>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_archivo_detalle", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edArchivo senUsuario = null;
                            int pos_idarchivo = mdrd.GetOrdinal("idarchivo");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_inota = mdrd.GetOrdinal("i_nota");
                            int pos_vobservacion = mdrd.GetOrdinal("v_observacion");
                            int pos_venlace = mdrd.GetOrdinal("v_enlace");

                            while (mdrd.Read())
                            {
                                senUsuario = new edArchivo();
                                senUsuario.idarchivo = (mdrd.IsDBNull(pos_idarchivo) ? 0 : mdrd.GetInt32(pos_idarchivo));
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senUsuario.Inota = (mdrd.IsDBNull(pos_inota) ? 0 : mdrd.GetInt32(pos_inota));
                                senUsuario.Sobservacion = (mdrd.IsDBNull(pos_vobservacion) ? "-" : mdrd.GetString(pos_vobservacion));
                                senUsuario.Senlace = (mdrd.IsDBNull(pos_venlace) ? "-" : mdrd.GetString(pos_venlace));

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