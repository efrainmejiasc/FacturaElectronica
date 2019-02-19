using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Modelo.Modulos.FacturaElectronica;
using System.Xml;

namespace TributarioCr
{
    public class EngineDocumentoXml
    {
        private Codigos MCodigo = new Codigos();
        private XmlWriter writer;
        private string numeroConsecutivo = string.Empty;
        private string clave = string.Empty;
        private EngineData Valor = EngineData.Instance();
        private DateTime fechaEmision;

       public XmlDocument  CrearDocumentoXml( ComprobanteElectronicoCRI objComprobanteElectronicoCRI, string tipoDocumento)
        { 
            XmlDocument documentoXml = new XmlDocument();
            XmlDeclaration xmlDeclaracion = documentoXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = documentoXml.DocumentElement;
            documentoXml.InsertBefore(xmlDeclaracion, root);

            switch (tipoDocumento)
             {
                case EngineData.facturaElectronica:
                    CreadorFactura XmlFactura = new CreadorFactura();
                    FacturaElectronica Factura = new FacturaElectronica();
                    Factura= XmlFactura.CrearEstructuraFactura(objComprobanteElectronicoCRI);
                    var navFactura = documentoXml.CreateNavigator();
                    using (writer = navFactura.AppendChild())
                    {
                        var serializer = new XmlSerializer(typeof(FacturaElectronica));
                        serializer.Serialize(writer, Factura);
                    }
                    break;
                case EngineData.notaCreditoElectronica:
                    CreadorNotaCredito XmlNotaCredito = new CreadorNotaCredito();
                    NotaCreditoElectronica NotaCredito = new NotaCreditoElectronica();
                    NotaCredito = XmlNotaCredito.CrearEstructuraNotaCredito(objComprobanteElectronicoCRI);
                    var navNotaCredito = documentoXml.CreateNavigator();
                    using (writer = navNotaCredito.AppendChild())
                    {
                        var serializer = new XmlSerializer(typeof(NotaCreditoElectronica));
                        serializer.Serialize(writer, NotaCredito);
                    }
                    break;
                case EngineData.notaDebitoElectronica:
                    CreadorNotaDebito XmlNotaDebito = new CreadorNotaDebito();
                    NotaDebitoElectronica NotaDebito = new NotaDebitoElectronica();
                    NotaDebito = XmlNotaDebito.CrearEstructuraNotaDebito(objComprobanteElectronicoCRI);
                    var navNotaDebito = documentoXml.CreateNavigator();
                    using (writer = navNotaDebito.AppendChild())
                    {
                        var serializer = new XmlSerializer(typeof(NotaDebitoElectronica));
                        serializer.Serialize(writer, NotaDebito);
                    }
                    break;
                case EngineData.tiqueteElectronico:
                    CreadorTiquete XmlTiquete = new CreadorTiquete();
                    TiqueteElectronico TiqueteElectronico = new TiqueteElectronico();
                    TiqueteElectronico = XmlTiquete.CrearEstructuraTiquete(objComprobanteElectronicoCRI);
                    var navTiquete = documentoXml.CreateNavigator();
                    using (writer = navTiquete.AppendChild())
                    {
                        var serializer = new XmlSerializer(typeof(TiqueteElectronico));
                        serializer.Serialize(writer, TiqueteElectronico);
                    }
                    break;

            }

            return documentoXml; 
        }

        public XmlDocument CrearDocumentoXmlReceptor(DataMensajeReceptor objDataMensajeReceptor)
        {
            XmlDocument documentoXml = new XmlDocument();
            XmlDeclaration xmlDeclaracion = documentoXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = documentoXml.DocumentElement;
            documentoXml.InsertBefore(xmlDeclaracion, root);
            CreadorMensajeReceptor XmlMensaje = new CreadorMensajeReceptor();
            MensajeReceptor MensajeReceptor = new MensajeReceptor();
            MensajeReceptor = XmlMensaje.CrearEstructuraMensajeReceptor(objDataMensajeReceptor);
            var navMensajeReceptor = documentoXml.CreateNavigator();
            using (writer = navMensajeReceptor.AppendChild())
            {
                var serializer = new XmlSerializer(typeof(MensajeReceptor));
                serializer.Serialize(writer, MensajeReceptor);
            }
            return documentoXml;
        }

        public DataMensajeReceptor SetearMensajeReceptor(string [] valoresDocumento , string mensaje , string detalleMensaje)
        {
            //{ "Clave", "FechaEmision", "TipoEmisor", "IdentificacionEmisor", "TipoReceptor","IdentificacionReceptor", "TotalImpuesto", "TotalComprobante" ,"NumeroConsecutivoReceptor"};
            DataMensajeReceptor DataComprobanteReceptor = new DataMensajeReceptor
            {
                Clave = valoresDocumento[0],
                NumeroCedulaEmisor = valoresDocumento[3],
                FechaEmisionDoc = Convert.ToDateTime(DateTime.UtcNow.ToString(EngineData.dateFormat)),
                Mensaje = TipoMensajeReceptor(mensaje),
                DetalleMensaje = detalleMensaje,
                NumeroCedulaReceptor = valoresDocumento[5],
                MontoTotalImpuesto = Convert.ToDecimal(valoresDocumento[6]),
                TotalFactura = Convert.ToDecimal(valoresDocumento[7]),
                NumeroConsecutivoReceptor = valoresDocumento[8]
            };

            return DataComprobanteReceptor;
        }

        public bool InsertFieldBoolCantidad(decimal cantidad)
        {
            bool resultado = false;
            if (cantidad > 0) resultado = true;
            return resultado;
        }

        public bool InsertFieldBoolCadena(string cadena)
        {
            bool resultado = false;
            if (!cadena.Equals(string.Empty)) resultado = true;
            return resultado;
        }

        public string [] ValoresCamposXml(XmlDocument documentoXml)
        {
            string uri = UriDocumentoXml(documentoXml);
            XmlNamespaceManager nS = new XmlNamespaceManager(documentoXml.NameTable);
            nS.AddNamespace("xs", uri);
            XmlNode nodo = null;
            string[] nombreNodoXml = Valor.NombreNodoXml();
            string [] valores = new string [9];
            for (int i = 0; i <= nombreNodoXml.Length - 1 ; i++)
            {
                nodo = documentoXml.SelectSingleNode(nombreNodoXml[i], nS);
                if(nodo != null)
                valores[i] = nodo.InnerText;
                else if (nodo == null)
                valores[i] = string.Empty ;
            }
            return valores;
        }

        public string NombreNodoRaiz (XmlDocument documentoXml)
        {
            return documentoXml.DocumentElement.Name;
        }

        public string UriDocumentoXml(XmlDocument documentoXml)
        {
            string uri = string.Empty;
            string tipoDocumento = NombreNodoRaiz(documentoXml);
            switch (tipoDocumento)
            {
                case EngineData.facturaElectronica:
                    uri = Valor.UriFactura();
                    break;
                case EngineData.notaCreditoElectronica:
                    uri = Valor.UriNotaCredito();
                    break;
                case EngineData.notaDebitoElectronica:
                    uri = Valor.UriNotaDebito();
                    break;
                case EngineData.tiqueteElectronico:
                    uri = Valor.UriTiquete();
                    break;
                case EngineData.mensajeReceptor:
                    uri = Valor.UriMensajeReceptor();
                    break;
            }
            return uri;
        }
    
        public string ValorNodo (XmlDocument documentoXml , string sintaxisXpath)
        {
            string uri = UriDocumentoXml(documentoXml); ;
            XmlNamespaceManager nS = new XmlNamespaceManager(documentoXml.NameTable);
            nS.AddNamespace("xs", uri);
            XmlNode nodo = documentoXml.SelectSingleNode(sintaxisXpath, nS);
            return nodo.InnerText;
        }

        public string NombreDocumentoXml(XmlDocument documentoXml)
        {
            string nombre = string.Empty;
            nombre = NombreNodoRaiz(documentoXml);
            nombre = nombre + ValorNodo(documentoXml, "//xs:Clave");
            nombre = nombre + EngineData.extensionXml;
            return nombre;
        }

        public string TipoMensajeReceptor(string mensaje)
        {
            switch (mensaje)
            {
                case EngineData.aceptado:
                    mensaje = EngineData.item1;
                    break;
                case EngineData.aceptadoParcial:
                    mensaje = EngineData.item2;
                    break;
                case EngineData.rechazado:
                    mensaje = EngineData.item3;
                    break;
            }
            return mensaje;
        }

        public ComprobanteElectronicoCRI ObjComprobanteElectronicoCRI(string tipoDocumento)
        {
           this.clave = MCodigo.Clave(tipoDocumento,EngineData.msjEmisor);
           this.numeroConsecutivo = MCodigo.NumeroConsecutivo(tipoDocumento);
           this.fechaEmision = Convert.ToDateTime(DateTime.UtcNow.ToString(EngineData.dateFormat));

            //***************************************************************************************************

            EngineData Valor = EngineData.Instance();
            List<ComprobanteElectronicoCRIDetalle> Detalle = new List<ComprobanteElectronicoCRIDetalle>();
            ComprobanteElectronicoCRI value = new ComprobanteElectronicoCRI
            {
                //ENCABEZADO
                Clave = this.clave,
                NumeroConsecutivo = this.numeroConsecutivo ,
                FechaEmision = fechaEmision,
                //EMISOR
                ComprobanteElectronicoCRIEntidadJuridicaEmisor = new ComprobanteElectronicoCRIEntidadJuridica
                {
                    Nombre = Valor.NombreEmisor(),
                    Identificacion = Valor.NumeroIdentificacionEmisor(),
                    TipoIdentificacion = Valor.TipoEmisor(),
                    NombreComercial = Valor.NombreEmisor(),
                    CodigoProvincia = Valor.ProvinciaEmisor(),
                    CodigoCanton = Valor.CantonEmisor(),
                    CodigoDistrito = Valor.DistritoEmisor(),
                    CodigoBarrio = Valor.BarrioEmisor(),
                    OtrasSennas = Valor.OtrasSenasEmisor(),
                    CodigoPaisTelefono = Valor.CodigoPaisEmisor(),
                    NumeroTelefono = Valor.TelefonoEmisor(),
                    CodigoPaisFax = Valor.CodigoPaisEmisor(),
                    NumeroFax = Valor.FaxEmisor(),
                    CorreoElectronico = Valor.EmailEmisor()

                   /* Nombre = "Byron Alejandro Rojas Burgos",
                    Identificacion = "303970663",
                    TipoIdentificacion = "Item01",
                    NombreComercial = "Byron Alejandro Rojas Burgos",
                    CodigoProvincia = "3",
                    CodigoCanton = "01",
                    CodigoDistrito = "11",
                    CodigoBarrio = "01",
                    OtrasSennas = "Condominio ALBACETE, casa 29A",
                    CodigoPaisTelefono = "506",
                    NumeroTelefono = "40701590",
                    CodigoPaisFax = "506",
                    NumeroFax = "40701590",
                    CorreoElectronico = "emisor@gmail.com"*/
                },
                //RECEPTOR
                ComprobanteElectronicoCRIEntidadJuridicaReceptor = new ComprobanteElectronicoCRIEntidadJuridica
                {

                    Nombre = "Dental Care",
                    Identificacion = "3001123208",
                    TipoIdentificacion = "Item02",
                    NombreComercial = "Dental Care",
                    CodigoProvincia = "1",
                    CodigoCanton = "01",
                    CodigoDistrito = "01",
                    CodigoBarrio = "01",
                    OtrasSennas = "Amon",
                    CodigoPaisTelefono = "506",
                    NumeroTelefono = "40701590",
                    CodigoPaisFax = "506",
                    NumeroFax = "40701590",
                    CorreoElectronico = "receptor@gmail.com"

                    /*Nombre = Valor.NombreReceptor(),
                    Identificacion = Valor.NumeroIdentificacionReceptor(),
                    TipoIdentificacion = Valor.TipoReceptor(),
                    NombreComercial = Valor.NombreReceptor(),
                    CodigoProvincia = Valor.ProvinciaReceptor(),
                    CodigoCanton = Valor.CantonReceptor(),
                    CodigoDistrito = Valor.DistritoReceptor(),
                    CodigoBarrio = Valor.BarrioReceptor(),
                    OtrasSennas = Valor.OtrasSenasReceptor(),
                    CodigoPaisTelefono = Valor.CodigoPaisReceptor(),
                    NumeroTelefono = Valor.TelefonoReceptor(),
                    CodigoPaisFax = Valor.CodigoPaisReceptor(),
                    NumeroFax = Valor.FaxReceptor(),
                    CorreoElectronico = Valor.EmailReceptor()*/
                },
                //INFORMACION DE LA VENTA
                CondicionVenta = "Item01",
                PlazoCredito = "",
                MedioPago = "Item01",
                //DETALLE 
                ListaComprobanteElectronicoCRIDetalle = DetalleDocumento(Detalle),
                //RESUMEN 
                CodigoMoneda = "CRC",
                TipoCambio = 1,
                TotalServGravados = 97000.6M / 2,
                TotalServExentos = 0,
                TotalMercanciasGravadas = 97000.6M / 2,
                TotalMercanciasExentas = 0,
                TotalGravado = 97000.6M,// TotalServGravado + TotalMercanciasGravadas
                TotalExento = 0,
                TotalVenta = 97000.6M,// TotalGravado + TotalExcento

                TotalDescuentos = 0,//Sumatoria de los descuentos
                TotalVentaNeta = 97000.6M,//TotalVenta - TotalDescuentos
                TotalImpuesto = 9750 + 2860.078M, // Sumatoria Impuestos
                TotalComprobante = 97000.6M + 12610.078M,// TotalVentaNeta + MontoTotalImpuesto
                //REFERENCIA 
                InformacionReferenciaTipoDocumento = "Item01",
                InformacionReferenciaNumero = this.numeroConsecutivo, //string 50
                InformacionReferenciaFechaEmision = this.fechaEmision,
                InformacionReferenciaCodigo = "Item01",
                InformacionReferenciaRazon = "Descripcion del Codigo Utilizado",
                //NORMATIVA 
                NumeroResolucion = Valor.ResolucionFactura(),
                FechaResolucion = Valor.FechaResolucionFactura(),
                //OTROS
                OtrosCodigo = "OTRO CODIGO",
                OtrosOtroTexto = "VALOR DEL OTRO CODIGO"
            };
            return value;
        }

        private List<ComprobanteElectronicoCRIDetalle> DetalleDocumento(List<ComprobanteElectronicoCRIDetalle> Detalle)
        {
            ComprobanteElectronicoCRIDetalle objCriDetalle = new ComprobanteElectronicoCRIDetalle
            {
                NumeroLinea = 1,
                TipoCodigo = "Item04",
                Codigo = "03",
                Cantidad = 3,
                UnidadMedida = "Unid",
                UnidadMedidaComercial = "",
                Detalle = "Unidad",
                PrecioUnitario = 25000,
                MontoTotal = 75000, //Cantidad * PrecioUnitario
                MontoDescuento = 0,
                NaturalezaDescuento = "",
                SubTotal = 75000, // MontoTotal - MontoDescuento
                //Impuestos
                ListaComprobanteElectronicoCRIDetalleImpuesto = DetalleImpuesto(0),
                //*****************************************************************************
                MontoTotalLinea = 75000 + 9750 // SubTotal + MontoImpuesto
            };

            Detalle.Insert(0, objCriDetalle);

            objCriDetalle = new ComprobanteElectronicoCRIDetalle
            {
                NumeroLinea = 2,
                TipoCodigo = "Item01",
                Codigo = "02",
                Cantidad = 2,
                UnidadMedida = "Sp",
                UnidadMedidaComercial = "",
                Detalle = "Servicios Profesionales",
                PrecioUnitario = 11000.30M,
                MontoTotal = 22000.60M,//Cantidad * PrecioUnitario
                MontoDescuento = 0,
                NaturalezaDescuento = "",
                SubTotal = 22000.60M,// MontoTotal - MontoDescuento
                //Impuestos
                ListaComprobanteElectronicoCRIDetalleImpuesto = DetalleImpuesto(1),
                //*****************************************************************************
                MontoTotalLinea = 22000.60M + 2860.078M // SubTotal + MontoImpuesto
            };

            Detalle.Insert(1, objCriDetalle);
            return Detalle;
        }


        private List<ComprobanteElectronicoCRIDetalleImpuesto> DetalleImpuesto(int linea)
        {
            List<ComprobanteElectronicoCRIDetalleImpuesto> j = new List<ComprobanteElectronicoCRIDetalleImpuesto>();

            if (linea == 0)
            {
                ComprobanteElectronicoCRIDetalleImpuesto objCriImpuesto = new ComprobanteElectronicoCRIDetalleImpuesto
                {
                    Codigo = "Item01",
                    Tarifa = 13,
                    Monto = 75000 * 0.13M ,//SubTotal * Tarifa
                    ExoneracionTipoDocumento = "Item01",
                    ExoneracionNumeroDocumento = this.clave.Substring(33,17), //string 17
                    ExoneracionNombreInstitucion = "GOB-CR",
                    ExoneracionFechaEmision = this.fechaEmision,
                    ExoneracionMontoImpuesto = 0,
                    ExoneracionPorcentajeCompra = 0
                };
                j.Add(objCriImpuesto);
            }
            else
            {
               ComprobanteElectronicoCRIDetalleImpuesto objCriImpuesto = new ComprobanteElectronicoCRIDetalleImpuesto
               {
                Codigo = "Item01",
                Tarifa = 13,
                Monto = 22000.60M * 0.13M,
                ExoneracionTipoDocumento = "Item01",
                ExoneracionNumeroDocumento = this.clave.Substring(33,17),//string 17
                ExoneracionNombreInstitucion = "GOB-CR",
                ExoneracionFechaEmision = this.fechaEmision,
                ExoneracionMontoImpuesto = 0,
                ExoneracionPorcentajeCompra = 0,
               };
               j.Add(objCriImpuesto);
            }
            return j;
        }
  
    }
}
