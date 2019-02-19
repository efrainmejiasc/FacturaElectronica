using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class EsquemaComprobanteReceptor
    {
        public string clave { get; set; }

        public string fecha { get; set; }

        public EmisorType emisor { get; set; }

        public ReceptorType receptor { get; set; }

        public string callbackUrl { get; set; }

        public string consecutivoReceptor { get; set; }

        public string comprobanteXml { get; set; }

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

    public class DataMensajeReceptor
    {
        public string Clave { get; set; }

        public string NumeroCedulaEmisor { get; set; }

        public DateTime FechaEmisionDoc { get; set; }

        public string Mensaje { get; set; }

        public string DetalleMensaje { get; set; }

        public decimal MontoTotalImpuesto { get; set; }

        public bool MontoTotalImpuestoSpecified { get; set; }

        public decimal TotalFactura { get; set; }

        public string NumeroCedulaReceptor { get; set; }

        public string NumeroConsecutivoReceptor { get; set; }
    }
}
