<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WsHaciendaCR._default" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <style type="text/css">
    .centrado-porcentual
    {
    position: absolute;
    left: 50%;
    top: 45%;
    transform: translate(-50%, -50%);
    -webkit-transform: translate(-50%, -50%);
    }
    </style>
</head>
<body>
     <form id="form1" runat="server">
        <div class="centrado-porcentual">
            <asp:Label ID="lblSeleccionarXml" runat="server" Text="SELECCIONE DOCUMENTO XML A CONFIRMAR" ForeColor="#333333"></asp:Label><br />
            <asp:FileUpload ID="fileUpXml" runat="server" Width="426px" height="30px" BackColor="#CCCCCC" /><br />
            <asp:Button ID="BtnUpFile" runat="server" Text="CARGAR  DOCUMENTO" Width="428px" OnClick="BtnUpFile_Click" Height="35px" BackColor="#999999" /><br /><br />

             <asp:Label ID="lblCampoRequerido" runat="server" Text="* Campo Requerido" ForeColor="Red"></asp:Label><br /><br />

             <asp:Label ID="lblClave" runat="server" Text="CLAVE *" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtClave" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

             <asp:Label ID="lblCedulaEmisor" runat="server" Text="CEDULA DEL EMISOR *" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtCedulaEmisor" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

             <asp:Label ID="lblFechaEmision" runat="server" Text="FECHA EMISION*" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtFechaEmision" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

             <asp:Label ID="lblMensaje" runat="server" Text="MENSAJE *" ForeColor="#333333"></asp:Label><br />
             <asp:DropDownList ID="ddlMensaje" runat="server" Height="22px" Width="418px" BackColor="#CCCCCC" OnSelectedIndexChanged="ddlMensaje_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><br />

             <asp:Label ID="lblDetalleMensaje" runat="server" Text="DETALLE DEL MENSAJE" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtDetalleMensaje" runat="server" Width="418px" height="98px" BackColor="#CCCCCC" MaxLength="80" TextMode="MultiLine"></asp:TextBox><br />

             <asp:Label ID="lblMontoTotalImpueto" runat="server" Text="MONTO TOTAL IMPUESTO" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtMontoTotalImpuesto" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

             <asp:Label ID="lblTotalFactura" runat="server" Text="TOTAL FACTURA *" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtTotalFactura" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

             <asp:Label ID="lblNumeroCedulaReceptor" runat="server" Text="NUMERO CEDULA RECEPTOR *" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtNumeroCedulaReceptor" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br />

              <asp:Label ID="lblNumeroConsecutivoReceptor" runat="server" Text="NUMERO CONSECUTIVO RECEPTOR *" ForeColor="#333333"></asp:Label><br />
             <asp:TextBox ID="txtNumeroConsecutivoReceptor" runat="server" Width="418px" height="22px" BackColor="#CCCCCC"></asp:TextBox><br /><br />

             <asp:Button ID="btnFirmarEnviarDocumento" runat="server" Text="FIRMAR Y ENVIAR DOCUMENTO" Width="428px"  Height="35px" BackColor="#999999" OnClick="btnFirmarEnviarDocumento_Click" AutoPostBack="True"/><br /><br />

           

       </div>

    </form>
</body>
</html>
