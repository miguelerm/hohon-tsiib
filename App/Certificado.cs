using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HohonTsiib.App
{
    public class Certificado
    {
        /// <summary>
        /// Llave privada
        /// </summary>
        public AsymmetricKeyParameter Key { get; private set; }

        /// <summary>
        /// Certificados
        /// </summary>
        public X509Certificate[] Chain { get; private set; }

        public Certificado(string rutaCompletaDelPfx)
        {
            using (var file = File.OpenRead(rutaCompletaDelPfx))
            {
                var password = new char[] { /* password en blanco para este certificado */ };
                var store = new Pkcs12Store(file, password);
                var alias = GetCertificateAlias(store);

                Key = store.GetKey(alias).Key;
                Chain = store.GetCertificateChain(alias).Select(x => x.Certificate).ToArray();
            }
        }

        private static string GetCertificateAlias(Pkcs12Store store)
        {
            foreach (string currentAlias in store.Aliases)
            {
                if (store.IsKeyEntry(currentAlias))
                {
                    return currentAlias;
                }
            }

            return null;
        }
    }
}