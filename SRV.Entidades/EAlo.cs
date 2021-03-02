using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace SRV.Entidades
{

    //===================================================================
    /// <summary>
    /// ESTRUCTURA PARAMETROS
    /// </summary>
    public class R_FTP
    {
        public String SERVIDOR { get; set; }
        public String USUARIO { get; set; }
        public String PASSWORD { get; set; }
        public String KEY { get; set; }

        public R_FTP()
        {
            SERVIDOR = "";
            USUARIO = "";
            PASSWORD = "";
            KEY = "";
        }

    }

    //===================================================================
    /// <summary>
    /// ESTRUCTURA PARAMETROS
    /// </summary>
    public class R_FILE_FTP
    {
        public String RUTA { get; set; }
        public String FILE { get; set; }

        public R_FILE_FTP()
        {
            RUTA = "";
            FILE = "";
        }

    }

    //===================================================================
    /// <summary>
    /// ESTRUCTURA PARAMETROS
    /// </summary>
    public class R_FILE_LOCAL
    {
        public String RUTA { get; set; }
        public String FILE { get; set; }

        public R_FILE_LOCAL()
        {
            RUTA = "";
            FILE = "";
        }

    }


    //===================================================================
    /// <summary>
    /// ESTRUCTURA PARAMETROS
    /// </summary>
    public class R_CORREO
    {
        public String ASUNTO { get; set; }
        public String CORREOS { get; set; }
        public String DELIMITADOR_CORREO { get; set; }

        public R_CORREO()
        {
            CORREOS = "";
            DELIMITADOR_CORREO = "";
            ASUNTO = "";
        }

    }


    //===================================================================
    /// <summary>
    /// ESTRUCTURA PARAMETROS
    /// </summary>
    public class R_DB_BCP
    {
        public String SERVIDOR { get; set; }
        public String USUARIO { get; set; }
        public String PASSWORD { get; set; }
        public String BASE_DATOS { get; set; }
        public String TABLA { get; set; }
        public String DELIMITADOR { get; set; }


        public R_DB_BCP()
        {
            SERVIDOR = "";
            USUARIO = "";
            PASSWORD = "";
            BASE_DATOS = "";
            TABLA = "";
            DELIMITADOR = "";
        }

    }

    //===================================================================
    /// <summary>
    /// INPUT JSON DEL APLICATIVO
    /// </summary>


    public class INPUT_JSON_FTP
    {



        public R_FTP R_FTP;
        public R_FILE_FTP R_FILE_FTP;
        public R_FILE_LOCAL R_FILE_LOCAL;

        //===========================================================
        // CONTRUCTOR                                              ==
        //===========================================================
        public INPUT_JSON_FTP()
        {
            R_FTP = new R_FTP();
            R_FILE_FTP = new R_FILE_FTP();
            R_FILE_LOCAL = new R_FILE_LOCAL();
        }



    }

    //===================================================================
    /// <summary>
    /// INPUT JSON DEL APLICATIVO
    /// </summary>


    public class INPUT_JSON_FTP_SMTP
    {



        public R_FTP R_FTP;
        public R_FILE_FTP R_FILE_FTP;
        public R_CORREO R_CORREO;

        //===========================================================
        // CONTRUCTOR                                              ==
        //===========================================================
        public INPUT_JSON_FTP_SMTP()
        {
            R_FTP = new R_FTP();
            R_FILE_FTP = new R_FILE_FTP();
            R_CORREO = new R_CORREO();
        }



    }

    //===================================================================
    /// <summary>
    /// INPUT JSON DEL APLICATIVO
    /// </summary>


    public class INPUT_JSON_FTP_BCP
    {



        public R_FTP R_FTP;
        public R_FILE_FTP R_FILE_FTP;
        public R_DB_BCP R_DB_BCP;

        //===========================================================
        // CONTRUCTOR                                              ==
        //===========================================================
        public INPUT_JSON_FTP_BCP()
        {
            R_FTP = new R_FTP();
            R_FILE_FTP = new R_FILE_FTP();
            R_DB_BCP = new R_DB_BCP();
        }



    }

    //===================================================================
    /// <summary>
    /// ENTIDAD DE ERRORES PROVOCADOS POR EL APLICATIVO
    /// </summary>
    /// 
    public class ErroresException
    {
        public string NombreMetodo { get; set; }
        public string Clase { get; set; }
        public string NameSpace { get; set; }
        public string Mensaje { get; set; }
        public List<Secuencia> Eventos { get; set; }


        public ErroresException()
        {
            NombreMetodo = "";
            Clase = "";
            NameSpace = "";
            Mensaje = "";
            Eventos = new List<Secuencia>();
        }

    }
    //===================================================================
    /// <summary>
    /// SECUENCIA DE METODOS QUE PROVOCARON LA CAIDA
    /// </summary>
    public class Secuencia
    {
        public string Item { get; set; }

        public Secuencia()
        {
            Item = "";
        }
    }
    //===================================================================
    /// <summary>
    /// ESTADO BINARIO DEL APLICATIVO AL CONTESTAR
    /// </summary>
    public class Header
    {
        public int ESTADO { get; set; }

    }

    //===================================================================
    /// <summary>
    /// RESULTADOS GENERICOS DEL SISTEMA
    /// </summary>
    public class ResultadosHTTP
    {

        public MensajeHTTP DETALLES { get; set; }

        public ResultadosHTTP()
        {
            DETALLES = new MensajeHTTP();
        }

    }


    //===================================================================
    /// <summary>
    /// RESULTADOS JSON DEL APLICATIVO
    /// </summary>
    public class RETURN_JSON_ALO
    {


        public Header HEADER;
        public ErroresException ERRORES;
        public ResultadosHTTP RESULT_HTTP;

        //===========================================================
        // CONTRUCTOR                                              ==
        //===========================================================
        public RETURN_JSON_ALO()
        {
            HEADER = new Header();
            ERRORES = new ErroresException();
            RESULT_HTTP = new ResultadosHTTP();
        }



    }

    //===================================================================
    /// <summary>
    /// ESTRUCTURA MENSAJE
    /// </summary>
    public class MensajeHTTP
    {
        public String MSG { get; set; }

    }

 

    
}
