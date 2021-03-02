using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.Entidades
{
    public class iSP_CREATE_FIFO_FTP
    {
        public String TOKEN { get; set; }
        public String SERVIDOR { get; set; }
        public String USUARIO { get; set; }
        public String PASSWORD { get; set; }
        public String KEY_SSH { get; set; }
        public String RUTA { get; set; }
        public String FILE_FTP { get; set; }
        public String ASUNTO { get; set; }
        public String CORREOS { get; set; }
        public String DELIMITADOR_CORREO { get; set; }
    }

    public class iSP_CREATE_FIFO_FTP_BCP
    {
        public String SERVIDOR_FTP { get; set; }
        public String USUARIO_FTP { get; set; }
        public String PASSWORD_FTP { get; set; }
        public String KEY_SSH_FTP { get; set; }
        public String RUTA { get; set; }
        public String FILE_FTP { get; set; }
        public String SERVIDOR_DB { get; set; }
        public String USUARIO_DB { get; set; }
        public String PASSWORD_DB { get; set; }
        public String BASE_DATOS { get; set; }
        public String TABLA { get; set; }
        public String DELIMITADOR { get; set; }
    }

}
