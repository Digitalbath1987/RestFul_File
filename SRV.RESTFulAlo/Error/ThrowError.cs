using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using SRV.Entidades;

namespace SRV.RESTFulAlo.Error
{
    public static class ThrowError
    {

        


        /// <summary>
        /// CREACIÓN DE ENTIDADES
        /// </summary>
        /// <param name="Entidad"></param>
        /// <param name="Root"></param>
        /// <returns></returns>
        private static XmlDocument GetEntityXml(ErroresException Entidad, String Root)
        {


            XmlDocument xmlDoc = new XmlDocument();

            try
            {


                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlAttributeOverrides overrides = new XmlAttributeOverrides();
                XmlAttributes attr = new XmlAttributes();
                attr.XmlRoot = new XmlRootAttribute(Root);
                overrides.Add(typeof(ErroresException), attr);



                XPathNavigator nav = xmlDoc.CreateNavigator();
                using (XmlWriter writer = nav.AppendChild())
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ErroresException), overrides);
                    ser.Serialize(writer, Entidad, ns);
                }




                return xmlDoc;
            }
            catch
            {

                return xmlDoc;
            }
        }



        /*----------------------------------------------------------------------*/
        /* MENSAJES THROW                                                       */
        /*----------------------------------------------------------------------*/
        public static String MensajeThrow(ErroresException Entidad)
        {

            String Mensaje = "";

            try
            {


                //===========================================================
                // DECLARACION DE VARIABLES                                ==
                //===========================================================
                XmlDocument Documento = new XmlDocument();



                //===========================================================
                // CREACIÓN DE XML                                         ==
                //===========================================================
                Documento = GetEntityXml(Entidad, "Detalles");


                //===========================================================
                // RUTA XSLT TRANSFORMADOR                                 ==
                //===========================================================
                string RUTA_XSLT = HttpContext.Current.Server.MapPath("~/Error/ThrowError.xslt");



                //===========================================================
                // TRANSFORMA XML                                          ==
                //===========================================================
                XPathDocument doc = new XPathDocument(new StringReader(Documento.InnerXml.ToString()));
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(RUTA_XSLT);
                StringWriter sw = new StringWriter();
                xslt.Transform(doc, null, sw);


                return sw.ToString();


            }
            catch
            {
                return Mensaje;
            }

        }



    }

}