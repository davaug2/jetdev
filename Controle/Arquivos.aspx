<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arquivos.aspx.cs" EnableEventValidation="false" Inherits="JetDev.Control.Arquivos" %>

<%@ Register Src="~/Arquivos.ascx" TagPrefix="uc1" TagName="Arquivos" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
        
            <uc1:Arquivos runat="server" id="ucArquivos" />

        </div>
    </form>
</body>
</html>
