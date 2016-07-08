using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HohonTsiib.App
{
    public class Notario
    {

        /// <summary>
        /// Ruta absoluta del archivo del cual se verificará la firma
        /// </summary>
        public string Archivo { get; set; }

        /// <summary>
        /// Certificado
        /// </summary>
        public Certificado CertificadoAUtilizar { get; set; }

        public bool Certificar()
        {
            using(var reader = new PdfReader(Archivo))
            {
                var campos = reader.AcroFields;
                var nombresDefirmas = campos.GetSignatureNames();
                foreach (var nombre in nombresDefirmas)
                {
                    var firma = campos.VerifySignature(nombre);

                    if (!firma.Verify()) continue;

                    foreach (var certificado in firma.Certificates)
                    {
                        
                        foreach (var certificadoDeConfianza in CertificadoAUtilizar.Chain)
                        {
                            try
                            {
                                certificado.Verify(certificadoDeConfianza.GetPublicKey());
                                // Si llega hasta aquí al menos una firma fue realizada con el certificado del sistema.
                                return true;
                            }
                            catch (InvalidKeyException)
                            {
                                continue;
                            }
                            catch (Exception ex)
                            {
                                Trace.TraceError("Error: {0}", ex);
                                continue;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}