using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Modulos.FacturaElectronica;

namespace TributarioCr
{
    public class CreadorNotaCredito
    {
        private EngineData Valor = EngineData.Instance();
        private EngineDocumentoXml EngineDocumento = new EngineDocumentoXml();

        public NotaCreditoElectronica CrearEstructuraNotaCredito(ComprobanteElectronicoCRI value)
        {
            NotaCreditoElectronica NotaCreditoElectronica = new NotaCreditoElectronica()
            {
                Clave = value.Clave,
                NumeroConsecutivo = value.NumeroConsecutivo,
                FechaEmision = value.FechaEmision,

                Emisor = new EmisorTypeNC
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Nombre,
                    Identificacion = new IdentificacionTypeNC
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Identificacion,
                        Tipo = (IdentificacionTypeTipoNC)Enum.Parse(typeof(IdentificacionTypeTipoNC), value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.TipoIdentificacion)
                    },
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NombreComercial,
                    Ubicacion = new UbicacionTypeNC
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.OtrasSennas
                    },
                    Telefono = new TelefonoTypeNC
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroTelefono
                    },
                    Fax = new TelefonoTypeNC
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CorreoElectronico
                },
                //FIN EMISOR
                Receptor = new ReceptorTypeNC
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Nombre,
                    Identificacion = new IdentificacionTypeNC
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Identificacion,
                        Tipo = (IdentificacionTypeTipoNC)Enum.Parse(typeof(IdentificacionTypeTipoNC), value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.TipoIdentificacion)
                    },
                    IdentificacionExtranjero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.IdentificacionExtranjero,
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NombreComercial,
                    Ubicacion = new UbicacionTypeNC
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.OtrasSennas
                    },
                    Telefono = new TelefonoTypeNC
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroTelefono
                    },
                    Fax = new TelefonoTypeNC
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CorreoElectronico
                },
                // FIN RECEPTOR
                CondicionVenta = (NotaCreditoElectronicaCondicionVenta)Enum.Parse(typeof(NotaCreditoElectronicaCondicionVenta), value.CondicionVenta),
                PlazoCredito = value.PlazoCredito,
                MedioPago = new NotaCreditoElectronicaMedioPago[] { (NotaCreditoElectronicaMedioPago)Enum.Parse(typeof(NotaCreditoElectronicaMedioPago), value.MedioPago) },
                // FIN INFORMACON DE LA VENTA

                DetalleServicio = DetalleNotaCredito(value).ToArray(), //DETALLE DE LA NOTA DE CREDITO

                ResumenFactura = new NotaCreditoElectronicaResumenFactura()
                {
                    CodigoMoneda = (NotaCreditoElectronicaResumenFacturaCodigoMoneda)Enum.Parse(typeof(NotaCreditoElectronicaResumenFacturaCodigoMoneda), value.CodigoMoneda),
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

                InformacionReferencia = new NotaCreditoElectronicaInformacionReferencia[]
                 {
                   new NotaCreditoElectronicaInformacionReferencia
                   {
                     TipoDoc = (NotaCreditoElectronicaInformacionReferenciaTipoDoc)Enum.Parse(typeof(NotaCreditoElectronicaInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                     Numero = value.InformacionReferenciaNumero,
                     FechaEmision = value.InformacionReferenciaFechaEmision,
                     Codigo = (NotaCreditoElectronicaInformacionReferenciaCodigo)Enum.Parse(typeof(NotaCreditoElectronicaInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                     Razon = value.InformacionReferenciaRazon
                   }
                 }, 
                //FIN REFERENCIA DE LA FACTURA

                Normativa = new NotaCreditoElectronicaNormativa
                {
                    NumeroResolucion = value.NumeroResolucion,
                    FechaResolucion = value.FechaResolucion
                },
                //FIN NORMATIVA DE LA NOTA DE CREDITO

                Otros = new NotaCreditoElectronicaOtros
                {
                    OtroTexto = new NotaCreditoElectronicaOtrosOtroTexto[]
                    {
                        new NotaCreditoElectronicaOtrosOtroTexto
                        {
                           codigo = value.OtrosCodigo,
                           Value = value.OtrosOtroTexto
                        }
                    },
                   /* OtroContenido = new NotaCreditoElectronicaOtrosOtroContenido[]
                     {
                         new NotaCreditoElectronicaOtrosOtroContenido
                         {
                           codigo ="obs",
                         }
                     }*/
            } // FIN OTROS CONTENIDOS

            };
            return NotaCreditoElectronica;

        }

        public List<NotaCreditoElectronicaLineaDetalle> DetalleNotaCredito(ComprobanteElectronicoCRI value)
        {
            List<NotaCreditoElectronicaLineaDetalle> detalleServicio = new List<NotaCreditoElectronicaLineaDetalle>();

            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 999) { return detalleServicio; }
                NotaCreditoElectronicaLineaDetalle linea = new NotaCreditoElectronicaLineaDetalle
                {
                    NumeroLinea = value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea.ToString(),
                    Codigo = new CodigoTypeNC[]
                      {
                          new CodigoTypeNC
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

                    Impuesto = ImpuestoNotaCredito(value, value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea - 1).ToArray(),

                    MontoTotalLinea = value.ListaComprobanteElectronicoCRIDetalle[i].MontoTotalLinea.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture)
                };

                detalleServicio.Add(linea);
            }

            return detalleServicio;
        }

        public List<ImpuestoTypeNC> ImpuestoNotaCredito(ComprobanteElectronicoCRI value, int numeroLinea)
        {
            List<ImpuestoTypeNC> detalleImpuesto = new List<ImpuestoTypeNC>();
            if (value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count == 0) { return detalleImpuesto; }
            for (int n = 0; n <= value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count - 1; n++)
            {
                ImpuestoTypeNC lineaImpuesto = new ImpuestoTypeNC
                {
                    Codigo = (ImpuestoTypeCodigoNC)Enum.Parse(typeof(ImpuestoTypeCodigoNC), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Codigo),
                    Tarifa = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Tarifa.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Monto = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Monto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Exoneracion = new ExoneracionTypeNC
                    {
                        TipoDocumento = (ExoneracionTypeTipoDocumentoNC)Enum.Parse(typeof(ExoneracionTypeTipoDocumentoNC), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionTipoDocumento),
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

        private List<NotaCreditoElectronicaInformacionReferencia> InformacionReferenciaNotaCredito(ComprobanteElectronicoCRI value)
        {
            List<NotaCreditoElectronicaInformacionReferencia> informacionReferencia = new List<NotaCreditoElectronicaInformacionReferencia>();
            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 9) { return informacionReferencia; }
                NotaCreditoElectronicaInformacionReferencia lineaReferencia = new NotaCreditoElectronicaInformacionReferencia
                {
                    TipoDoc = (NotaCreditoElectronicaInformacionReferenciaTipoDoc)Enum.Parse(typeof(NotaCreditoElectronicaInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                    Numero = value.InformacionReferenciaNumero,
                    FechaEmision = value.InformacionReferenciaFechaEmision,
                    Codigo = (NotaCreditoElectronicaInformacionReferenciaCodigo)Enum.Parse(typeof(NotaCreditoElectronicaInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                    Razon = value.InformacionReferenciaRazon
                };
            }
            return informacionReferencia;
        }

    }
}
