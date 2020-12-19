using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdArchivo : td_aglobal
    {
        adArchivo iadArchivo;

        public int tdInsertarArchivo(int tdidgrado, int tdidusuario, string tdnombre, string tddescripcion, string tdrutaenlace, string tdrutavideo
                                , int tdcategoria, string tdimagen, string tdimagenruta, int tdorden, Int16 tdestado, DateTime tdfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adInsertarArchivo(tdidgrado, tdidusuario, tdnombre, tddescripcion, tdrutaenlace, tdrutavideo
                                                                    , tdcategoria, tdimagen, tdimagenruta, tdorden, tdestado, tdfecharegistro);
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

        public List<edArchivo> tdObtenerArchivo(int tdidclase)
        {

            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adObtenerArchivo(tdidclase);
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

        public int tdActualizarArchivoDetalle(int tdidarchivodetalle, int tdnota, string tdobservacion, int tdidusuario
                                        , int tdtiponota, Int16 tdestado, DateTime tdfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adActualizarArchivoDetalle(tdidarchivodetalle, tdnota, tdobservacion, tdidusuario
                                                                            , tdtiponota, tdestado, tdfecharegistro);
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

        public int tdInsertarArchivoDetalle(int tdidarchivo, int tdidusuario, string tdimagen, int tdnota, string tdobservacion
                                        , string tdenlace, DateTime tdfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adInsertarArchivoDetalle(tdidarchivo, tdidusuario, tdimagen, tdnota, tdobservacion
                                                                        , tdenlace, tdfecharegistro);
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

        public List<edArchivo> tdListarArchivoDetalle(int tdidarchivo, int tdidusuario)
        {
            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adListarArchivoDetalle(tdidarchivo, tdidusuario);
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

        public List<edArchivo> tdListarArchivo(int tdidclase)
        {
            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adListarArchivo(tdidclase);
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
