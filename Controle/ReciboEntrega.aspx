<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReciboEntrega.aspx.cs" Inherits="JetDev.Control.ReciboEntrega" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: Arial;
        }
        .print > a
        {
            background-color:#008FD7;
            position:absolute;
            color:#fff;
            padding:10px;
            font-weight:bold;
            top:1px;
            left:1px;
        }
    </style>
    <style type="text/css" media="print">
        .ocultoImpressao { display:none;
        }
        .
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ocultoImpressao print">
            <a href="#" onclick="window.print();">Imprimir</a>
        </div>
        <table>
            <tr>
                <td style="width: 800px; border: 1px solid #ccc; padding: 10px; vertical-align: top;" runat="server"  id="ReciboProduto">

                    <table>
                        <tr>
                            <td>
                                <img src="img/jetdev.png" width="100px" />
                            </td>
                            <td>
                                <b>RECIBO DE ENTREGA DE PRODUTOS </b>
                                <a href="#" class="ocultoImpressao" style="display:none;" onclick="document.getElementById('ReciboProduto').style.display='none';">Ocultar</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 12px">Recebi da empresa JetDev Tecnologia e Design Ltda os produtos abaixo relacionados:<br />
                                <br />
                                <asp:Repeater runat="server" ID="rptProdutos">
                                    <ItemTemplate>
                                        <div style="border-bottom: 1px solid #ccc">
                                            <%# Eval("Quantidade").ToString().PadLeft(3,'0') %> - <%# Eval("Descricao") %> - R$ <%# Eval("SubTotal", "{0:N2}") %> <%# Eval("CodSituacao").ToString() == "8" ? "- <b>Entregue</b>" : "- <b>Não Entregue</b>" %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <br />
                                <br />
                                Na assinatura deste confirmo que os produtos foram entregues conforme solicitado atendendo todas as necessidades apresentadas por mim.<br />
                                <br />
                                <span style="float: right">Araxá, <%= DateTime.Today.ToString("dd \\de MMMM \\de yyyy") %></span><br />
                                <br />
                                <br />

                                <span style="width: 100%; text-align: center;">
                                    <center>_____________________________________</center>
                                    <center><asp:Literal ID="litNomeCliente" runat="server"></asp:Literal></center>
                                </span>

                            </td>
                        </tr>
                    </table>

                </td>
                <td style="width: 800px; border: 1px solid #ccc; padding: 10px; vertical-align: top;" runat="server" id="Recibo">
                    <table>
                        <tr>
                            <td>
                                <img src="img/jetdev.png" width="100px" />
                            </td>
                            <td>
                                <b>RECIBO</b>
                                <a href="#" class="ocultoImpressao" style="display:none;" onclick="document.getElementById('Recibo').style.display='none';">Ocultar</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 12px">Recebemos do Sr(a)
                                <asp:Literal ID="litNomeCliente2" runat="server"></asp:Literal>
                                a quantia de R$
                                <asp:Literal ID="litValor" runat="server"></asp:Literal>
                                (<asp:Literal ID="litValorExtenso" runat="server"></asp:Literal>) referente aos produtos listados abaixo:<br />
                                <br />
                                <asp:Repeater runat="server" ID="rptProdutos2">
                                    <ItemTemplate>
                                        <div style="border-bottom: 1px solid #ccc">
                                            <%# Eval("Quantidade").ToString().PadLeft(3,'0') %> - <%# Eval("Descricao") %> - R$ <%# Eval("SubTotal", "{0:N2}") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <br />
                                <asp:Literal runat="server" ID="litValorRestante"></asp:Literal>
                                <br />
                                <br />
                                <span style="float: right">Araxá, <%= DateTime.Today.ToString("dd \\de MMMM \\de yyyy") %></span><br />
                                <br />
                                <br />
                                <span style="width: 100%; text-align: center;">
                                    <center>_____________________________________</center>
                                    <center><asp:Literal ID="litResponsavel" runat="server"></asp:Literal></center>
                                    <center>JetDev Tecnologia e Design Ltda</center>
                                </span>

                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
