using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HohonTsiib.App
{
    public class Firmante
    {

        /// <summary>
        /// Ruta absoluta del archivo pdf que se quiere firmar.
        /// </summary>
        public string ArchivoOrigen { get; set; }

        /// <summary>
        /// Ruta absoluta en donde se guardará el nuevo archivo pdf ya firmado.
        /// </summary>
        public string ArchivoDestino { get; set; }

        /// <summary>
        /// Certificado
        /// </summary>
        public Certificado CertificadoAUtilizar { get; set; }

        public Guid Firmar()
        {
            var codigo = Guid.NewGuid();

            float signatureWidth = 108;
            float signatureHeight = 32;

            using (var reader = new PdfReader(ArchivoOrigen))
            using (var writer = new FileStream(ArchivoDestino, FileMode.Create, FileAccess.Write))
            using (var stamper = PdfStamper.CreateSignature(reader, writer, '\0', null, true))
            {

                var signatureBox = reader.GetCropBox(1);
                var signature = stamper.SignatureAppearance;

                var signatureLocation = new Rectangle(signatureBox.GetRight(signatureWidth), signatureBox.GetBottom(0), signatureBox.GetRight(0), signatureBox.GetBottom(signatureHeight));

                signature.SetVisibleSignature(signatureLocation, 1, signature.GetNewSigName());
                signature.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
                signature.Reason = codigo.ToString();
                signature.ReasonCaption = "Código: ";
                
                var signatureKey = new PrivateKeySignature(CertificadoAUtilizar.Key, DigestAlgorithms.SHA256);
                var signatureChain = CertificadoAUtilizar.Chain;

                MakeSignature.SignDetached(signature, signatureKey, signatureChain, null, null, null, 0, CryptoStandard.CADES);

            }

            return codigo;

        }
    }
}