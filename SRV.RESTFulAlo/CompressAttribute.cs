using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRV.RESTFulAlo
{
    public class CompressAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = HttpContext.Current.Response;


            var acceptEncodingHeader = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(acceptEncodingHeader) &&
                ((acceptEncodingHeader.Contains("gzip") || acceptEncodingHeader.Contains("deflate"))))
            {
                response.Headers.Remove("Content-Encoding");
                if (acceptEncodingHeader.Contains("gzip"))
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }

           
        }

        /// <summary>
        /// EVENTO AL FINALIZAR METODO
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            try
            {


                
                //===========================================================
                // RUTAS
                //===========================================================
                string FolderUpload = System.Web.HttpContext.Current.Server.MapPath("~/FileUpload/");
                string FolderDownload = System.Web.HttpContext.Current.Server.MapPath("~/FileDownload/");



                //=========================================================
                // FECHA ACTUAL
                //=========================================================
                DateTime FECHA_ACTUAL = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                //=========================================================
                // LECTURA DE CARPETA UPLOAD
                //=========================================================
                DirectoryInfo DirUpload = new DirectoryInfo(FolderUpload);
                foreach (var FileItem in DirUpload.GetFiles("*.*"))
                {

                    FileInfo FileToken = new FileInfo(FileItem.FullName);
                    DateTime FECHA_FILE = Convert.ToDateTime(FileToken.CreationTime.ToShortDateString());


                    //=====================================================
                    // DIAS
                    //=====================================================
                    int Dias = ((TimeSpan)(FECHA_ACTUAL - FECHA_FILE)).Days;


                    if (Dias == 1)
                    {


                        //=================================================
                        // ELIMINAR ARCHIVO
                        //=================================================
                        File.Delete(FileItem.FullName);


                    }



                }

                //=========================================================
                // LECTURA DE CARPETA DOWNLOAD
                //=========================================================
                DirectoryInfo DirDownload = new DirectoryInfo(FolderDownload);
                foreach (var FileItem in DirDownload.GetFiles("*.*"))
                {

                    FileInfo FileToken = new FileInfo(FileItem.FullName);
                    DateTime FECHA_FILE = Convert.ToDateTime(FileToken.CreationTime.ToShortDateString());


                    //=====================================================
                    // DIAS
                    //=====================================================
                    int Dias = ((TimeSpan)(FECHA_ACTUAL - FECHA_FILE)).Days;


                    if (Dias > 2)
                    {


                        //=================================================
                        // ELIMINAR ARCHIVO
                        //=================================================
                        File.Delete(FileItem.FullName);


                    }



                }


            }
            catch { }


        }


    }
}