using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRV.ADO;
using SRV.Entidades;
using SRV.Utilidades;



namespace SRV.Negocio
{
    public class NAlo: NConexion
    {

        /// <summary>
        /// CONSULTA DE METODO
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public decimal SP_CREATE_FIFO_FTP(object Input)
        {

            try
            {
                //===========================================================
                // SE EJECUTA SP 
                //===========================================================
                decimal Retorno = (decimal)ServiceMotorObject(T_Conexiones.CONEXION_ALO, "SP_CREATE_FIFO_FTP", Input);


                return Retorno;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// CONSULTA DE METODO
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public decimal SP_CREATE_FIFO_FTP_BCP(object Input)
        {
            try
            {
                //===========================================================
                // SE EJECUTA SP 
                //===========================================================
                decimal Retorno = (decimal)ServiceMotorObject(T_Conexiones.CONEXION_ALO, "SP_CREATE_FIFO_FTP_BCP", Input);


                return Retorno;
            }
            catch
            {
                throw;
            }
        }


    }
}
