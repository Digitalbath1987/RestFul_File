using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SRV.ADO;

namespace SRV.Negocio
{
    public class NConexion : IDisposable
    {


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

       

        /// <summary>
        /// RETORNO DE LISTA GENERICA
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        protected List<T> ServiceMotor<T>(object Contructor, string Metodo, object parametros)
        {


            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                List<T> Lista = new List<T>();
                object[] argsMetodo = new object[] { parametros };
                object[] argsClase = new object[] { Contructor };
                object Datos = new object();


                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                System.Reflection.MethodInfo Generico = MetodoClase.MakeGenericMethod(typeof(T));
                Datos = Generico.Invoke(Objeto, argsMetodo);
                Lista = (List<T>)Datos;


                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);




                return Lista;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }

        /// <summary>
        /// RETORNO DE LISTA GENERICA
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <returns></returns>
        protected List<T> ServiceMotor<T>(object Contructor, string Metodo)
        {


            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {



                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                List<T> Lista = new List<T>();
                object[] argsClase = new object[] { Contructor };
                object Datos = new object();



                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                System.Reflection.MethodInfo Generico = MetodoClase.MakeGenericMethod(typeof(T));
                Datos = Generico.Invoke(Objeto, null);
                Lista = (List<T>)Datos;



                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);



                return Lista;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }

    
        /// <summary>
        /// RETORNA DATASET
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        protected DataSet ServiceMotor(object Contructor, string Metodo, object parametros)
        {



            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {



                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsMetodo = new object[] { parametros };
                object[] argsClase = new object[] { Contructor };
                object Datos = new object();
                DataSet DataSet = new DataSet();




                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);



                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                Datos = MetodoClase.Invoke(Objeto, argsMetodo);
                DataSet = (DataSet)Datos;




                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);



                return DataSet;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }


        /// <summary>
        /// RETORNA OBJETO
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        protected object ServiceMotorObject(object Contructor, string Metodo, object parametros)
        {



            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {



                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsMetodo = new object[] { parametros };
                object[] argsClase = new object[] { Contructor };
                object Retorno = null;


                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);



                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                Retorno = MetodoClase.Invoke(Objeto, argsMetodo);


                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);



                return Retorno;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }


        /// <summary>
        /// RETORNA OBJETO
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <returns></returns>
        protected object ServiceMotorObject(object Contructor, string Metodo)
        {



            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {



                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsClase = new object[] { Contructor };
                object Retorno = null;


                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);



                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                Retorno = MetodoClase.Invoke(Objeto, null);


                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);



                return Retorno;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }

        /// <summary>
        /// RETORNA DATASET
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <returns></returns>
        protected DataSet ServiceMotor(object Contructor, string Metodo)
        {



            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {



                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsClase = new object[] { Contructor };
                object Datos = new object();
                DataSet DataSet = new DataSet();



                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                Datos = MetodoClase.Invoke(Objeto, null);
                DataSet = (DataSet)Datos;



                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);



                return DataSet;


            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }


        /// <summary>
        /// SOLO EJECUTAR
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        /// <param name="parametros"></param>
        protected void VoidServiceMotor(object Contructor, string Metodo, object parametros)
        {



            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsMetodo = new object[] { parametros };
                object[] argsClase = new object[] { Contructor };


                //===========================================================
                // SE INSTANCIAN LA CLASE                                  ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                MetodoClase.Invoke(Objeto, argsMetodo);




                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);




            }
            catch (Exception Ex)
            {
                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }


        /// <summary>
        /// SOLO EJECUTAR
        /// </summary>
        /// <param name="Contructor"></param>
        /// <param name="Metodo"></param>
        protected void VoidServiceMotor(object Contructor, string Metodo)
        {


            //===============================================================
            // SE DECLARA EL OBJETO QUE CONTENDRA LA CLASE                 ==
            //===============================================================
            object Objeto = new object();
            Type Clase = typeof(AAlo);


            try
            {




                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                object[] argsClase = new object[] { Contructor };


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                Objeto = Activator.CreateInstance(Clase, argsClase);


                //===========================================================
                // LA CLASE DEBE CONECTAR CON LA BASE DE DATOS             ==
                //===========================================================
                System.Reflection.MethodInfo MetodoConexion = Clase.GetMethod("Conecta");
                MetodoConexion.Invoke(Objeto, null);


                //===========================================================
                // SE INVOCA METODO QUE REALIZA LA CLASE                   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoClase = Clase.GetMethod(Metodo);
                MetodoClase.Invoke(Objeto, null);




                //===========================================================
                // LA CLASE DEBE CERRAR LA CONEXION CON LA BASE DE DATOS   ==
                //===========================================================
                System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                MetodoCerrar.Invoke(Objeto, null);




            }
            catch (Exception Ex)
            {

                if (Ex.InnerException != null)
                {

                    throw new Exception(Ex.InnerException.ToString());
                }
                else
                {
                    throw new Exception(Ex.Message);
                }

            }
            finally
            {
                try
                {

                    System.Reflection.MethodInfo MetodoCerrar = Clase.GetMethod("CerrarConexion");
                    MetodoCerrar.Invoke(Objeto, null);

                }
                catch { }

            }

        }

    }
}
