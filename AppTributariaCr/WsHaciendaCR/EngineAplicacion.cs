using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using TributarioCr;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;


namespace WsHaciendaCR
{
    public class EngineAplicacion
    {
        private EngineDocumentoXml EngineDocumentoXml = new EngineDocumentoXml();
        private ConsumidorApi ConsumoApi = new ConsumidorApi();
        private Codigos Codigo = new Codigos();

        public bool SubirArchivoXml(FileUpload fileUpXml)
        {
            bool resultado = false;
            try
            {
                if (fileUpXml.HasFile)
                {
                    string nombreDocumento = Path.GetFileName(fileUpXml.FileName);
                    HttpContext.Current.Session["NombreDocumento"] = nombreDocumento;
                    string extension = nombreDocumento.Substring(nombreDocumento.Length - 3, 3);
                    if (extension == EngineData.xml)
                    {
                        if (nombreDocumento.Contains("Factura") || nombreDocumento.Contains("Nota") || nombreDocumento.Contains("Tiquete"))
                        {
                            string folderPath = System.Web.HttpContext.Current.Server.MapPath(EngineData.pathFile);
                            if (!Directory.Exists(folderPath))
                            {
                                System.IO.Directory.CreateDirectory(folderPath);
                            }
                            fileUpXml.SaveAs(folderPath + nombreDocumento);
                            XmlDocument xmlDocument = new XmlDocument();
                            xmlDocument.Load(folderPath + nombreDocumento);
                            HttpContext.Current.Session["Documento"] = xmlDocument;
                            resultado = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error subiendo archivo", ex);
            }
            return resultado;
        }

        public string [] ValoresDocumentoXml()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument = (XmlDocument)HttpContext.Current.Session["Documento"];
            string [] valores = EngineDocumentoXml.ValoresCamposXml(xmlDocument);
            return valores;
        }

        public string NumeroConsecutivoReceptor(string tipoComprobante)
        {
            return Codigo.NumeroConsecutivo(tipoComprobante);
        }

        public async Task FirmaEnvio(string mensaje, string detalleMensaje)
        {
            string accessToken = await ConsumoApi.ObtenerAutentificacionPostReceptor();
            string[] valoresDocumento = HttpContext.Current.Session["ValoresDocumentos"] as string[];
            string urlValidacion = await ConsumoApi.EnviarDocumentoPostReceptor(accessToken, valoresDocumento, mensaje, detalleMensaje);
            string desconexion = await ConsumoApi.DeconexionGet(accessToken);
        }

        public void BorrarArchivo()
        {
            List<string> strFiles = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(EngineData.pathFile), "*").ToList();
            foreach (string fichero in strFiles)
            {
                if (File.Exists(fichero))
                {
                    File.Delete(fichero);
                }
            }
        }

        public void ValoresComboMensaje(DropDownList ddlMensaje)
        {
            ddlMensaje.Items.Add(string.Empty);
            ddlMensaje.Items.Add(EngineData.aceptado);
            ddlMensaje.Items.Add(EngineData.aceptadoParcial);
            ddlMensaje.Items.Add(EngineData.rechazado);
        }

        public TextBox DetalleMensajeMax(TextBox txtBox )
        {
            txtBox.Attributes["maxlength"] = "80";
            return txtBox;
        }

    }
}