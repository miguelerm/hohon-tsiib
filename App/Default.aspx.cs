using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HohonTsiib.App
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {

            var rutaCertificado = Server.MapPath("~/App_Data/certificado.pfx");
            var rutaPdf = Server.MapPath("~/App_Data/Cuestionario.pdf");
            var rutaPdfFirmado = Server.MapPath($"~/App_Data/CuestionarioFirmado{DateTime.Now:yyyyMMddHHmmss}.pdf");

            var certificado = new Certificado(rutaCertificado);

            var firmante = new Firmante {
                ArchivoOrigen = rutaPdf,
                ArchivoDestino = rutaPdfFirmado,
                CertificadoAUtilizar = certificado
            };

            firmante.Firmar();

            txtArchivoFirmado.Text = Path.GetFileNameWithoutExtension(rutaPdfFirmado);

        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            var rutaCertificado = Server.MapPath("~/App_Data/certificado.pfx");
            var certificado = new Certificado(rutaCertificado);

            var rutaPdfFirmado = Server.MapPath($"~/App_Data/{txtArchivoFirmado.Text}.pdf");

            if (!File.Exists(rutaPdfFirmado))
            {
                lblResult.Text = "";
                return;
            }

            var notario = new Notario
            {
                Archivo = rutaPdfFirmado,
                CertificadoAUtilizar = certificado
            };

            if (notario.Certificar())
            {
                lblResult.Text = "El archivo fue firmado por el certificado.pfx";
            }
            else
            {
                lblResult.Text = "El archivo no fue firmado por el certificado.pfx";
            }
        }
    }
}