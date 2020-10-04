﻿using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adUsuario : ad_aglobal
    {
        public adUsuario(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarCuenta(int adidnivel, int adidgrado, int adidsede, string adnombres, string adamaterno, string adapaterno, string adgenero
                                    ,string adcorreo,Int16 adestado, DateTime adfechaRegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_alumno", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_idsede", MySqlDbType.Int32).Value = adidsede;
                cmd.Parameters.Add("_nombres", MySqlDbType.VarChar, 50).Value = adnombres;
                cmd.Parameters.Add("_apellido_materno", MySqlDbType.VarChar, 50).Value = adamaterno;
                cmd.Parameters.Add("_apellido_paterno", MySqlDbType.VarChar, 50).Value = adapaterno;
                cmd.Parameters.Add("_genero", MySqlDbType.VarChar, 1).Value = adgenero;
                cmd.Parameters.Add("_correo", MySqlDbType.VarChar, 50).Value = adcorreo;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adestado;
                cmd.Parameters.Add("_fecha_registro", MySqlDbType.DateTime).Value = adfechaRegistro;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adInsertarUsuario(int adidusuario, int aditipousuario, string adusuario, string adclave, string adtoken, Int16 adestado, DateTime adfechaRegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_usuario", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_tipo_usuario", MySqlDbType.Int32).Value = aditipousuario;
                cmd.Parameters.Add("_usuario", MySqlDbType.VarChar, 45).Value = adusuario;
                cmd.Parameters.Add("_clave", MySqlDbType.VarChar, 45).Value = adclave;
                cmd.Parameters.Add("_token", MySqlDbType.VarChar, 300).Value = adtoken;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adestado;
                cmd.Parameters.Add("_fecha_registro", MySqlDbType.DateTime).Value = adfechaRegistro;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adObtenerAcceso(string adusuario, string adclave)
        {
            try
            {
                int result = -1;
                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_acceso", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_usuario", MySqlDbType.VarChar, 45).Value = adusuario;
                    cmd.Parameters.Add("_clave", MySqlDbType.VarChar, 45).Value = adclave;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_result = mdrd.GetOrdinal("_result");

                            while (mdrd.Read())
                            {
                                result = (mdrd.IsDBNull(pos_result) ? 0 : mdrd.GetInt16(pos_result));

                            }
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public edUsuario adObtenerUsuario(int adidusuario)
        {
            try
            {
                edUsuario senUsuario = null;
                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_usuario", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_idgrado = mdrd.GetOrdinal("idgrado");
                            int pos_idsede = mdrd.GetOrdinal("idsede");
                            int pos_idseccion = mdrd.GetOrdinal("idseccion");
                            int pos_nombres = mdrd.GetOrdinal("v_nombres");
                            int pos_apellidopaterno = mdrd.GetOrdinal("v_apellido_paterno");
                            int pos_apellidomaterno = mdrd.GetOrdinal("v_apellido_materno");
                            int pos_correo = mdrd.GetOrdinal("v_correo");

                            while (mdrd.Read())
                            {
                                senUsuario = new edUsuario();
                                senUsuario.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                senUsuario.idgrado = (mdrd.IsDBNull(pos_idgrado) ? 0 : mdrd.GetInt32(pos_idgrado));
                                senUsuario.idsede = (mdrd.IsDBNull(pos_idsede) ? 0 : mdrd.GetInt32(pos_idsede));
                                senUsuario.idseccion = (mdrd.IsDBNull(pos_idseccion) ? 0 : mdrd.GetInt32(pos_idseccion));
                                senUsuario.Snombres = (mdrd.IsDBNull(pos_nombres) ? "-" : mdrd.GetString(pos_nombres));
                                senUsuario.SApellidoPaterno = (mdrd.IsDBNull(pos_apellidopaterno) ? "-" : mdrd.GetString(pos_apellidopaterno));
                                senUsuario.SApellidoMaterno = (mdrd.IsDBNull(pos_apellidomaterno) ? "-" : mdrd.GetString(pos_apellidomaterno));
                                senUsuario.Scorreo = (mdrd.IsDBNull(pos_correo) ? "-" : mdrd.GetString(pos_correo));
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

        //falta completar en la base de datos
        public int adInsertarSesion(int adidalumno, string addireccionip, string addireccionmac, int adtipoconexion)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("spalumno_registro", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@_idalumno", MySqlDbType.Int32).Value = adidalumno;
                cmd.Parameters.Add("@_direccionip", MySqlDbType.VarChar, 150).Value = addireccionip;
                cmd.Parameters.Add("@_direccionmac", MySqlDbType.VarChar, 150).Value = addireccionmac;
                cmd.Parameters.Add("@_tipoconexion", MySqlDbType.Int32).Value = adtipoconexion;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

    }
}
