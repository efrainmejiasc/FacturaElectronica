using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class Codigos
    {
        private EngineData Valor = EngineData.Instance();

        public string NumeroConsecutivo(string tipoComprobante)
        {
            string numeroConsecutivo = string.Empty;
            numeroConsecutivo = TipoEstablecimiento(numeroConsecutivo);
            numeroConsecutivo = TerminalPuntoVenta(numeroConsecutivo);
            numeroConsecutivo = TipoComprobante(tipoComprobante, numeroConsecutivo);
            numeroConsecutivo = SucursalTerminal(numeroConsecutivo);
            return numeroConsecutivo;
        }

        public string TipoEstablecimiento(string numeroConsecutivo)
        {
            return numeroConsecutivo + Valor.EstablecimientoTipo1();
        }

        public string TerminalPuntoVenta(string numeroConsecutivo)
        {
            return numeroConsecutivo + Valor.TerminalPuntoVenta1();
        }

        public string TipoComprobante(string tipoComprobante, string numeroConsecutivo)
        {
            string codigo = string.Empty;
            switch (tipoComprobante)
            {
                case EngineData.facturaElectronica:
                    codigo = Valor.TipoFactura();
                    break;
                case EngineData.notaDebitoElectronica:
                    codigo = Valor.TipoNotaDebito();
                    break;
                case EngineData.notaCreditoElectronica:
                    codigo = Valor.TipoNotaCredito();
                    break;
                case EngineData.tiqueteElectronico:
                    codigo = Valor.TipoTiquete();
                    break;
                case EngineData.aceptado:
                    codigo = Valor.TipoAceptacion();
                    break;
                case EngineData.aceptadoParcial:
                    codigo = Valor.TipoAceptacionParcial();
                    break;
                case EngineData.rechazado:
                    codigo = Valor.TipoRechazo();
                    break;
            }
            return numeroConsecutivo + codigo;
        }

        public string SucursalTerminal(string numeroConsecutivo)
        {
            return numeroConsecutivo + Valor.NumeroDocumentoElectronico();
        }

        public string Clave(string tipoComprobante,string tipoMensaje)
        {
            string clave = string.Empty;
            clave = CodigoPais(clave);
            clave = Dia(clave);
            clave = Mes(clave);
            clave = Año(clave);
            switch (tipoMensaje)
            {
                case EngineData.msjEmisor:
                    clave = CedulaEmisor(clave);
                    break;
                case EngineData.msjReceptor:
                    clave = CedulaReceptor(clave);
                    break;
            }
            clave = NumeroConsecutivo(clave, tipoComprobante);
            clave = SituacionComprobante(clave);
            clave = AleatorioSeguridad(clave);
            return clave;
        }

        public string CodigoPais(string clave)
        {
            return clave = Valor.CodigoPais();
        }

        public string Dia(string clave)
        {
            int day = 0;
            string dia = string.Empty;
            day = DateTime.Now.Day;
            if (day <= 9)
                dia = Valor.Cero() + day.ToString();
            else
                dia = day.ToString();

            return clave + dia;
        }

        public string Mes(string clave)
        {
            int month = 0;
            string mes = string.Empty;
            month = DateTime.Now.Month;
            if (month <= 9)
                mes = Valor.Cero() + month.ToString();
            else
                mes = month.ToString();

            return clave + mes;
        }

        public string Año(string clave)
        {
            string año = DateTime.Now.Year.ToString();
            año = año.Substring(año.Length - 2, 2);
            return clave + año;
        }

        public string CedulaEmisor(string clave)
        {
            return clave + Valor.CedulaEmisor();
        }

        public string CedulaReceptor(string clave)
        {
            return clave + Valor.CedulaReceptor();
        }

        public string NumeroConsecutivo(string clave, string tipoComprobante)
        {
            string numeroConsecutivo = string.Empty;
            numeroConsecutivo = TipoEstablecimiento(numeroConsecutivo);
            numeroConsecutivo = TerminalPuntoVenta(numeroConsecutivo);
            numeroConsecutivo = TipoComprobante(tipoComprobante, numeroConsecutivo);
            numeroConsecutivo = SucursalTerminal(numeroConsecutivo);
            return clave + numeroConsecutivo;
        }

        public string SituacionComprobante(string clave)
        {
            string codigo = Valor.Uno();
            return clave + codigo;
        }

        public string AleatorioSeguridad(string clave)
        {
            string numero = string.Empty;
            Random generador = new Random();
            numero = generador.Next().ToString();
            numero = numero.Substring(0, 8);
            return clave + numero;
        }
    }
}
