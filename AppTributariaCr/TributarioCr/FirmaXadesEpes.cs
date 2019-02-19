using FirmaXadesNet;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Signature.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TributarioCr
{
    public class FirmaXadesEpes
    {
       private EngineData Valor = EngineData.Instance();
       private EngineDocumentoXml EngineDocumentoXml = new EngineDocumentoXml();

       public XmlDocument  XadesEpesFirma(XmlDocument documentoXml , string pathCertificado , string pinCertificado)
        {
            var xadesService = new XadesService();
            X509Certificate2 MontCertificat = new X509Certificate2(ConvertirCertificadoEnBytes(pathCertificado) , pinCertificado);
            try
            {
                var parametros = new SignatureParameters
                {
                    SignaturePolicyInfo = new SignaturePolicyInfo
                    {
                        PolicyIdentifier = Valor.IdentificadorPolitica(),
                        PolicyHash = Valor.HashPolitica()
                    },
                    SignaturePackaging = SignaturePackaging.ENVELOPED,
                    InputMimeType = EngineData.text_xml,
                };

                using (parametros.Signer = new Signer(MontCertificat))
                {
                    using (MemoryStream fs = new MemoryStream())
                    {
                        documentoXml.PreserveWhitespace = true;
                        documentoXml.Save(fs);
                        fs.Flush();
                        fs.Position = 0;
                        var docFirmado = xadesService.Sign(fs, parametros);
                        documentoXml = docFirmado.Document;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error en metodo XadesEpesFirma", ex);
            }
            documentoXml.Save(@"C:\Users\Public\Documents\" + EngineDocumentoXml.NombreDocumentoXml(documentoXml));
            return documentoXml;
        }

        private byte[] ConvertirCertificadoEnBytes(string pathCertificado)
        {
            byte[] certificadoBytes = System.IO.File.ReadAllBytes(pathCertificado);
            return certificadoBytes;
        }

        public X509Certificate2 ElegirCertificado()
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificates = store.Certificates;
            X509Certificate2Collection foundCertificates = certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2 cert = foundCertificates.OfType<X509Certificate2>().Where(x => x.Subject == "CN=NEOTECNOLOGIAS SOCIEDAD ANONIMA, OU=CPJ, O=PERSONA JURIDICA, C=CR, G=NEOTECNOLOGIAS SOCIEDAD ANONIMA, SN=\"\", SERIALNUMBER=CPJ-3-101-408861").First();
            return cert;
        }

    }
}
