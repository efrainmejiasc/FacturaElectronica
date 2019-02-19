using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace TributarioCr
{
    public class CreadorComprobante
    {
        private EngineData Valor = EngineData.Instance();

        public string CadenaComprobante (string clave, string consecutivo,string tipoIdEmisor, string numeroIdEmisor, string tipoIdReceptor, string numeroIdReceptor,XmlDocument documentoXml)
        {
            string resultado = string.Empty;
            try
            {
                EsquemaComprobante comprobante = new EsquemaComprobante();
                comprobante = CrearComprobante (clave, consecutivo, tipoIdEmisor, numeroIdEmisor, tipoIdReceptor, numeroIdReceptor,documentoXml);
                resultado = new JavaScriptSerializer().Serialize(comprobante);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en metodo CadenaComprobante", ex);
            }
            return resultado;
        }

        public EsquemaComprobante CrearComprobante(string clave, string consecutivo, string tipoIdEmisor, string numeroIdEmisor, string tipoIdReceptor, string numeroIdReceptor, XmlDocument documentoXml)
        {

            EsquemaComprobante comprobante = new EsquemaComprobante()
            {
                clave = clave,
                fecha = DateTime.UtcNow.ToString(EngineData.dateFormat),
                emisor = new EsquemaComprobante.EmisorType()
                {
                    tipoIdentificacion = tipoIdEmisor,
                    numeroIdentificacion = numeroIdEmisor
                },
                receptor = new EsquemaComprobante.ReceptorType()
                {
                    tipoIdentificacion = tipoIdReceptor,
                    numeroIdentificacion = numeroIdReceptor
                },
                callbackUrl = Valor.UrlDevolucionLlamadaEmisor(),
                comprobanteXml = ConvertirBase64(documentoXml)
            };

            return comprobante;
        }

        public string CadenaComprobante(string [] valores, XmlDocument documentoXml)
        {
            string resultado = string.Empty;
            try
            {
                EsquemaComprobanteReceptor comprobante = new EsquemaComprobanteReceptor();
                comprobante = CrearComprobanteReceptor(valores, documentoXml);
                resultado = new JavaScriptSerializer().Serialize(comprobante);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en metodo CadenaComprobanteReceptor", ex);
            }
            return resultado;
        }

        public EsquemaComprobanteReceptor CrearComprobanteReceptor(string [] valores , XmlDocument documentoXml)
        {
            //{ "Clave", "FechaEmision", "TipoEmisor", "IdentificacionEmisor", "TipoReceptor","IdentificacionReceptor", "TotalImpuesto", "TotalComprobante" ,"NumeroConsecutivoReceptor"};
            EsquemaComprobanteReceptor comprobante = new EsquemaComprobanteReceptor()
            {
                clave = valores[0],
                fecha = DateTime.UtcNow.ToString(EngineData.dateFormat),
                emisor = new EsquemaComprobanteReceptor.EmisorType()
                {
                    tipoIdentificacion = valores[2],
                    numeroIdentificacion = valores[3]
                },
                receptor = new EsquemaComprobanteReceptor.ReceptorType()
                {
                    tipoIdentificacion = valores[4],
                    numeroIdentificacion = valores[5]
                },
                callbackUrl = Valor.UrlDevolucionLlamadaReceptor(),
                consecutivoReceptor = valores[8],
                comprobanteXml = ConvertirBase64(documentoXml)
            };

            return comprobante;
        }

        private string ConvertirBase64(XmlDocument documentXml)
        {
            documentXml.PreserveWhitespace = true;
            var comprobanteXmlPlainTextBytes = Encoding.UTF8.GetBytes(documentXml.InnerXml);
            var comprobanteXml = Convert.ToBase64String(comprobanteXmlPlainTextBytes);
            return comprobanteXml;
        }

        public string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


    }
}
