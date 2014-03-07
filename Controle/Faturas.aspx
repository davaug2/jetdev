<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Faturas.aspx.cs" Inherits="JetDev.Control.Faturas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Button runat="server" ID="btnNova" Text="Nova Fatura" CssClass="btn btn-primary" />
            <asp:Button runat="server" ID="btnFaturas" Text="Faturas emitidas" CssClass="btn btn-primary" /><br />
            <asp:MultiView ID="mvFatura" runat="server" ActiveViewIndex="0">
                <asp:View runat="server" ID="vnovaFatura">
                    <fieldset>
                        <legend>Nova Fatura</legend>
                        <label runat="server" class="label label-important" id="label" visible="false"></label>
                        <table>
                            <tr>
                                <td>Produto
                                </td>
                                <td>Quantidade
                                </td>
                                <td>Valor</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlProdutos" runat="server"></asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtProduto" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="btnOutroProduto" runat="server" class="btn btn-small" OnClick="btnOutroProduto_Click">Outro</asp:LinkButton>
                                    <asp:LinkButton ID="btnProdutoCadastrado" runat="server" class="btn btn-small" Visible="false" OnClick="btnProdutoCadastrado_Click">Cadastrado</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQtde" runat="server" Width="60px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtValor" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lnkInserir" class="btn btn-primary btn-small" OnClick="lnkInserir_Click"><i class="icon-ok icon-white"></i> Inserir </asp:LinkButton></td>
                                <td></td>
                            </tr>
                        </table>

                        asd
                        <asp:GridView ID="gvNovaFatura" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowCommand="GridView2_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Descricao" HeaderText="Produto" />
                                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                <asp:BoundField DataField="ValorUnit" HeaderText="Valor Unit." />
                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkExcluir" CssClass="btn btn-mini btn-warning" CommandName="Excluir" CommandArgument='<%# Eval("Codigo") %>'><i class="icon-white icon-remove"></i> Excluir</asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </asp:View>
                <asp:View runat="server" ID="vFaturas">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover" DataSourceID="entidadeDS" EmptyDataText="Nenhuma fatura encontrada">
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                            <asp:BoundField DataField="DataVencimento" HeaderText="DataVencimento" SortExpression="DataVencimento" />
                            <asp:BoundField DataField="DataPagamento" HeaderText="DataPagamento" SortExpression="DataPagamento" />
                            <asp:BoundField DataField="Situacao" HeaderText="Situacao" SortExpression="Situacao" />
                        </Columns>
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>




        </div>
        <asp:ObjectDataSource ID="entidadeDS" runat="server" SelectMethod="Faturas" TypeName="JetDev.Control.WHMCS.WHMCS">
            <SelectParameters>
                <asp:QueryStringParameter Name="codigo" QueryStringField="c" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
