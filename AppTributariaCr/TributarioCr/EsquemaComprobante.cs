using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class EsquemaComprobante
    {
        public string clave { get; set; }

        public string  fecha { get; set; }

        public EmisorType emisor { get; set; }

        public ReceptorType receptor { get; set; }

        public string callbackUrl{ get; set; }

        public string comprobanteXml{ get; set; }

        public class EmisorType
        {
            public string tipoIdentificacion { get; set; }
            public string numeroIdentificacion { get; set; }
        }

        public class ReceptorType
        {
            public string tipoIdentificacion { get; set; }
            public string numeroIdentificacion { get; set; }
        }
    }

}
