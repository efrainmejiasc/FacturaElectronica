using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using TributarioCr;


namespace WsHaciendaCR
{
    public partial class _default : System.Web.UI.Page
    {
        private EngineAplicacion Funcion = new EngineAplicacion();
        private ConsumidorApi ConsumoApi = new ConsumidorApi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funcion.ValoresComboMensaje(ddlMensaje);// Agrega Valores al combo
                txtDetalleMensaje = Funcion.DetalleMensajeMax(txtDetalleMensaje); //Maximo 80 caracteres para el msj
            }
        }

        protected void BtnUpFile_Click(object sender, EventArgs e)
        {
            bool resultado = Funcion.SubirArchivoXml(fileUpXml);
            if (!resultado) { return; }
            string [] valoresDocumento = Funcion.ValoresDocumentoXml();
            VistaValoresDocumento(valoresDocumento);
        }

        private void VistaValoresDocumento(string [] valor)
        {
            //{ "Clave", "FechaEmision", "TipoEmisor", "IdentificacionEmisor", "TipoReceptor","IdentificacionReceptor", "TotalImpuesto", "TotalComprobante" };
            txtClave.Text = valor[0];
            txtFechaEmision.Text = DateTime.UtcNow.ToString(EngineData.dateFormat);
            txtCedulaEmisor.Text = valor[3];
            txtNumeroCedulaReceptor.Text = valor[5];
            txtMontoTotalImpuesto.Text = valor[6];
            txtTotalFactura.Text = valor[7];
            valor[8] = "";
            Session["ValoresDocumentos"] = valor;
        }

        protected async void btnFirmarEnviarDocumento_Click(object sender, EventArgs e)
        {
            if (ddlMensaje.SelectedIndex != -1 && ddlMensaje.SelectedIndex != 0)
            {
                string mensaje = ddlMensaje.SelectedItem.ToString();
                string detalleMensaje = txtDetalleMensaje.Text;
                await Funcion.FirmaEnvio(mensaje, detalleMensaje);
            }
        }

        protected void ddlMensaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMensaje.SelectedIndex == -1 || ddlMensaje.SelectedIndex == 0 || Session["ValoresDocumentos"] == null )
            {
                txtNumeroConsecutivoReceptor.Text = string.Empty;
                return;
            }
            string[] valor = Session["ValoresDocumentos"] as string[];
            valor[8] = Funcion.NumeroConsecutivoReceptor(ddlMensaje.SelectedItem.ToString());
            txtNumeroConsecutivoReceptor.Text = Funcion.NumeroConsecutivoReceptor(ddlMensaje.SelectedItem.ToString());
            Session["ValoresDocumentos"] = valor;
        }
    }
}