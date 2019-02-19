using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class EngineData
    {

        private static EngineData valor;
        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        // ATRIBUTOS UTILES PARA LA APLICACION //
        public const string application_json = "application/json";
        public const string text_xml = "text/xml";
        public const string bearer = "Bearer";
        public const string falso = "false";

        public const string facturaElectronica = "FacturaElectronica";
        public const string notaDebitoElectronica = "NotaDebitoElectronica";
        public const string notaCreditoElectronica = "NotaCreditoElectronica";
        public const string tiqueteElectronico = "TiqueteElectronico";
        public const string mensajeReceptor = "MensajeReceptor";

        /*public const string aceptacionComprobante = "Aceptacion";
        public const string aceptacionParcial = "AceptacionParcial";
        public const string rechazo = "Rechazo";*/

        public const string msjEmisor = "msjEmisor";
        public const string msjReceptor = "msjReceptor";

        public const string dateFormat = "yyyy-MM-ddTHH:mm:ss+hh:mm";
        public const string decimalFormat3 = "#.000";
        public const string decimalFormat5 = "#.00000";
        public const string extensionXml = ".xml";

        //ATRIBUTOS UTILES PARA LA APLICACION AMBIENTE WEB
        public const string aceptado = "ACEPTADO";
        public const string item1 = "Item1";
        public const string aceptadoParcial = "ACEPTACION PARCIAL";
        public const string item2 = "Item2";
        public const string rechazado = "RECHAZADO";
        public const string item3 = "Item3";
        public const string xml = "xml";
        public const string pathFile = "~/File/";
        public const string post = "POST";
        public const string nombreCadenaConexion = "CnxTributaria";
      
        private string[] nombreNodoXML = { "//xs:Clave", "//xs:FechaEmision", "(//xs:Tipo)[1]", "(//xs:Numero)[1]", "(//xs:Tipo)[2]","(//xs:Numero)[2]", "//xs:TotalImpuesto", "//xs:TotalComprobante" };
        public string[] NombreNodoXml() { return nombreNodoXML; }

        private string uriFactura = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica";
        public string UriFactura() { return uriFactura; }

        private string uriNotaCredito = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica";
        public string UriNotaCredito() { return uriNotaCredito; }

        private string uriNotaDebito = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaDebitoElectronica";
        public string UriNotaDebito() { return uriNotaDebito; }

        private string uriTiquete= "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/tiqueteElectronico";
        public string  UriTiquete() { return uriTiquete; }

        private string uriMensasjeReceptor = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor";
        public string UriMensajeReceptor() { return uriMensasjeReceptor; }

        // PROPIEDADES PARA LA CONEXION CON EL SERVIDOR DE HACIENDA ///////////////////////////////////////////////////////////////////////
        public const string clienteID = "client_id";
        public const string passWord = "password";
        public const string grantType = "grant_type";
        public const string userName = "username";
        public const string scoPe = "scope";
        public const string offset = "offset=";
        public const string limit = "&limit=";
        public const string emisor = "&emisor=";
        public const string receptor = "&receptor=";

        private string client_id = "api-stag";
        public string  Client_Id() { return client_id; }

        private string grant_type = "password";
        public string  Grant_Type() { return grant_type; }

        private string username = "cpj-3-101-408861@stag.comprobanteselectronicos.go.cr";
        public string Username() { return username; }

        private string password = "O]^+{6gU-XN4{Y@-!tF*";
        public string Password() { return password; }

        private string scope = "all";
        public string Scope() { return scope; }

        private string usernameReceptor = "cpf-03-0397-0663@stag.comprobanteselectronicos.go.cr";
        public string UsernameReceptor() { return usernameReceptor; }

        private string passwordReceptor = "hL?/)?=k[Y#BZ=)5)8j6";
        public string PasswordReceptor() { return passwordReceptor; }

        private string urlAutentificacion = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";
        public string UrlAutentificacion () { return urlAutentificacion; }

        private string urlEnvioDocumento = "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion/";
        public string UrlEnvioDocumento() { return urlEnvioDocumento; }

        private string urlResumenComprobante = "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/?";
        public string UrlResumenComprobante() { return urlResumenComprobante; }

        private string urlDesconexion = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/logout";
        public string UrlDesconexion() { return urlDesconexion; }

        private string urlDevolucionLlamadaEmisor = "http://efrainemilio-001-site1.etempurl.com/CallBackUrlAppEmisor.ashx";
        public string UrlDevolucionLlamadaEmisor() { return urlDevolucionLlamadaEmisor; }

        private string urlDevolucionLlamadaReceptor = "http://efrainemilio-001-site1.etempurl.com/CallBackUrlAppReceptor.ashx";
        public string UrlDevolucionLlamadaReceptor() { return urlDevolucionLlamadaReceptor; }

        // PROPIEDADES DEL EMISOR DEL DOCUMENTO ///////////////////////////////////////////////////////////////////////////////////////////
        private string nombreEmisor = "NEOTECNOLOGIAS SA";
        public  string NombreEmisor () { return nombreEmisor; }

        //private string numeroIdentificacionEmisor = "303970663"; //BYRON
        private string numeroIdentificacionEmisor = "3101408861"; // NEOTECNOLOGIAS
        public string NumeroIdentificacionEmisor() { return numeroIdentificacionEmisor; }

        private string cedulaEmisor = "003101408861";// NEOTECNOLOGIAS Se usa en caso que la cedula de emisor deba completarse con ceros como prefijo hasta 12 caracteres
        //private string cedulaEmisor = "000303970663";// BYRON
        public string CedulaEmisor() { return cedulaEmisor; }

        private string tipoEmisor = "Item02";
        public string TipoEmisor() { return tipoEmisor; }

        private string provinciaEmisor = "1";
        public string ProvinciaEmisor() { return provinciaEmisor; }

        private string cantonEmisor = "02";
        public string CantonEmisor() { return cantonEmisor; }

        private string distritoEmisor = "01";
        public string DistritoEmisor() { return distritoEmisor; }

        private string barrioEmisor = "01";
        public string BarrioEmisor() { return barrioEmisor; }

        private string otrasSenasEmisor = "CENTRO CORPORATIVO PLAZA ROBLE EDIFICIO LAS TERRAZAS 5TO PISO";
        public string OtrasSenasEmisor() { return otrasSenasEmisor; }

        private string codigoPaisEmisor = "506";
        public string CodigoPaisEmisor() { return codigoPaisEmisor; }

        private string telefonoEmisor = "40701540";
        public string  TelefonoEmisor() { return telefonoEmisor; }

        private string faxEmisor = "40701540";
        public string FaxEmisor() { return faxEmisor; }

        private string emailEmisor = "brojas@gmail.com";
        public string EmailEmisor() { return emailEmisor; }


        // PROPIEDADES DEL RECEPTOR DEL DOCUMENTO ///////////////////////////////////////////////////////////////////////////////////////////
        private string nombreReceptor = "NEOTECNOLOGIAS SA";
        public string NombreReceptor() { return nombreReceptor; }

        private string numeroIdentificacionReceptor = "3101408861";
        public string NumeroIdentificacionReceptor() { return numeroIdentificacionReceptor; }

        private string cedulaReceptor = "003101408861";// Se usa en caso que la cedula de receptor deba completarse con dos ceros como prefijo
        public string CedulaReceptor() { return cedulaReceptor; }

        private string tipoReceptor = "Item02";
        public string TipoReceptor() { return tipoReceptor; }

        private string provinciaReceptor = "1";
        public string ProvinciaReceptor() { return provinciaReceptor; }

        private string cantonReceptor = "02";
        public string CantonReceptor() { return cantonReceptor; }

        private string distritoReceptor = "01";
        public string DistritoReceptor() { return distritoReceptor; }

        private string barrioReceptor = "01";
        public string BarrioReceptor() { return barrioReceptor; }

        private string otrasSenasReceptor = "CENTRO CORPORATIVO PLAZA ROBLE EDIFICIO LAS TERRAZAS 5TO PISO";
        public string OtrasSenasReceptor() { return otrasSenasReceptor; }

        private string codigoPaisReceptor = "506";
        public string CodigoPaisReceptor() { return codigoPaisReceptor; }

        private string telefonoReceptor = "40701540";
        public string TelefonoReceptor() { return telefonoReceptor; }

        private string faxReceptor = "40701540";
        public string FaxReceptor() { return faxReceptor; }

        private string emailReceptor = "brojas@gmail.com";
        public string EmailReceptor() { return emailReceptor; }

        //PROPIEDADES DE LA NORMATIVA DE LA FACTURA
        private string resolucionFactura = "DGT-R-48-2016";
        public string ResolucionFactura() { return resolucionFactura; }

        private string fechaResolucionFactura = "07-10-2016 08:00:00";
        public string  FechaResolucionFactura() { return fechaResolucionFactura; }

        //PROPIEDADES DEL CERTIFICADO DE HACIENDA

        /*private string pathCertificadoWebByron= System.Web.HttpContext.Current.Server.MapPath("~/ServerFile/030397066315.p12");//BYRON
        public string PathCertificadoWebByron() { return pathCertificadoWebByron; }

        private string pathCertificadoWebNeo = System.Web.HttpContext.Current.Server.MapPath("~/ServerFile/310140886114.p12");//NEOTECNOLOGIAS
        public string PathCertificadoWebNeo() { return pathCertificadoWebNeo; }*/

        private string pathCertificadoByron = @"C:\Users\Public\Documents\030397066315.p12";//BYRON
        public  string PathCertificadoByron () { return pathCertificadoByron; }

        private string pathCertificadoNeo= @"C:\Users\Public\Documents\310140886114.p12";//NEOTECNOLOGIAS
        public string PathCertificadoNeo() { return pathCertificadoNeo; }

        private string pinCertificadoByron= "0149"; //BYRON
        public string PinCertificadoByron() { return pinCertificadoByron; }

        private string pinCertificadoNeo= "0149"; //NEOTECNOLOGIAS
        public string PinCertificadoNeo() { return pinCertificadoNeo; }

        //RESOLUCION DE LA FACTURA ELECTRONICA - SE USA PARA TODOS LOS DOCUMENTOS

        private string identificadorPolitica = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/ResolucionComprobantesElectronicosDGT-R-48-2016_4.2.pdf";
        public string IdentificadorPolitica() { return identificadorPolitica; }

        private string hashPolitica = "Ohixl6upD6av8N7pEvDABhEL6hM=";
        public string HashPolitica() { return hashPolitica; }

        //PROPIEDADES PARA LA CONSTRUCCION DEL NUMERO CONSECUTIVO DEL DOCUMENTO

        private string establecimientoTipo1 = "001";//Oficina Central
        public string EstablecimientoTipo1() { return establecimientoTipo1; }

        private string terminalPuntoVenta1 = "00001";//Terminal o Punto de Venta
        public string TerminalPuntoVenta1() { return terminalPuntoVenta1; }

        private string tipoFactura = "01";
        public string TipoFactura() { return tipoFactura; }

        private string tipoNotaDebito = "02";
        public string TipoNotaDebito() { return tipoNotaDebito; }

        private string tipoNotaCredito = "03";
        public string TipoNotaCredito() { return tipoNotaCredito; }

        private string tipoTiquete = "04";
        public string TipoTiquete() { return tipoTiquete; }

        private string tipoAceptacion= "05";
        public string TipoAceptacion() { return tipoAceptacion; }

        private string tipoAceptacionParcial = "06";
        public string TipoAceptacionParcial() { return tipoAceptacionParcial; }

        private string tipoRechazo = "07";
        public string TipoRechazo() { return tipoRechazo; }

        private string numeroDocumentoElectronico = "0000000068"; //  EMISOR Numero Consecutivo Asociado a cada Sucursal o Terminal FACTURA 68, NOTA DEBITO 6 , NOTA CREDITO 6, TIQUETE 8

        ////  RECEPTOR Numero Consecutivo Asociado a cada Sucursal o Terminal FACTURA 5, NOTA DEBITO 0 , NOTA CREDITO 0, TIQUETE 0

        public string  NumeroDocumentoElectronico() { return numeroDocumentoElectronico; }

        private string codigoPais = "506";
        public string CodigoPais() { return codigoPais; }

        private string cero = "0";
        public string Cero() { return cero; }

        private string uno = "1";
        public string Uno() { return uno; }

    }
}
