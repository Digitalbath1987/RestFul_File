using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using SRV.Entidades;

namespace SRV.RESTFulAlo
{
    public static class FuncionesGenerales
    {

        /// <summary>
        /// CONFIGURACIÓN DE ERRORES 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ErroresException CONFIGURA_ERRORES(Exception ex)
        {

            ErroresException Entidad = new ErroresException();


            try
            {



                MethodBase MetodoGatillante = ex.TargetSite;
                Type Class = MetodoGatillante.ReflectedType;


                //===========================================================
                // OPCIONES DE METODO GATILLANTE                           ==
                //===========================================================
                Entidad.NombreMetodo = MetodoGatillante.Name;
                Entidad.Clase = Class.FullName;
                Entidad.NameSpace = Class.Namespace;

                string Mensaje = ex.Message;
                Mensaje = Mensaje.Replace("\"", " ");
                Mensaje = Mensaje.Replace("'", " ");


                Entidad.Mensaje = Mensaje;


                //===========================================================
                // METODO EN CUAL FUE LA CAIDA                             ==
                //===========================================================
                List<Secuencia> Lista = new List<Secuencia>();
                StackTrace st = new StackTrace(ex, true);
                for (int i = st.FrameCount - 1; i >= 0; i--)
                {

                    StackFrame sf = st.GetFrame(i);
                    System.Reflection.MethodBase method = sf.GetMethod();
                    Lista.Add(new Secuencia { Item = method.Name });
                }
                Entidad.Eventos = Lista;





                return Entidad;

            }
            catch
            {
                return Entidad;

            }

        }


    }
}