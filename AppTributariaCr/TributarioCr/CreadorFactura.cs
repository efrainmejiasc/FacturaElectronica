using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Modelo.Modulos.FacturaElectronica;

namespace TributarioCr
{
    public class CreadorFactura
    {
        private EngineData Valor = EngineData.Instance();
        private EngineDocumentoXml EngineDocumento = new EngineDocumentoXml();

        public FacturaElectronica CrearEstructuraFactura(ComprobanteElectronicoCRI value)
        {
            FacturaElectronica FacturaElectronica = new FacturaElectronica()
            {
                Clave = value.Clave,
                NumeroConsecutivo = value.NumeroConsecutivo,
                FechaEmision = value.FechaEmision,

                Emisor = new EmisorType
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Nombre,
                    Identificacion = new IdentificacionType
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Identificacion,
                        Tipo = (IdentificacionTypeTipo)Enum.Parse(typeof(IdentificacionTypeTipo), value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.TipoIdentificacion)
                    },
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NombreComercial,
                    Ubicacion = new UbicacionType
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.OtrasSennas
                    },
                    Telefono = new TelefonoType
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroTelefono
                    },
                    Fax = new TelefonoType
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CorreoElectronico
                },
                // FIN EMISOR

                Receptor = new ReceptorType
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Nombre,
                    Identificacion = new IdentificacionType
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Identificacion,
                        Tipo = (IdentificacionTypeTipo)Enum.Parse(typeof(IdentificacionTypeTipo), value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.TipoIdentificacion)
                    },
                    IdentificacionExtranjero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.IdentificacionExtranjero,
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NombreComercial,
                    Ubicacion = new UbicacionType
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.OtrasSennas
                    },
                    Telefono = new TelefonoType
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroTelefono
                    },
                    Fax = new TelefonoType
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CorreoElectronico
                },
                // FIN RECEPTOR

                CondicionVenta = (FacturaElectronicaCondicionVenta)Enum.Parse(typeof(FacturaElectronicaCondicionVenta), value.CondicionVenta),
                PlazoCredito = value.PlazoCredito,
                MedioPago = new FacturaElectronicaMedioPago[] { (FacturaElectronicaMedioPago)Enum.Parse(typeof(FacturaElectronicaMedioPago), value.MedioPago) },
                // FIN INFORMACON DE LA VENTA

                DetalleServicio = DetalleFactura(value).ToArray(), //DETALLE DE LA FACTURA

                ResumenFactura = new FacturaElectronicaResumenFactura()
                {
                    CodigoMoneda = (FacturaElectronicaResumenFacturaCodigoMoneda)Enum.Parse(typeof(FacturaElectronicaResumenFacturaCodigoMoneda), value.CodigoMoneda),
                    CodigoMonedaSpecified = EngineDocumento.InsertFieldBoolCadena(value.CodigoMoneda),
                    TipoCambio = value.TipoCambio.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    TipoCambioSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TipoCambio),
                    TotalServGravados = value.TotalServGravados.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    TotalServGravadosSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalServGravados),
                    TotalServExentos = value.TotalServExentos.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    TotalServExentosSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalServExentos),
                    TotalMercanciasGravadas = value.TotalMercanciasGravadas.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    TotalMercanciasGravadasSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalMercanciasGravadas),
                    TotalMercanciasExentas = value.TotalMercanciasExentas.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    TotalMercanciasExentasSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalMercanciasExentas),
                    TotalGravado = value.TotalGravado.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//TotalServGravados + TotalMercanciasgravadas
                    TotalGravadoSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalGravado),
                    TotalExento = value.TotalExento.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//totalServExcento + totalMercanciasexentas
                    TotalExentoSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalExento),
                    TotalVenta = value.TotalVenta.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//totalGravado + totalExento
                    TotalDescuentos = value.TotalDescuentos.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),// suma de todos los campos de “monto dedescuentos concedidos
                    TotalDescuentosSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalDescuentos),
                    TotalVentaNeta = value.TotalVentaNeta.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//totalVenta - totaDescuentos
                    TotalImpuesto = value.TotalImpuesto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//suma  de  todos los campos denominados Monto del impuesto.
                    TotalImpuestoSpecified = EngineDocumento.InsertFieldBoolCantidad(value.TotalImpuesto),
                    TotalComprobante = value.TotalComprobante.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//totalVentaNeta + totalImpuesto

                },//FIN RESUMEN DE LA FACTURA 

               /* InformacionReferencia = new FacturaElectronicaInformacionReferencia[]
                 {
                   new FacturaElectronicaInformacionReferencia
                   {
                     TipoDoc = (FacturaElectronicaInformacionReferenciaTipoDoc)Enum.Parse(typeof(FacturaElectronicaInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                     Numero = value.InformacionReferenciaNumero,
                     FechaEmision = value.InformacionReferenciaFechaEmision,
                     Codigo = (FacturaElectronicaInformacionReferenciaCodigo)Enum.Parse(typeof(FacturaElectronicaInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                     Razon = value.InformacionReferenciaRazon
                   }
                 },*/
                //FIN REFERENCIA DE LA FACTURA

                Normativa = new FacturaElectronicaNormativa
                {
                    NumeroResolucion = value.NumeroResolucion,
                    FechaResolucion = value.FechaResolucion
                },
                //FIN NORMATIVA DE LA FACTURA

                Otros = new FacturaElectronicaOtros
                {
                    OtroTexto = new FacturaElectronicaOtrosOtroTexto[]
                    {
                        new FacturaElectronicaOtrosOtroTexto
                        {
                          codigo = value.OtrosCodigo,
                          Value = value.OtrosOtroTexto
                        }
                    },
                   /*OtroContenido = new FacturaElectronicaOtrosOtroContenido[]
                     {
                         new FacturaElectronicaOtrosOtroContenido
                         {
                           codigo ="obs",
                         }
                     }*/
                } // FIN OTROS CONTENIDOS

            };// FIN DE LA FACTURA

            return FacturaElectronica;
        }

        private List<FacturaElectronicaLineaDetalle> DetalleFactura(ComprobanteElectronicoCRI value)
        {
            List<FacturaElectronicaLineaDetalle> detalleServicio = new List<FacturaElectronicaLineaDetalle>();

            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 999) { return detalleServicio; }
                FacturaElectronicaLineaDetalle linea = new FacturaElectronicaLineaDetalle
                {
                    NumeroLinea = value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea.ToString(),
                    Codigo = new CodigoType[]
                      {
                          new CodigoType
                          {
                            Tipo = (CodigoTypeTipo)Enum.Parse(typeof(CodigoTypeTipo), value.ListaComprobanteElectronicoCRIDetalle[i].TipoCodigo),
                            Codigo = value.ListaComprobanteElectronicoCRIDetalle[i].Codigo
                          }
                      },

                    Cantidad = value.ListaComprobanteElectronicoCRIDetalle[i].Cantidad.ToString(EngineData.decimalFormat3, CultureInfo.InvariantCulture),// 13 enteros 3 decimales
                    UnidadMedida = (UnidadMedidaType)Enum.Parse(typeof(UnidadMedidaType), value.ListaComprobanteElectronicoCRIDetalle[i].UnidadMedida),
                    UnidadMedidaComercial = value.ListaComprobanteElectronicoCRIDetalle[i].UnidadMedidaComercial,
                    Detalle = value.ListaComprobanteElectronicoCRIDetalle[i].Detalle,
                    PrecioUnitario = value.ListaComprobanteElectronicoCRIDetalle[i].PrecioUnitario.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//13enteros 5 decimales 
                    MontoTotal = value.ListaComprobanteElectronicoCRIDetalle[i].MontoTotal.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//cantidad * precioUnitario 13enteros 5 decimales 
                    MontoDescuento = value.ListaComprobanteElectronicoCRIDetalle[i].MontoDescuento.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//13enteros 5 decimales 
                    MontoDescuentoSpecified = EngineDocumento.InsertFieldBoolCantidad(value.ListaComprobanteElectronicoCRIDetalle[i].MontoDescuento),
                    NaturalezaDescuento = value.ListaComprobanteElectronicoCRIDetalle[i].NaturalezaDescuento,
                    SubTotal = value.ListaComprobanteElectronicoCRIDetalle[i].SubTotal.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),//montoTotal - montoDescuento

                    Impuesto = ImpuestoFactura(value, value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea - 1 ).ToArray(),

                    MontoTotalLinea = value.ListaComprobanteElectronicoCRIDetalle[i].MontoTotalLinea.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture)
                };

                detalleServicio.Add(linea);
            }

            return detalleServicio;
        }

        private List<ImpuestoType> ImpuestoFactura(ComprobanteElectronicoCRI value, int numeroLinea)
        {
            List<ImpuestoType> detalleImpuesto = new List<ImpuestoType>();

            if (value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count == 0) { return detalleImpuesto; }
            for (int n = 0; n <= value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count - 1; n++)
            {
                ImpuestoType lineaImpuesto = new ImpuestoType
                {
                    Codigo = (ImpuestoTypeCodigo)Enum.Parse(typeof(ImpuestoTypeCodigo), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Codigo),//Codigo del Impuesto
                    Tarifa = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Tarifa.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Monto = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Monto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Exoneracion = new ExoneracionType
                    {
                        TipoDocumento = (ExoneracionTypeTipoDocumento)Enum.Parse(typeof(ExoneracionTypeTipoDocumento), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionTipoDocumento),
                        NumeroDocumento = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionNumeroDocumento,
                        NombreInstitucion = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionNombreInstitucion,
                        FechaEmision = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionFechaEmision,
                        MontoImpuesto = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionMontoImpuesto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                        PorcentajeCompra = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionPorcentajeCompra.ToString()
                    }
                };
                detalleImpuesto.Add(lineaImpuesto);
            }
            return detalleImpuesto;
        }

        private List<FacturaElectronicaInformacionReferencia> InformacionReferenciaFactura(ComprobanteElectronicoCRI value)
        {
            List<FacturaElectronicaInformacionReferencia> informacionReferencia = new List<FacturaElectronicaInformacionReferencia>();
            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 9) { return informacionReferencia; }
                FacturaElectronicaInformacionReferencia lineaReferencia = new FacturaElectronicaInformacionReferencia
                {
                    TipoDoc = (FacturaElectronicaInformacionReferenciaTipoDoc)Enum.Parse(typeof(FacturaElectronicaInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                    Numero = value.InformacionReferenciaNumero,
                    FechaEmision = value.InformacionReferenciaFechaEmision,
                    Codigo = (FacturaElectronicaInformacionReferenciaCodigo)Enum.Parse(typeof(FacturaElectronicaInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                    Razon = value.InformacionReferenciaRazon
                };
            }
            return informacionReferencia;
        }

    }
}
