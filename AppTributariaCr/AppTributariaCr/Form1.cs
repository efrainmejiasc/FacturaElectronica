using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TributarioCr;
using Modelo.Modulos.FacturaElectronica;

namespace AppTributariaCr
{
    public partial class Form1 : Form
    {
        private int paso = 0;
        private CreadorComprobante Comprobante = new CreadorComprobante();
        private ConsumidorApi ConsumoApi = new ConsumidorApi();
        private EngineData Valor = EngineData.Instance();
        private string tokenAccess = string.Empty;
        private string urlValidacion = string.Empty;
        private string desconexion = string.Empty;
        private string tipoDocumento = string.Empty;
        private string nombreArchivoXml = string.Empty;
        private string resumenXml = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1500;
            timer2.Interval = 1500;
            timer3.Interval = 1500;
        }

        private void chkFactura_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Name == "chkFactura")
            {
                chkNotaCredito.Checked = false;
                chkNotaDebito.Checked = false;
                chkTicket.Checked = false;
            }
            else if (chk.Name == "chkNotaDebito")
            {
                chkNotaCredito.Checked = false;
                chkFactura.Checked = false;
                chkTicket.Checked = false;
            }
            else if (chk.Name == "chkNotaCredito")
            {
                chkNotaDebito.Checked = false;
                chkFactura.Checked = false;
                chkTicket.Checked = false;
            }
            else if (chk.Name == "chkTicket")
            {
                chkNotaDebito.Checked = false;
                chkFactura.Checked = false;
                chkNotaCredito.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            if (!InternetConnection()){ MensajeSinInternet(); return; }
            if (chkFactura.Checked) { tipoDocumento = "FacturaElectronica"; nombreArchivoXml = "FacturaElectronica.xml"; }
            else if (chkNotaDebito.Checked) { tipoDocumento = "NotaDebitoElectronica"; nombreArchivoXml = "NotaDebitoElectronica.xml"; }
            else if (chkNotaCredito.Checked) { tipoDocumento = "NotaCreditoElectronica"; nombreArchivoXml = "NotaCreditoElectronica.xml"; }
            else if (chkTicket.Checked) { tipoDocumento = "TiqueteElectronico"; nombreArchivoXml = "TiqueteElectronico.xml"; }
            else { MessageBox.Show("Debe elegir un tipo de documento"); return; }

            timer1.Start();
            paso = 1;
        }
        //  104.20.5.119:443
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (paso)
            {
                case 1:
                    timer1.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    MetodoConsumo();
                    break;
                case 2:
                    timer1.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    MetodoConsumo();
                    break;
                case 3:
                    timer1.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    MetodoConsumo();
                    break;
                case 4:
                    timer1.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    MetodoConsumo();
                    break;
            }
        }
        private async void MetodoConsumo()
        {
            try
            {
                switch (paso)
                {
                    case 1:
                        tokenAccess = await ConsumoApi.ObtenerAutentificacionPost();
                        if (tokenAccess != string.Empty && tokenAccess != "false")
                        {
                            Mostrar(tokenAccess);
                            paso++;
                            timer1.Start();
                        }
                        else
                        {
                            MessageBox.Show("ERROR OBTENIENDO AUTENTIFICACION");
                        }
                        break;
                    case 2:
                        ComprobanteElectronicoCRI objComprobanteElectronicoCRI = new ComprobanteElectronicoCRI();
                        TributarioCr.EngineDocumentoXml Metodo = new TributarioCr.EngineDocumentoXml();
                        objComprobanteElectronicoCRI = Metodo.ObjComprobanteElectronicoCRI(tipoDocumento);
                        urlValidacion = await ConsumoApi.EnviarDocumentoPost(tokenAccess, objComprobanteElectronicoCRI, tipoDocumento);
                        if (urlValidacion != string.Empty && urlValidacion != "false")
                        {
                            Mostrar(urlValidacion);
                            paso++;
                            timer1.Start();
                        }
                        else
                        {
                            MessageBox.Show("ERROR ENVIANDO DOCUMENTO");
                        }
                        break;
                    case 3:
                        ConsumidorApi.Validacion validacion = new ConsumidorApi.Validacion();
                        validacion = await ConsumoApi.ValidarDocumentoGet(urlValidacion, tokenAccess);
                        Mostrar(validacion.IndEstado);
                        Mostrar(validacion.RespuestaXml);
                        Mostrar(Comprobante.DecodeBase64(validacion.RespuestaXml));
                        paso++;
                        timer1.Start();
                        break;
                    case 4:
                        desconexion = await ConsumoApi.DeconexionGet(tokenAccess);
                        Mostrar(desconexion);
                        paso = 0;
                        timer1.Stop();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool InternetConnection()
        {
            bool r = false;
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                r = true;
            }
            catch { }
            return r;
        }

        public void Mostrar (string k)
        {
            if (richTextBox1.Text.Equals(string.Empty)) richTextBox1.Text = k;
            else richTextBox1.Text = richTextBox1.Text + Environment.NewLine + k; 
        }

       public void MensajeSinInternet() { MessageBox.Show("** SIN CONEXION A INTERNET **"); paso = 0; }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            timer2.Start();
            paso = 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (paso)
            {
                case 1:
                    timer2.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ObtenerResumen();
                    break;
                case 2:
                    timer2.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ObtenerResumen();
                    break;
                case 3:
                    timer2.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ObtenerResumen();
                    break;
            }
        }

        private async void ObtenerResumen()
        {
            try
            {
                switch (paso)
                {
                    case 1:
                        tokenAccess = await ConsumoApi.ObtenerAutentificacionPost();
                        if (tokenAccess != string.Empty && tokenAccess != "false")
                        {
                            Mostrar(tokenAccess);
                            paso++;
                            timer2.Start();
                        }
                        else
                        {
                            MessageBox.Show("ERROR OBTENIENDO AUTENTIFICACION");
                        }
                        break;
                    case 2:
                        resumenXml = await ConsumoApi.ResumenDocumentoGet(tokenAccess,1,50, "02003001123208");
                        if (resumenXml != string.Empty && resumenXml != "false")
                        {
                            Mostrar(resumenXml);
                            paso++ ;
                            timer2.Start();
                        }
                        else
                        {
                            MessageBox.Show("ERROR OBTENIENDO RESUMEN");
                        }
                        break;
                    case 3:
                        desconexion = await ConsumoApi.DeconexionGet(tokenAccess);
                        Mostrar(desconexion);
                        paso = 0;
                        timer2.Stop();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            if (textBox1.Text == string.Empty) {MessageBox.Show("Debe ingresar la clave del documento a consultar"); return; }
            timer3.Start();
            paso = 1;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            switch (paso)
            {
                case 1:
                    timer3.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ProcesoValidar();
                    break;
                case 2:
                    timer3.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ProcesoValidar();
                    break;
                case 3:
                    timer3.Stop();
                    if (!InternetConnection()) { MensajeSinInternet(); return; }
                    ProcesoValidar();
                    break;
            }

        }

        private async void ProcesoValidar()
        {
            try
            {
                switch (paso)
                {
                    case 1:
                        tokenAccess = await ConsumoApi.ObtenerAutentificacionPost();
                        if (tokenAccess != string.Empty && tokenAccess != "false")
                        {
                            Mostrar(tokenAccess);
                            paso++;
                            timer3.Start();
                        }
                        else
                        {
                            MessageBox.Show("ERROR OBTENIENDO AUTENTIFICACION");
                        }
                        break;
                    case 2:
                        ConsumidorApi.Validacion validacion = new ConsumidorApi.Validacion();
                        validacion = await ConsumoApi.ValidarDocumentoGet(Valor.UrlEnvioDocumento() + textBox1.Text , tokenAccess);
                        Mostrar(validacion.IndEstado);
                        Mostrar(validacion.RespuestaXml);
                        Mostrar(Comprobante.DecodeBase64(validacion.RespuestaXml));
                        paso++;
                        timer3.Start();
                        break;
                    case 3:
                        desconexion = await ConsumoApi.DeconexionGet(tokenAccess);
                        Mostrar(desconexion);
                        paso = 0;
                        timer3.Stop();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            if (chkFactura.Checked) { tipoDocumento = "FacturaElectronica"; nombreArchivoXml = "FacturaElectronica.xml"; }
            else if (chkNotaDebito.Checked) { tipoDocumento = "NotaDebitoElectronica"; nombreArchivoXml = "NotaDebitoElectronica.xml"; }
            else if (chkNotaCredito.Checked) { tipoDocumento = "NotaCreditoElectronica"; nombreArchivoXml = "NotaCreditoElectronica.xml"; }
            else if (chkTicket.Checked) { tipoDocumento = "TiqueteElectronico"; nombreArchivoXml = "TiqueteElectronico.xml"; }
            else { MessageBox.Show("Debe elegir un tipo de documento"); return; }

            ComprobanteElectronicoCRI objComprobanteElectronicoCRI = new ComprobanteElectronicoCRI();
            TributarioCr.EngineDocumentoXml Metodo = new TributarioCr.EngineDocumentoXml();
            objComprobanteElectronicoCRI = Metodo.ObjComprobanteElectronicoCRI(tipoDocumento);
            if (ConsumoApi.CrearDocumentoElectronico(objComprobanteElectronicoCRI, tipoDocumento))
                MessageBox.Show("Documento " + tipoDocumento + " creado satisfactoriamente");
            else
                MessageBox.Show("El documento " + tipoDocumento + " no pudo ser creado");
        }

      
    }
}
