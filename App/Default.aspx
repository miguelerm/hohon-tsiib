<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HohonTsiib.App.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnFirmar" runat="server" Text="Firmar" OnClick="btnFirmar_Click" />
            <br />
            <asp:TextBox ID="txtArchivoFirmado" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnVerificar" runat="server" Text="Validar firma" OnClick="btnVerificar_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>
