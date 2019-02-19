using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Modulos.FacturaElectronica;

namespace TributarioCr
{
   public class CreadorTiquete
    {
        private EngineData Valor = EngineData.Instance();
        private EngineDocumentoXml EngineDocumento = new EngineDocumentoXml();

        public TiqueteElectronico CrearEstructuraTiquete(ComprobanteElectronicoCRI value)
        {
            TiqueteElectronico  TiqueteElectronico = new TiqueteElectronico()
            {
                Clave = value.Clave,
                NumeroConsecutivo = value.NumeroConsecutivo,
                FechaEmision = value.FechaEmision,

                Emisor = new EmisorTypeT
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Nombre,
                    Identificacion = new IdentificacionTypeT
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Identificacion,
                        Tipo = (IdentificacionTypeTipoT)Enum.Parse(typeof(IdentificacionTypeTipoT), value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.TipoIdentificacion)
                    },
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NombreComercial,
                    Ubicacion = new UbicacionTypeT
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.OtrasSennas
                    },
                    Telefono = new TelefonoTypeT
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroTelefono
                    },
                    Fax = new TelefonoTypeT
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.CorreoElectronico
                },
                // FIN EMISOR

                Receptor = new ReceptorTypeT
                {
                    Nombre = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Nombre,
                    Identificacion = new IdentificacionTypeT
                    {
                        Numero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Identificacion,
                        Tipo = (IdentificacionTypeTipoT)Enum.Parse(typeof(IdentificacionTypeTipoT), value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.TipoIdentificacion)
                    },
                    IdentificacionExtranjero = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.IdentificacionExtranjero,
                    NombreComercial = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NombreComercial,
                    Ubicacion = new UbicacionTypeT
                    {
                        Provincia = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoProvincia,
                        Canton = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoCanton,
                        Distrito = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoDistrito,
                        Barrio = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoBarrio,
                        OtrasSenas = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.OtrasSennas
                    },
                    Telefono = new TelefonoTypeT
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisTelefono,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroTelefono
                    },
                    Fax = new TelefonoTypeT
                    {
                        CodigoPais = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CodigoPaisFax,
                        NumTelefono = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.NumeroFax
                    },
                    CorreoElectronico = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.CorreoElectronico
                },
                // FIN RECEPTOR

                CondicionVenta = (TiqueteElectronicoCondicionVenta)Enum.Parse(typeof(TiqueteElectronicoCondicionVenta), value.CondicionVenta),
                PlazoCredito = value.PlazoCredito,
                MedioPago = new TiqueteElectronicoMedioPago[] { (TiqueteElectronicoMedioPago)Enum.Parse(typeof(TiqueteElectronicoMedioPago), value.MedioPago) },
                // FIN INFORMACON DE LA VENTA

                DetalleServicio = DetalleTiquete(value).ToArray(),  //DETALLE DEL TIQUETE

                ResumenFactura = new TiqueteElectronicoResumenFactura()
                {
                    CodigoMoneda = (TiqueteElectronicoResumenFacturaCodigoMoneda)Enum.Parse(typeof(TiqueteElectronicoResumenFacturaCodigoMoneda), value.CodigoMoneda),
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

                },//FIN RESUMEN DEL TIQUETE

                InformacionReferencia = new TiqueteElectronicoInformacionReferencia[]
                 {
                   new TiqueteElectronicoInformacionReferencia
                   {
                     TipoDoc = (TiqueteElectronicoInformacionReferenciaTipoDoc)Enum.Parse(typeof(TiqueteElectronicoInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                     Numero = value.InformacionReferenciaNumero,
                     FechaEmision = value.InformacionReferenciaFechaEmision,
                     Codigo = (TiqueteElectronicoInformacionReferenciaCodigo)Enum.Parse(typeof(TiqueteElectronicoInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                     Razon = value.InformacionReferenciaRazon
                   }
                 },
                //FIN REFERENCIA DEL TIQUETE

                Normativa = new TiqueteElectronicoNormativa
                {
                    NumeroResolucion = value.NumeroResolucion,
                    FechaResolucion = value.FechaResolucion
                }, 
                //FIN NORMATIVA DEL TIQUETE

                 Otros = new TiqueteElectronicoOtros
                 {
                     OtroTexto = new TiqueteElectronicoOtrosOtroTexto[]
                     {
                         new TiqueteElectronicoOtrosOtroTexto
                         {
                           codigo = value.OtrosCodigo,
                           Value = value.OtrosOtroTexto
                         }
                     },
                     /*OtroContenido = new TiqueteElectronicoOtrosOtroContenido[]
                      {
                           new TiqueteElectronicoOtrosOtroContenido
                           {
                             codigo ="obs",
                           }
                     }*/
                  } // FIN OTROS CONTENIDOS

            };// FIN DEL TIQUETE


            return TiqueteElectronico ;
        }

        public List<TiqueteElectronicoLineaDetalle> DetalleTiquete (ComprobanteElectronicoCRI value)
        {
            List<TiqueteElectronicoLineaDetalle> detalleServicio = new List<TiqueteElectronicoLineaDetalle>();

            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 999) { return detalleServicio; }
                TiqueteElectronicoLineaDetalle linea = new TiqueteElectronicoLineaDetalle
                {
                    NumeroLinea = value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea.ToString(),
                    Codigo = new CodigoTypeT[]
                      {
                          new CodigoTypeT
                          {
                            Tipo = (CodigoTypeTipoT)Enum.Parse(typeof(CodigoTypeTipoT), value.ListaComprobanteElectronicoCRIDetalle[i].TipoCodigo),
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

                    Impuesto = ImpuestoTiquete(value,value.ListaComprobanteElectronicoCRIDetalle[i].NumeroLinea - 1).ToArray(),

                    MontoTotalLinea = value.ListaComprobanteElectronicoCRIDetalle[i].MontoTotalLinea.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture)
                };

                detalleServicio.Add(linea);
            }

            return detalleServicio;
        }

        public List<ImpuestoTypeT> ImpuestoTiquete(ComprobanteElectronicoCRI value,int numeroLinea)
        {
            List<ImpuestoTypeT> detalleImpuesto = new List<ImpuestoTypeT>();
            if (value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count == 0) { return detalleImpuesto; }
            for (int n = 0; n <= value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto.Count - 1; n++)
            {
                ImpuestoTypeT lineaImpuesto = new ImpuestoTypeT
                {
                    Codigo = (ImpuestoTypeCodigoT)Enum.Parse(typeof(ImpuestoTypeCodigoT), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Codigo),
                    Tarifa = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Tarifa.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Monto = value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].Monto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                    Exoneracion = new ExoneracionTypeT
                    {
                        TipoDocumento = (ExoneracionTypeTipoDocumentoT)Enum.Parse(typeof(ExoneracionTypeTipoDocumentoT), value.ListaComprobanteElectronicoCRIDetalle[numeroLinea].ListaComprobanteElectronicoCRIDetalleImpuesto[n].ExoneracionTipoDocumento),
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

        private List<TiqueteElectronicoInformacionReferencia> InformacionReferenciaNotaDebito(ComprobanteElectronicoCRI value)
        {
            List<TiqueteElectronicoInformacionReferencia> informacionReferencia = new List<TiqueteElectronicoInformacionReferencia>();
            for (int i = 0; i <= value.ListaComprobanteElectronicoCRIDetalle.Count - 1; i++)
            {
                if (i > 9) { return informacionReferencia; }
                TiqueteElectronicoInformacionReferencia lineaReferencia = new TiqueteElectronicoInformacionReferencia
                {
                    TipoDoc = (TiqueteElectronicoInformacionReferenciaTipoDoc)Enum.Parse(typeof(TiqueteElectronicoInformacionReferenciaTipoDoc), value.InformacionReferenciaTipoDocumento),
                    Numero = value.InformacionReferenciaNumero,
                    FechaEmision = value.InformacionReferenciaFechaEmision,
                    Codigo = (TiqueteElectronicoInformacionReferenciaCodigo)Enum.Parse(typeof(TiqueteElectronicoInformacionReferenciaCodigo), value.InformacionReferenciaCodigo),
                    Razon = value.InformacionReferenciaRazon
                };
            }
            return informacionReferencia;
        }

    }
}
