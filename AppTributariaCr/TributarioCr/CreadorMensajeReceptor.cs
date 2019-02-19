using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Modulos.FacturaElectronica;
using System.Globalization;

namespace TributarioCr
{
    public class CreadorMensajeReceptor
    {
        private EngineData Valor = EngineData.Instance();
        private EngineDocumentoXml EngineDocumento = new EngineDocumentoXml();

        public MensajeReceptor CrearEstructuraMensajeReceptor (DataMensajeReceptor value)
        {
            MensajeReceptor MensajeReceptor = new MensajeReceptor
            {
                Clave = value.Clave, //CLAVE DEL DOCUMENTO AL CUAL SE ASOCIA ESTE MESAJE
                NumeroCedulaEmisor = value.NumeroCedulaEmisor,
                FechaEmisionDoc = value.FechaEmisionDoc, // FECHA Y HORA DE LA EMISION DE ESTA RESPUESTA
                Mensaje = (MensajeReceptorMensaje)Enum.Parse(typeof(MensajeReceptorMensaje), value.Mensaje),//Item1(aceptado),Item2(aceptado parcialmente) y Item3(rechazado)
                DetalleMensaje = value.DetalleMensaje, //80 CARACTERES MAXIMOS
                MontoTotalImpuesto = value.MontoTotalImpuesto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                MontoTotalImpuestoSpecified =  EngineDocumento.InsertFieldBoolCantidad(value.MontoTotalImpuesto),
                TotalFactura = value.TotalFactura.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                NumeroCedulaReceptor = value.NumeroCedulaReceptor,
                NumeroConsecutivoReceptor = value.NumeroConsecutivoReceptor,
            };
            return MensajeReceptor;
        }
    }
}

                /*Clave = value.Clave, //CLAVE DEL DOCUMENTO AL CUAL SE ASOCIA ESTE MESAJE
                NumeroCedulaEmisor = value.ComprobanteElectronicoCRIEntidadJuridicaEmisor.Identificacion,
                FechaEmisionDoc = value.FechaEmision, // FECHA Y HORA DE LA EMISION DE ESTA RESPUESTA
                Mensaje = (MensajeReceptorMensaje) Enum.Parse(typeof(MensajeReceptorMensaje), "Item01"),//1(aceptado),2(aceptado parcialmente) y 3(rechazado)
                DetalleMensaje ="Mensaje aceptado", //80 CARACTERES MAXIMOS
                MontoTotalImpuesto = value.TotalImpuesto.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                MontoTotalImpuestoSpecified =  EngineDocumento.InsertFieldBoolCantidad(value.TotalImpuesto),
                TotalFactura = value.TotalComprobante.ToString(EngineData.decimalFormat5, CultureInfo.InvariantCulture),
                NumeroCedulaReceptor = value.ComprobanteElectronicoCRIEntidadJuridicaReceptor.Identificacion,
                NumeroConsecutivoReceptor = value.NumeroConsecutivo,*/
