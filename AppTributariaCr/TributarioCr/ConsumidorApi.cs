using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Modelo.Modulos.FacturaElectronica;
using System.Xml;

namespace TributarioCr
{
    public class ConsumidorApi
    {
        private string cadenaToken = string.Empty;
        private string jsonValidacion = string.Empty;
        private string urlValidacion = string.Empty;
        private EngineData Valor = EngineData.Instance();
     

        private class Token { public string Access_token { get; set; } }

        //METODO PARA OBTENER  TOKEN DE HACIENDA (USUARIO Y PASSWORD EMISOR)
        public async Task<string> ObtenerAutentificacionPost()
        {
            cadenaToken = string.Empty;
            Token token = new Token();
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Valor.UrlAutentificacion());
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>(EngineData.clienteID,Valor.Client_Id()));
            formData.Add(new KeyValuePair<string, string>(EngineData.grantType,Valor.Grant_Type()));
            formData.Add(new KeyValuePair<string, string>(EngineData.userName, Valor.Username()));
            formData.Add(new KeyValuePair<string, string>(EngineData.passWord, Valor.Password()));
            formData.Add(new KeyValuePair<string, string>(EngineData.scoPe, Valor.Scope()));
            request.Content = new FormUrlEncodedContent(formData);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                cadenaToken = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(cadenaToken);
                cadenaToken = token.Access_token;
            }
            else
            {
                cadenaToken= EngineData.falso;
            }

            return cadenaToken;
        }

        //METODO PARA CREAR , FIRMAR Y ENVIAR DOCUMENTOS ELECTRONICOS EMITIDOS (Factura, NotaDebito , NotaCredito,TiqueteElectronico ) 
        public async Task<string> EnviarDocumentoPost(string accessToken, ComprobanteElectronicoCRI objComprobanteElectronicoCRI, string tipoDocumento)
        {
            urlValidacion = string.Empty;
            string jsonDocumento = string.Empty;
            EngineDocumentoXml MetodoCrear = new EngineDocumentoXml ();
            XmlDocument documentoXml = new XmlDocument();
            documentoXml = MetodoCrear.CrearDocumentoXml(objComprobanteElectronicoCRI, tipoDocumento );
            FirmaXadesEpes MetodoFirmar = new FirmaXadesEpes();
            documentoXml = MetodoFirmar.XadesEpesFirma(documentoXml,Valor.PathCertificadoNeo(),Valor.PinCertificadoNeo()); // DATOS DEL CERTIFICADO DEL EMISOR DEL DOCUMENTO
            //documentoXml = MetodoFirmar.XadesEpesFirma(documentoXml, Valor.PathCertificadoByron(), Valor.PinCertificadoByron());

            CreadorComprobante ComprobanteXml= new CreadorComprobante();
            string tipoEmisorNumero = Valor.TipoEmisor().Substring(Valor.TipoEmisor().Length - 2, 2);//Quito la palabra Item al TipoEmisor
            string tipoReceptorNumero = objComprobanteElectronicoCRI.ComprobanteElectronicoCRIEntidadJuridicaReceptor.TipoIdentificacion.Substring(objComprobanteElectronicoCRI.ComprobanteElectronicoCRIEntidadJuridicaReceptor.TipoIdentificacion.Length - 2 , 2);
            jsonDocumento = ComprobanteXml.CadenaComprobante(objComprobanteElectronicoCRI.Clave, objComprobanteElectronicoCRI.NumeroConsecutivo, tipoEmisorNumero, Valor.NumeroIdentificacionEmisor(), tipoEmisorNumero, objComprobanteElectronicoCRI.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Identificacion, documentoXml);

            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EngineData.bearer, accessToken);
            HttpResponseMessage response = await client.PostAsync(Valor.UrlEnvioDocumento(), new StringContent(jsonDocumento, Encoding.UTF8, EngineData.application_json));
            if (response.IsSuccessStatusCode)
            {
                urlValidacion = response.Headers.Location.ToString();
            }
            else
            {
                urlValidacion = EngineData.falso;
            }

            return urlValidacion;
        }

        // CLASE PARA SERIALIZAR LA RESPUESTA DE HACIENDA
        public class Validacion { public string Clave { get; set; } public string Fecha { get; set; } public string IndEstado { get; set; } public string RespuestaXml { get; set; } }

        // METODO DONDE SE VALIDA EL DOCUMENTO ENVIADO
        public async Task<Validacion> ValidarDocumentoGet(string urlLocation, string accessToken)
        {
            Validacion validacion = new Validacion();

            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EngineData.bearer, accessToken);
            HttpResponseMessage response = await client.GetAsync(urlLocation);
            if (response.IsSuccessStatusCode)
            {
                jsonValidacion = response.Content.ReadAsStringAsync().Result;
                jsonValidacion = jsonValidacion.Replace("-", "");
                validacion = JsonConvert.DeserializeObject<Validacion>(jsonValidacion);
            }
            else
            {
                validacion.Clave = string.Empty;
                validacion.Fecha = string.Empty;
                validacion.IndEstado = "ERROR VALIDANDO : " + response.StatusCode.ToString();
                validacion.RespuestaXml = string.Empty;
            }
            return validacion;
        }

         // METODO PARA CERRAR LA CONEXION CON EL SERVIDOR DE HACIENDA
        public async Task<string> DeconexionGet(string accessToken)
        {
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EngineData.bearer, accessToken);
            HttpResponseMessage response = await client.GetAsync(Valor.UrlDesconexion());
            if (response.IsSuccessStatusCode)
            {
                resultado = response.StatusCode.ToString();
            }
            else
            {
                resultado = "ERROR DESCONECTANDO: " + response.StatusCode.ToString();
            }

            return resultado;
        }

        //METODO PARA OBTENER  TOKEN DE HACIENDA (USUARIO Y PASSWORD RECEPTOR)
        public async Task<string> ObtenerAutentificacionPostReceptor()
        {
            cadenaToken = string.Empty;
            Token token = new Token();
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Valor.UrlAutentificacion());
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>(EngineData.clienteID, Valor.Client_Id()));
            formData.Add(new KeyValuePair<string, string>(EngineData.grantType, Valor.Grant_Type()));
            formData.Add(new KeyValuePair<string, string>(EngineData.userName, Valor.UsernameReceptor()));
            formData.Add(new KeyValuePair<string, string>(EngineData.passWord, Valor.PasswordReceptor()));
            formData.Add(new KeyValuePair<string, string>(EngineData.scoPe, Valor.Scope()));
            request.Content = new FormUrlEncodedContent(formData);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                cadenaToken = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(cadenaToken);
                cadenaToken = token.Access_token;
            }
            else
            {
                cadenaToken = EngineData.falso;
            }

            return cadenaToken;
        }

        //METODO UTILIZADO PARA CREAR , FIRMAR Y ENVIAR EL MENSAJE RECEPTOR 
       public async Task<string> EnviarDocumentoPostReceptor(string accessToken , string [] valoresDocumento,string mensaje,string detalleMensaje)
        {
            urlValidacion = string.Empty;
            string jsonDocumento = string.Empty;
            EngineDocumentoXml EngineDataXml = new EngineDocumentoXml();
            DataMensajeReceptor objDataMensajeReceptor = new DataMensajeReceptor();
            objDataMensajeReceptor = EngineDataXml.SetearMensajeReceptor(valoresDocumento, mensaje, detalleMensaje);

            XmlDocument documentoXml = new XmlDocument();
            documentoXml = EngineDataXml.CrearDocumentoXmlReceptor(objDataMensajeReceptor);
            FirmaXadesEpes MetodoFirmar = new FirmaXadesEpes();
            documentoXml = MetodoFirmar.XadesEpesFirma(documentoXml,Valor.PathCertificadoNeo(),Valor.PinCertificadoNeo());

            CreadorComprobante ComprobanteXml = new CreadorComprobante();
            jsonDocumento = ComprobanteXml.CadenaComprobante(valoresDocumento, documentoXml);

            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EngineData.bearer, accessToken);
            HttpResponseMessage response = await client.PostAsync(Valor.UrlEnvioDocumento(), new StringContent(jsonDocumento, Encoding.UTF8, EngineData.application_json));
            if (response.IsSuccessStatusCode)
            {
                urlValidacion = response.Headers.Location.ToString();
            }
            else
            {
                urlValidacion = EngineData.falso;
            }

            return urlValidacion;
        }

        // METODO PARA OBTENER EL RESUMEN  DE DOCUMENTOS ENVIADOS POR EL EMISOR
        public async Task<string> ResumenDocumentoGet(string accessToken,int offSet,int limit,string tipoIdReceptor)
        {
             string resumenComprobante = string.Empty;
             HttpClient client = new HttpClient();
             System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EngineData.application_json));
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EngineData.bearer, accessToken);
             string urlResumenComprobante = Valor.UrlResumenComprobante() + ParametrosResumen(offSet, limit, tipoIdReceptor);
             HttpResponseMessage response = await client.GetAsync(urlResumenComprobante);
             if (response.IsSuccessStatusCode)
             {
                resumenComprobante = response.Content.ReadAsStringAsync().Result;
             }
             else
             {
                resumenComprobante = EngineData.falso;
             }
            return resumenComprobante;
        }

        //METODO DONDE SE CONCATENAN LOS PARAMETROS PASADOS POR EL GET PARA OBTENER EL RESUMEN
        private string ParametrosResumen(int offSet , int limit , string tipoIdReceptor)
        {
            string getParametros = string.Empty;
            getParametros = EngineData.offset + offSet.ToString() ;
            getParametros = getParametros + EngineData.limit + limit.ToString();
            string tipoEmisorNumero = Valor.TipoEmisor().Substring(Valor.TipoEmisor().Length - 2, 2);//Quito la palabra Item al TipoEmisor
            getParametros = getParametros + EngineData.emisor + tipoEmisorNumero +  Valor.CedulaEmisor();
            getParametros = getParametros + EngineData.receptor + tipoIdReceptor;
            return getParametros;
        }

        //METODO PARA CREAR DOCUMENTOS ELECTRONICOS DE PRUEBA (Factura, NotaDebito , NotaCredito,TiqueteElectronico ) - NO ES NESESARIO DENTRO DEL PROYECTO
        public bool CrearDocumentoElectronico (ComprobanteElectronicoCRI objComprobanteElectronicoCRI, string tipoDocumento)
        {
            bool resultado = false;
            try
            {
                EngineDocumentoXml MetodoCrear = new EngineDocumentoXml();
                XmlDocument documentoXml = new XmlDocument();
                documentoXml = MetodoCrear.CrearDocumentoXml(objComprobanteElectronicoCRI, tipoDocumento);
                documentoXml.Save(@"C:\Users\Public\Documents\" + MetodoCrear.NombreDocumentoXml(documentoXml));
                resultado = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en metodo CrearDocumentoElectronico ", ex);
            }
            return resultado;
        }

    }
}
