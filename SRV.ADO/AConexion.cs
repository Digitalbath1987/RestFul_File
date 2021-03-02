using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRV.Entidades;
using SRV.Utilidades;

namespace SRV.ADO
{
    public class AConexion
    {


        //===============================================================================
        //  DECLARACION DE VARIABLES PUBLICAS DE LA CLASE                              
        //===============================================================================
        public SqlConnection SPConexion;
        private bool SeEjecutaTransaccion = false;



        //===============================================================================
        //  DECLARACION DE VARIABLES PRIVADAS DE LA CLASE                              
        //===============================================================================
        private string CadenaConexion;
        public SqlTransaction Transaccion;

        int ErrorRaise = 0;
        int ID;
        string ErrorEvento;




        /// <summary>
        /// CONTRUCTOR
        /// </summary>
        /// <param name="TConexion"></param>
        public AConexion(T_Conexiones TConexion)
        {

            try
            {
                ErrorEvento = "";
                ErrorRaise = 0;
                switch (TConexion)
                {

                    case T_Conexiones.CONEXION_ALO:
                        this.CadenaConexion = new UConexionesADO().ConexionAlo();
                        break;
                    default:
                        throw new Exception("CONEXION ENVIADA NO SE ENCUENTRA IMPLEMENTADA EN SISTEMA");

                }

            }
            catch (Exception e)
            {
                throw new Exception("CONEXION NO PUDO SER IMPLEMENTADA  :" + e.Message);
            }
        }


        /// <summary>
        /// CONECCIONES CON TRANSACCIONES EN LAS PETICIONES
        /// </summary>
        /// <param name="Trans"></param>
        public void TransaccionBD(SqlTransaction Trans)
        {
            this.Transaccion = Trans;
            this.SeEjecutaTransaccion = true;
        }


        /// <summary>
        /// EVENTO INFORMATIVO DEL CONEXION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Conexion_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {

            try
            {

            }
            catch
            {
            }


        }

        /// <summary>
        /// EVENTO INFORMATIVO DE LA CONECCION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Conexion_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {


            //=================================================================
            // DECLARACION DE VARIABLES                                      ==
            //=================================================================
            string CadenaInfo = "";

            try
            {

                //=============================================================
                // EVENTO SOBRE CONEXION                                     ==
                //=============================================================
                foreach (SqlError Errores in e.Errors)
                {

                    CadenaInfo = "";
                    CadenaInfo = Errores.Message;
                    CadenaInfo = CadenaInfo.Replace("System.Data.SqlClient.SqlError:", "");


                    if (Errores.Number.ToString() != "0")
                    {
                        ErrorRaise = Convert.ToInt32(Errores.Number.ToString());
                        ErrorEvento = ErrorEvento + " " + CadenaInfo;
                        throw new Exception("ERRORES AL EJECUTAR EN ACCESO A DATOS  :" + CadenaInfo);

                    }
                    else
                    {
                        ErrorRaise = 0;
                    }


                }
            }
            catch
            {
                throw;

            }

        }

        /// <summary>
        /// CONECCIÓN A MOTOR DE BASE DE DATOS
        /// </summary>
        public void Conecta()
        {

            try
            {

                Random rnd = new Random();
                this.ID = rnd.Next(0, 10000);



                SPConexion = new SqlConnection(this.CadenaConexion);

                SPConexion.InfoMessage += new SqlInfoMessageEventHandler(Conexion_InfoMessage);
                SPConexion.StateChange += new StateChangeEventHandler(Conexion_StateChange);


                SPConexion.FireInfoMessageEventOnUserErrors = true;
                SPConexion.Open();


            }
            catch (SqlException SQLEx)
            {
                throw new Exception("ERRORES AL CONECTAR  :" + SQLEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception("ERRORES AL CONECTAR  :" + Ex.Message);
            }

        }



        /// <summary>
        /// RETORNA OBJETO DATASET
        /// </summary>
        /// <param name="Comando"></param>
        /// <returns></returns>
        public DataSet GetGataset(SqlCommand Comando)
        {



            try
            {
                //=============================================================
                // EJECUCION DE COMADO EN SISTEMA                            ==
                //=============================================================
                Comando.Connection = SPConexion;
                Comando.Transaction = this.Transaccion;
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = Comando;
                adapter.Fill(ds);


                //=============================================================
                // CUANDO UN ERROR PROVOCA LA CAIDA EN UN EVENTO ESTE NO     ==
                // VOLCA LOS DEMAS PROCEDMIENTOS                             ==
                //=============================================================
                if (ErrorRaise != 0)
                {
                    throw new Exception(ErrorEvento);
                }


                return ds;

            }
            catch (SqlException SQLEx)
            {

                throw new Exception("ERROR AL CONSULTAR : " + Comando.CommandText + " " + SQLEx.Message);

            }
            catch (Exception Ex)
            {
                throw new Exception("ERROR AL CONSULTAR : " + Comando.CommandText + " " + Ex.Message);

            }


        }


        /// <summary>
        /// RETORNA LISTAS GENERICAS ESPECIFICADAS EN LA CLASE ENTIDADES
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Comando"></param>
        /// <returns></returns>
        protected List<T> GetReturnLista<T>(SqlCommand Comando)
        {


            ErrorRaise = 0;



            //=============================================================
            // SE CREA LISTA QUE DEVOLVERA LA ENTIDAD REQUERIDA          ==
            //=============================================================
            List<T> Lista = new List<T>();



            //=============================================================
            // ASIGNACIONES A COMANO                                     ==
            //=============================================================
            Comando.Connection = SPConexion;
            Comando.Transaction = this.Transaccion;


            try
            {
                //=========================================================
                // EJECUCION DE COMADO EN SISTEMA                        ==
                //=========================================================
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = Comando;
                adapter.Fill(ds);


                //=========================================================
                // CUANDO UN ERROR PROVOCA LA CAIDA EN UN EVENTO ESTE NO ==
                // VOLCA LOS DEMAS PROCEDMIENTOS                         ==
                //=========================================================
                if (ErrorRaise != 0)
                {
                    throw new Exception(ErrorEvento);
                }


                Lista = ACreateEntity.DataTableToList<T>(ds.Tables[0]);





                return Lista;
            }
            catch (SqlException SQLEx)
            {

                throw new Exception("ERROR AL CONSULTAR : " + Comando.CommandText + " " + SQLEx.Message);

            }
            catch (Exception Ex)
            {
                throw new Exception("ERROR AL CONSULTAR : " + Comando.CommandText + " " + Ex.Message);

            }


        }

        /// <summary>
        /// DEVUELVE CONEXIONES RETURN STATUS
        /// </summary>
        /// <param name="Comando"></param>
        public void Ejecucion(SqlCommand Comando)
        {




            //===============================================================
            // ASIGNACIONES A COMANDO                                      ==
            //===============================================================
            Comando.Connection = SPConexion;
            Comando.Transaction = this.Transaccion;

            if (SeEjecutaTransaccion == true)
            {
                Comando.Transaction = this.Transaccion;
            }



            try
            {

                ErrorRaise = 0;


                //=========================================================
                // EJECUCION DE COMADO EN SISTEMA                        ==
                //=========================================================
                Comando.ExecuteNonQuery();


                //=========================================================
                //ERRORES POR RAISEERROR                                 ==
                //=========================================================
                if (ErrorRaise != 0)
                {
                    throw new Exception(ErrorEvento);
                }



            }
            catch (SqlException SQLEx)
            {

                throw new Exception("ERROR AL EJECUTAR : " + Comando.CommandText + " " + SQLEx.Message);

            }
            catch (Exception Ex)
            {
                throw new Exception("ERROR AL EJECUTAR : " + Comando.CommandText + " " + Ex.Message);

            }


        }


        /// <summary>
        /// CERRAR CONECCIÓN EN SISTEMA
        /// </summary>
        public void CerrarConexion()
        {


            //===============================================================
            // SI LA CONECCION ES NULLA SE DEBE SALIR                      ==
            //===============================================================
            if (this.SPConexion == null)
            {
                return;

            }

            //===============================================================
            // SI LA CONECCION ESTA ABIERTA LA CERRAREMOS                  ==
            //===============================================================
            if (this.SPConexion.State == ConnectionState.Open)
            {
                this.SPConexion.Close();
                SPConexion.Dispose();
            }
            //===============================================================
            // DE IGUAL FORMA DEJAMOS NULO EL OBJETO DE CONECCION          ==
            //===============================================================
            this.SPConexion = null;
        }


    }
}
