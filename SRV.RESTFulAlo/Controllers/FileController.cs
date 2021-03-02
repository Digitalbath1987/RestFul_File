using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SRV.Entidades;
using SRV.Negocio;
using SRV.Utilidades;


namespace SRV.RESTFulAlo.Controllers
{
    public class FileController : Controller
    {

        /// <summary>
        /// UPLOAD FILE FTP
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Compress]
        [HttpPost]
        public string F_POST_UPLOAD_FTP(string json)
        {



            RETURN_JSON_ALO RetornoDATA = new RETURN_JSON_ALO();
            string Metodo = "F_POST_UPLOAD_FTP";

            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                INPUT_JSON_FTP EstructuraInput = new INPUT_JSON_FTP();


                //===========================================================
                // SABIENDO EL METODO PUEDO CONSTRUIR EL OBJETO            ==
                //===========================================================
                INPUT_JSON_FTP Objeto = null;
                using (UObjetoJson ObjetoRetorno = new UObjetoJson())
                {
                    Objeto = ObjetoRetorno.Deserialize<INPUT_JSON_FTP>(json);
                }


                //===========================================================
                // PUT DE ARCHIVO PARA SER ENVIADO AL FTP
                //===========================================================
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/FileUpload/" + Objeto.R_FILE_LOCAL.FILE);

                if (System.IO.File.Exists(FilePath) == false)
                {
                    throw new Exception("ARCHIVO ESPECIFICADO NO EXISTE EN RUTA");

                }


                //===========================================================
                // CONECTAR A SITIO FTP
                //===========================================================
                using (UWinSCP FTP_ALO = new UWinSCP(Objeto.R_FTP.SERVIDOR,Objeto.R_FTP.USUARIO,Objeto.R_FTP.PASSWORD,Objeto.R_FTP.KEY))
                {


                    string PathRemoto = Objeto.R_FILE_FTP.RUTA + @"/" + Objeto.R_FILE_FTP.FILE;


                    int ESTADO_UPLOAD = FTP_ALO.PutFTP(FilePath, PathRemoto);

                    if (ESTADO_UPLOAD == 1)
                    {
                        System.IO.File.Delete(FilePath);

                    }
                    else
                    {
                        throw new Exception("ARCHIVO NO FUE SUBIDO A SITIO FTP SOLICITADO");

                    }

                }


                //===========================================================
                // SALIDA DE DATOS                                         ==
                //===========================================================
                RetornoDATA.HEADER.ESTADO = 1;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "OK";


                //===========================================================
                // RETORNO                                                 ==
                //===========================================================
                String JsonResult = "";
                using (UObjetoJson ObjetoSer = new UObjetoJson())
                {
                    JsonResult = ObjetoSer.Serialize(RetornoDATA);
                }

                return JsonResult;



            }
            catch (System.Exception ex)
            {

                RetornoDATA.HEADER.ESTADO = 0;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "0";
                RetornoDATA.ERRORES = CONFIGURA_ERRORES(ex, Metodo);
                return new JavaScriptSerializer().Serialize(RetornoDATA);

            }
            finally
            {
                RetornoDATA = null;
            }
        }


        /// <summary>
        /// UPLOAD FILE FTP SMTP
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Compress]
        [HttpPost]
        public string F_POST_UPLOAD_FTP_SMTP(string json)
        {



            RETURN_JSON_ALO RetornoDATA = new RETURN_JSON_ALO();
            string Metodo = "F_POST_UPLOAD_FTP_SMTP";

            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES
                //===========================================================
                NAlo Negocio = new NAlo();


                //===========================================================
                // SABIENDO EL METODO PUEDO CONSTRUIR EL OBJETO            ==
                //===========================================================
                INPUT_JSON_FTP_SMTP Objeto = null;
                using (UObjetoJson ObjetoRetorno = new UObjetoJson())
                {
                    Objeto = ObjetoRetorno.Deserialize<INPUT_JSON_FTP_SMTP>(json);
                }


                //===========================================================
                // FTP
                //===========================================================
                R_FTP FTP = Objeto.R_FTP;

                if (FTP == null)
                {
                    throw new Exception("FTP NO FUE ENVIADO");
                }


                if (String.IsNullOrEmpty(FTP.SERVIDOR))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(FTP.USUARIO))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(FTP.PASSWORD))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }



                //===========================================================
                // ARCHIVOS
                //===========================================================
                R_FILE_FTP ARCHIVOS = Objeto.R_FILE_FTP;

                if (ARCHIVOS == null)
                {
                    throw new Exception("ARCHIVOS NO FUERON ENVIADOS");
                }

                
                //===========================================================
                // CORREOS
                //===========================================================
                R_CORREO CORREO = Objeto.R_CORREO;

                if (CORREO == null)
                {
                    throw new Exception("CORREOS NO FUERON ENVIADOS");
                }

                //===========================================================
                // PARAMETROS DE ENTRADA 
                //===========================================================
                iSP_CREATE_FIFO_FTP ParametrosInput = new iSP_CREATE_FIFO_FTP();
                ParametrosInput.TOKEN = Guid.NewGuid().ToString();
                ParametrosInput.SERVIDOR = Objeto.R_FTP.SERVIDOR;
                ParametrosInput.USUARIO = Objeto.R_FTP.USUARIO;
                ParametrosInput.PASSWORD = Objeto.R_FTP.PASSWORD;
                ParametrosInput.KEY_SSH = Objeto.R_FTP.KEY;
                ParametrosInput.RUTA = Objeto.R_FILE_FTP.RUTA;
                ParametrosInput.FILE_FTP = Objeto.R_FILE_FTP.FILE;
                ParametrosInput.ASUNTO = Objeto.R_CORREO.ASUNTO;
                ParametrosInput.CORREOS = Objeto.R_CORREO.CORREOS;
                ParametrosInput.DELIMITADOR_CORREO = Objeto.R_CORREO.DELIMITADOR_CORREO;


                //===========================================================
                // ENVIAR OBJETO A NEGOCIO 
                //===========================================================
                decimal Retorno = Negocio.SP_CREATE_FIFO_FTP(ParametrosInput);


                //===========================================================
                // EVALUAR RETORNOS 
                //===========================================================
                if (Retorno == 0)
                {
                    throw new Exception("SISTEMA NO INGRESO FIFO FTP PARA ENVIO DE CORREOS");
                }
                



                //===========================================================
                // SALIDA DE DATOS                                         ==
                //===========================================================
                RetornoDATA.HEADER.ESTADO = 1;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "OK";


                //===========================================================
                // RETORNO                                                 ==
                //===========================================================
                String JsonResult = "";
                using (UObjetoJson ObjetoSer = new UObjetoJson())
                {
                    JsonResult = ObjetoSer.Serialize(RetornoDATA);
                }

                return JsonResult;



            }
            catch (System.Exception ex)
            {

                RetornoDATA.HEADER.ESTADO = 0;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "0";
                RetornoDATA.ERRORES = CONFIGURA_ERRORES(ex, Metodo);
                return new JavaScriptSerializer().Serialize(RetornoDATA);

            }
            finally
            {
                RetornoDATA = null;
            }
        }

        /// <summary>
        /// UPLOAD FILE FTP BCP
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Compress]
        [HttpPost]
        public string F_POST_UPLOAD_FTP_BCP(string json)
        {



            RETURN_JSON_ALO RetornoDATA = new RETURN_JSON_ALO();
            string Metodo = "F_POST_UPLOAD_FTP_BCP";

            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES
                //===========================================================
                NAlo Negocio = new NAlo();


                //===========================================================
                // SABIENDO EL METODO PUEDO CONSTRUIR EL OBJETO            ==
                //===========================================================
                INPUT_JSON_FTP_BCP Objeto = null;
                using (UObjetoJson ObjetoRetorno = new UObjetoJson())
                {
                    Objeto = ObjetoRetorno.Deserialize<INPUT_JSON_FTP_BCP>(json);
                }


                //===========================================================
                // FTP
                //===========================================================
                R_FTP FTP = Objeto.R_FTP;

                if (FTP == null)
                {
                    throw new Exception("FTP NO FUE ENVIADO");
                }


                if (String.IsNullOrEmpty(FTP.SERVIDOR))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(FTP.USUARIO))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(FTP.PASSWORD))
                {
                    throw new Exception("FTP DEBE CONTENER SERVIDOR");

                }

                
                //===========================================================
                // ARCHIVOS
                //===========================================================
                R_FILE_FTP ARCHIVOS = Objeto.R_FILE_FTP;

                if (ARCHIVOS == null)
                {
                    throw new Exception("ARCHIVOS NO FUERON ENVIADOS");
                }

                if (String.IsNullOrEmpty(ARCHIVOS.FILE))
                {
                    throw new Exception("ARCHIVOS  DEBE CONTENER UN NOMBRE");

                }

                if (String.IsNullOrEmpty(ARCHIVOS.RUTA))
                {
                    throw new Exception("RUTA  DEBE CONTENER UN NOMBRE");

                }


                //===========================================================
                // BASE DE DATOS
                //===========================================================
                R_DB_BCP DB = Objeto.R_DB_BCP;

                if (DB == null)
                {
                    throw new Exception("BASE DE DATOS NO FUERON ENVIADOS");
                }

                if (String.IsNullOrEmpty(DB.SERVIDOR))
                {
                    throw new Exception("BASE DE DATOS DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(DB.USUARIO))
                {
                    throw new Exception("BASE DE DATOS DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(DB.PASSWORD))
                {
                    throw new Exception("BASE DE DATOS DEBE CONTENER SERVIDOR");

                }

                if (String.IsNullOrEmpty(DB.TABLA))
                {
                    throw new Exception("BASE DE DATOS DEBE CONTENER TABLA");

                }

                if (String.IsNullOrEmpty(DB.DELIMITADOR))
                {
                    throw new Exception("BASE DE DATOS DEBE CONTENER DELIMITADOR");

                }


                //===========================================================
                // PARAMETROS DE ENTRADA 
                //===========================================================
                iSP_CREATE_FIFO_FTP_BCP ParametrosInput = new iSP_CREATE_FIFO_FTP_BCP();
                ParametrosInput.SERVIDOR_FTP = Objeto.R_FTP.SERVIDOR;
                ParametrosInput.USUARIO_FTP = Objeto.R_FTP.USUARIO;
                ParametrosInput.PASSWORD_FTP = Objeto.R_FTP.PASSWORD;
                ParametrosInput.KEY_SSH_FTP = Objeto.R_FTP.KEY;
                ParametrosInput.RUTA = Objeto.R_FILE_FTP.RUTA;
                ParametrosInput.FILE_FTP = Objeto.R_FILE_FTP.FILE;
                ParametrosInput.SERVIDOR_DB = Objeto.R_DB_BCP.SERVIDOR;
                ParametrosInput.USUARIO_DB = Objeto.R_DB_BCP.USUARIO;
                ParametrosInput.PASSWORD_DB = Objeto.R_DB_BCP.PASSWORD;
                ParametrosInput.BASE_DATOS = Objeto.R_DB_BCP.BASE_DATOS;
                ParametrosInput.TABLA = Objeto.R_DB_BCP.TABLA;
                ParametrosInput.DELIMITADOR = Objeto.R_DB_BCP.DELIMITADOR;



                //===========================================================
                // ENVIAR OBJETO A NEGOCIO 
                //===========================================================
                decimal Retorno = Negocio.SP_CREATE_FIFO_FTP_BCP(ParametrosInput);


                //===========================================================
                // EVALUAR RETORNOS 
                //===========================================================
                if (Retorno == 0)
                {
                    throw new Exception("SISTEMA NO INGRESO FIFO FTP PARA ENVIO DE CORREOS");
                }




                //===========================================================
                // SALIDA DE DATOS                                         ==
                //===========================================================
                RetornoDATA.HEADER.ESTADO = 1;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "OK";


                //===========================================================
                // RETORNO                                                 ==
                //===========================================================
                String JsonResult = "";
                using (UObjetoJson ObjetoSer = new UObjetoJson())
                {
                    JsonResult = ObjetoSer.Serialize(RetornoDATA);
                }

                return JsonResult;



            }
            catch (System.Exception ex)
            {

                RetornoDATA.HEADER.ESTADO = 0;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "0";
                RetornoDATA.ERRORES = CONFIGURA_ERRORES(ex, Metodo);
                return new JavaScriptSerializer().Serialize(RetornoDATA);

            }
            finally
            {
                RetornoDATA = null;
            }
        }

        /// <summary>
        /// UPLOAD FILE SERVIDOR
        /// </summary>
        /// <returns></returns>
        [Compress]
        [HttpPost]
        public string F_POST_UPLOAD_FILE()
        {


            RETURN_JSON_ALO RetornoDATA = new RETURN_JSON_ALO();
            string KEY;
            string Metodo = "F_POST_UPLOAD_FILE";

            try
            {



               
                //===========================================================
                // VERIFICAR QUE VIENEN ARCHIVOS A CARGAR         
                //===========================================================
                var httpRequest = System.Web.HttpContext.Current.Request;
                KEY = Guid.NewGuid().ToString();


                //===========================================================
                // SUBIR ARCHIVO          
                //===========================================================
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var PostFile = httpRequest.Files[file];
                        string Nombre = NameFile(PostFile.FileName);

                        Nombre = PostFile.FileName.Replace(Nombre, KEY);

                        var FilePath = System.Web.HttpContext.Current.Server.MapPath("~/FileUpload/" + Nombre);
                        PostFile.SaveAs(FilePath);


                    }

                }
                else
                {
                    throw new Exception("SERVICIO NO RECONOCIO ARCHIVO ENVIADO");

                }
                


                //===========================================================
                // SALIDA DE DATOS                                         ==
                //===========================================================
                RetornoDATA.HEADER.ESTADO = 1;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = KEY;


                //===========================================================
                // RETORNO                                                 ==
                //===========================================================
                String JsonResult = "";
                using (UObjetoJson ObjetoSer = new UObjetoJson())
                {
                    JsonResult = ObjetoSer.Serialize(RetornoDATA);
                }

                return JsonResult;



            }
            catch (System.Exception ex)
            {

                RetornoDATA.HEADER.ESTADO = 0;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "0";
                RetornoDATA.ERRORES = CONFIGURA_ERRORES(ex, Metodo);
                return new JavaScriptSerializer().Serialize(RetornoDATA);

            }
            finally
            {
                RetornoDATA = null;
            }
        }

        /// <summary>
        /// ELIMINAR ARCHIVO
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Compress]
        [HttpPost]
        public string F_POST_DELETE_FTP(string json)
        {



            RETURN_JSON_ALO RetornoDATA = new RETURN_JSON_ALO();
            string Metodo = "F_POST_DELETE_FTP";

            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                INPUT_JSON_FTP EstructuraInput = new INPUT_JSON_FTP();


                //===========================================================
                // SABIENDO EL METODO PUEDO CONSTRUIR EL OBJETO            ==
                //===========================================================
                INPUT_JSON_FTP Objeto = null;
                using (UObjetoJson ObjetoRetorno = new UObjetoJson())
                {
                    Objeto = ObjetoRetorno.Deserialize<INPUT_JSON_FTP>(json);
                }


               

                //===========================================================
                // CONECTAR A SITIO FTP
                //===========================================================
                using (UWinSCP FTP_ALO = new UWinSCP(Objeto.R_FTP.SERVIDOR, Objeto.R_FTP.USUARIO, Objeto.R_FTP.PASSWORD, Objeto.R_FTP.KEY))
                {


                    string PathRemoto = Objeto.R_FILE_FTP.RUTA + @"/" + Objeto.R_FILE_FTP.FILE;


                    int ESTADO_UPLOAD = FTP_ALO.DeleteFTP(PathRemoto);

                    if (ESTADO_UPLOAD != 1)
                    {
                       
                        throw new Exception("ARCHIVO NO FUE ELIMINADO EN SITIO FTP SOLICITADO");

                    }

                }


                //===========================================================
                // SALIDA DE DATOS                                         ==
                //===========================================================
                RetornoDATA.HEADER.ESTADO = 1;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "OK";


                //===========================================================
                // RETORNO                                                 ==
                //===========================================================
                String JsonResult = "";
                using (UObjetoJson ObjetoSer = new UObjetoJson())
                {
                    JsonResult = ObjetoSer.Serialize(RetornoDATA);
                }

                return JsonResult;



            }
            catch (System.Exception ex)
            {

                RetornoDATA.HEADER.ESTADO = 0;
                RetornoDATA.RESULT_HTTP.DETALLES.MSG = "0";
                RetornoDATA.ERRORES = CONFIGURA_ERRORES(ex, Metodo);
                return new JavaScriptSerializer().Serialize(RetornoDATA);

            }
            finally
            {
                RetornoDATA = null;
            }
        }
        /// <summary>
        /// DESCARGAR FTP
        /// </summary>
        /// <param name="json"></param>
        [Compress]
        [HttpPost]
        public void F_POST_DOWNLOAD_FTP(string json)
        {




            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                
                //===========================================================
                INPUT_JSON_FTP EstructuraInput = new INPUT_JSON_FTP();
                string KEY = Guid.NewGuid().ToString();
                string Nombre = "";
                string FileDestino = "";



                //===========================================================
                // SABIENDO EL METODO PUEDO CONSTRUIR EL OBJETO            
                //===========================================================
                INPUT_JSON_FTP Objeto = null;
                using (UObjetoJson ObjetoRetorno = new UObjetoJson())
                {
                    Objeto = ObjetoRetorno.Deserialize<INPUT_JSON_FTP>(json);
                }

                //===========================================================
                // DEBEMOS SABER QUE ARCHIVO DEBEMOS DESCARGAR
                //===========================================================


                //===========================================================
                // CONECTAR A SITIO FTP
                //===========================================================
                using (UWinSCP FTP_ALO = new UWinSCP(Objeto.R_FTP.SERVIDOR, Objeto.R_FTP.USUARIO, Objeto.R_FTP.PASSWORD, Objeto.R_FTP.KEY))
                {


                    string PathRemoto = Objeto.R_FILE_FTP.RUTA + @"/" + Objeto.R_FILE_FTP.FILE;


                    Nombre = Objeto.R_FILE_FTP.FILE.Replace(Objeto.R_FILE_FTP.FILE, KEY);
                    FileDestino = System.Web.HttpContext.Current.Server.MapPath("~/FileDownload/" + Nombre);


                    int ESTADO_UPLOAD = FTP_ALO.DowloadSFTP(PathRemoto, FileDestino);

                    if (ESTADO_UPLOAD != 1)
                    {
                        throw new Exception("ARCHIVO NO FUE DESCARGADO A SITIO FTP SOLICITADO");

                    }

                }




                //===========================================================
                // ARCHIVO
                //===========================================================
                string ArchivoDescarga = FileDestino;
                FileInfo FileDownload = new FileInfo(ArchivoDescarga);


                byte[] bytes = System.IO.File.ReadAllBytes(ArchivoDescarga);

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + Objeto.R_FILE_LOCAL.FILE);
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.End();


               



            }
            catch 
            {


            }
            finally
            {
               
            }
        }
        /// <summary>
        /// CONFIGURACIÓN DE ERRORES 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private ErroresException CONFIGURA_ERRORES(Exception ex, string Metodo)
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


                Entidad.Mensaje = Metodo + " " + Mensaje;


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

        /// <summary>
        /// NOMBRE DE ARCHIVO
        /// </summary>
        /// <param name="FullPath"></param>
        /// <returns></returns>
        public string NameFile(string FullPath)
        {
            try
            {

                FileInfo FILE_EXE = new FileInfo(FullPath);


                char Separador = Convert.ToChar(".");
                string[] PathSplit = FILE_EXE.Name.Split(Separador);



                if (PathSplit.Length > 1)
                {

                    return PathSplit[0].ToString();

                }
                else
                {

                    throw new Exception("NO SE PUDO DETERMINAR NOMBRE DE ARCHIVO");
                }


            }
            catch
            {
                throw;
            }

        }
    }
}
