using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRV.Entidades;

namespace SRV.ADO
{
    public class AAlo : AConexion
    {


        /// <summary>
        /// CONTRUCTOR 
        /// </summary>
        /// <param name="t_Conexiones"></param>
        public AAlo(T_Conexiones t_Conexiones)
            : base(t_Conexiones) { }



        /// <summary>
        /// METODO SP
        /// </summary>
        /// <param name="Parametros"></param>
        /// <returns></returns>
        public decimal SP_CREATE_FIFO_FTP(iSP_CREATE_FIFO_FTP Parametros)
        {
            using (SqlCommand Comando = new SqlCommand())
            {
                try
                {
                    decimal Retorno = 0m;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Comando.CommandText = "SP_CREATE_FIFO_FTP";

                    Comando.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                    Comando.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                    Comando.Parameters["RETURN_VALUE"].Size = 4;

                    Comando.Parameters.Add(new SqlParameter("@TOKEN", SqlDbType.VarChar));
                    Comando.Parameters["@TOKEN"].Value = Parametros.TOKEN;
                    Comando.Parameters["@TOKEN"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@TOKEN"].Size = 50;

                    Comando.Parameters.Add(new SqlParameter("@SERVIDOR", SqlDbType.VarChar));
                    Comando.Parameters["@SERVIDOR"].Value = Parametros.SERVIDOR;
                    Comando.Parameters["@SERVIDOR"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@SERVIDOR"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@USUARIO", SqlDbType.VarChar));
                    Comando.Parameters["@USUARIO"].Value = Parametros.USUARIO;
                    Comando.Parameters["@USUARIO"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@USUARIO"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@PASSWORD", SqlDbType.VarChar));
                    Comando.Parameters["@PASSWORD"].Value = Parametros.PASSWORD;
                    Comando.Parameters["@PASSWORD"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@PASSWORD"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@KEY_SSH", SqlDbType.VarChar));
                    Comando.Parameters["@KEY_SSH"].Value = Parametros.KEY_SSH;
                    Comando.Parameters["@KEY_SSH"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@KEY_SSH"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@RUTA", SqlDbType.VarChar));
                    Comando.Parameters["@RUTA"].Value = Parametros.RUTA;
                    Comando.Parameters["@RUTA"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@RUTA"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@FILE_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@FILE_FTP"].Value = Parametros.FILE_FTP;
                    Comando.Parameters["@FILE_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@FILE_FTP"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@ASUNTO", SqlDbType.VarChar));
                    Comando.Parameters["@ASUNTO"].Value = Parametros.ASUNTO;
                    Comando.Parameters["@ASUNTO"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@ASUNTO"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@CORREOS", SqlDbType.VarChar));
                    Comando.Parameters["@CORREOS"].Value = Parametros.CORREOS;
                    Comando.Parameters["@CORREOS"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@CORREOS"].Size = 1000;

                    Comando.Parameters.Add(new SqlParameter("@DELIMITADOR_CORREO", SqlDbType.Char));
                    Comando.Parameters["@DELIMITADOR_CORREO"].Value = Parametros.DELIMITADOR_CORREO;
                    Comando.Parameters["@DELIMITADOR_CORREO"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@DELIMITADOR_CORREO"].Size = 1;


                    //===========================================================
                    //EJECUCION
                    //===========================================================
                    Ejecucion(Comando);

                    Retorno = Decimal.Parse(Comando.Parameters["RETURN_VALUE"].Value.ToString());
                    return Retorno;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// METODO SP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parametros"></param>
        /// <returns></returns>
        public decimal SP_CREATE_FIFO_FTP_BCP(iSP_CREATE_FIFO_FTP_BCP Parametros)
        {
            using (SqlCommand Comando = new SqlCommand())
            {
                try
                {
                    decimal Retorno = 0m;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Comando.CommandText = "SP_CREATE_FIFO_FTP_BCP";

                    Comando.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                    Comando.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                    Comando.Parameters["RETURN_VALUE"].Size = 4;

                    Comando.Parameters.Add(new SqlParameter("@SERVIDOR_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@SERVIDOR_FTP"].Value = Parametros.SERVIDOR_FTP;
                    Comando.Parameters["@SERVIDOR_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@SERVIDOR_FTP"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@USUARIO_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@USUARIO_FTP"].Value = Parametros.USUARIO_FTP;
                    Comando.Parameters["@USUARIO_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@USUARIO_FTP"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@PASSWORD_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@PASSWORD_FTP"].Value = Parametros.PASSWORD_FTP;
                    Comando.Parameters["@PASSWORD_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@PASSWORD_FTP"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@KEY_SSH_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@KEY_SSH_FTP"].Value = Parametros.KEY_SSH_FTP;
                    Comando.Parameters["@KEY_SSH_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@KEY_SSH_FTP"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@RUTA", SqlDbType.VarChar));
                    Comando.Parameters["@RUTA"].Value = Parametros.RUTA;
                    Comando.Parameters["@RUTA"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@RUTA"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@FILE_FTP", SqlDbType.VarChar));
                    Comando.Parameters["@FILE_FTP"].Value = Parametros.FILE_FTP;
                    Comando.Parameters["@FILE_FTP"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@FILE_FTP"].Size = 255;

                    Comando.Parameters.Add(new SqlParameter("@SERVIDOR_DB", SqlDbType.VarChar));
                    Comando.Parameters["@SERVIDOR_DB"].Value = Parametros.SERVIDOR_DB;
                    Comando.Parameters["@SERVIDOR_DB"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@SERVIDOR_DB"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@USUARIO_DB", SqlDbType.VarChar));
                    Comando.Parameters["@USUARIO_DB"].Value = Parametros.USUARIO_DB;
                    Comando.Parameters["@USUARIO_DB"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@USUARIO_DB"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@PASSWORD_DB", SqlDbType.VarChar));
                    Comando.Parameters["@PASSWORD_DB"].Value = Parametros.PASSWORD_DB;
                    Comando.Parameters["@PASSWORD_DB"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@PASSWORD_DB"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@BASE_DATOS", SqlDbType.VarChar));
                    Comando.Parameters["@BASE_DATOS"].Value = Parametros.BASE_DATOS;
                    Comando.Parameters["@BASE_DATOS"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@BASE_DATOS"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@TABLA", SqlDbType.VarChar));
                    Comando.Parameters["@TABLA"].Value = Parametros.TABLA;
                    Comando.Parameters["@TABLA"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@TABLA"].Size = 100;

                    Comando.Parameters.Add(new SqlParameter("@DELIMITADOR", SqlDbType.Char));
                    Comando.Parameters["@DELIMITADOR"].Value = Parametros.DELIMITADOR;
                    Comando.Parameters["@DELIMITADOR"].Direction = ParameterDirection.Input;
                    Comando.Parameters["@DELIMITADOR"].Size = 1;

                    //===========================================================
                    //EJECUCION
                    //===========================================================
                    Ejecucion(Comando);

                    Retorno = Decimal.Parse(Comando.Parameters["RETURN_VALUE"].Value.ToString());
                    return Retorno;
                }
                catch
                {
                    throw;
                }
            }
        }



    }
}
