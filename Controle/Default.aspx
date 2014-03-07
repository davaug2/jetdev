<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JetDev.Control._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style>
        table.strapless {
            max-width: 100%;
            background-color: transparent;
            border-collapse: separate;
            border-spacing: 2px;
            border-color: gray;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
        <div class="container" style="width: 1200px;">
            <input type="button" onclick="document.location.href = 'Venda.aspx'" class="btn btn-primary" value="Cadastrar nova venda" />            
            <asp:Button runat="server" ID="btnPgtoDiario" Text="Fechamento diário" CssClass="btn" OnClick="btnPgtoDiario_Click" />
            &nbsp;&nbsp;&nbsp;
            <input type="button" onclick="document.location.href = 'default.aspx'" class="btn" value="Todas as vendas" />            
            <asp:Button runat="server" ID="btnAtrasado" Text="Atrasados" CssClass="btn" OnClick="btnAtrasado_Click"  />
            <asp:Button runat="server" ID="btnEntrega_Dia" Text="Entegas do dia" CssClass="btn" OnClick="btnEntrega_Dia_Click" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvVendas" EventName="RowCommand" />
                </Triggers>
                <ContentTemplate>
                    <asp:GridView ID="gvVendas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" BorderWidth="0px" GridLines="None"  OnRowCommand="gvVendas_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Venda.Codigo" HeaderText="#" />
                            <asp:TemplateField HeaderText="Venda" ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <div style="width: 300px;">
                                        <div style="float: left; text-align: left;">
                                            <b>Cliente:</b><%# Eval("Cliente") %><br />
                                            <b>Total:</b>&nbsp;R$ <%# Eval("Venda.ValorTotal") %>
                                        </div>
                                        <div style="float: right; text-align: right;">
                                            <%# Eval("Venda.Data", "{0:dd/MM/yyyy}") %><br />
                                            <span style="width: 80px; color: <%# decimal.Parse(Eval("Venda.ValorPago").ToString()) != decimal.Parse(Eval("Venda.ValorTotal").ToString()) ? "red" : "blue" %>; font-weight: bold;">Valor pago: R$ <%# Eval("Venda.ValorPago") %></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Produtos" ItemStyle-Width="800px">
                                <HeaderTemplate>
                                    Produtos<br />
                                    Produto  /  Valor  /  Data Entrega  /  Situação
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Repeater runat="server" ID="rptItem" DataSource='<%#Eval("Itens") %>'>
                                        <ItemTemplate>
                                            <div style="width: 200px; float: left;"><%# Eval("Produto") %></div>
                                            <div style="width: 80px; float: left;"><%# Eval("SubTotal", "R$ {0:N2}") %></div>
                                            <div style="width: 90px; float: left;"><%# Eval("ItemVenda.DataPrevisaoEntrega", "{0:dd/MM/yyyy}") %></div>
                                            <div style="width: 190px; float: left; color: <%# Eval("Situacao.Cor") %>; font-weight: bold;"><%# Eval("Situacao.Descricao") %></div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnEditar" class="btn btn-mini" CommandName="editar" Text="Detalhes" CommandArgument='<%# Eval("Venda.Codigo") %>' />&nbsp;
                            <asp:Button runat="server" ID="btnPagamentos" class="btn btn-mini" CommandName="pagamentos" Style="margin-top: 2px;" Text="Pagamentos" CommandArgument='<%# Eval("Venda.Codigo") %>' /><br />
                                    <asp:Button runat="server" ID="btnRecibo" class="btn btn-mini" CommandName="recibo" Style="margin-top: 2px;" Text="Recibo Entrega" CommandArgument='<%# Eval("Venda.Codigo") %>' />
                                    <asp:Button runat="server" ID="btnCancelar" class="btn btn-mini btn-danger" CommandName="cancelar" Style="margin-top: 2px;" Text="Cancelar" CommandArgument='<%# Eval("Venda.Codigo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="mPagamentos" class="modal hide fade">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h3>Pagamentos</h3>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server" ID="upPagamentos">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvVendas" EventName="RowCommand" />
                    </Triggers>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Data</td>
                                <td>Responsável</td>
                                <td>Valor</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDataPagamento" Width="80px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlResponsavel" Width="140px">
                                        <asp:ListItem Value="Eduarda Borges" Text="Eduarda Borges"></asp:ListItem>
                                        <asp:ListItem Value="Messias Aquino" Text="Messias Aquino"></asp:ListItem>
                                        <asp:ListItem Value="Leandro Oliveira" Text="Leandro Oliveira"></asp:ListItem>
                                        <asp:ListItem Value="David Silva" Text="David Silva"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValor" Width="80px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnInserir" OnClick="btnInserir_Click" Text="Inserir" CssClass="btn btn-small btn-primary" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView runat="server" ID="gvPagamentos" AutoGenerateColumns="false" CssClass="table table-striped" BorderWidth="0px" GridLines="None" OnRowDataBound="gvPagamentos_RowDataBound" OnRowCommand="gvPagamentos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Responsavel" HeaderText="Responsável" />
                                <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="SituacaoPagamento.Descricao" HeaderText="Situação" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnRecibo" class="btn btn-mini" CommandName="recibo" Style="margin-top: 2px;" Visible='<%# Eval("CodSituacao").ToString() == "2" ? true : false %>' Text="Emitir Recibo" CommandArgument='<%# Eval("Codigo") %>' />
                                        <asp:Button runat="server" ID="btnPago" class="btn btn-mini btn-success" Visible='<%# Eval("CodSituacao").ToString() == "1" ? true : false %>' CommandName="pago" Style="margin-top: 2px;" Text="Marcar pagamento" CommandArgument='<%# Eval("Codigo") %>' />
                                        <asp:Button runat="server" ID="Button2" class="btn btn-mini btn-danger" Visible='<%# Eval("CodSituacao").ToString() == "2" ? true : false %>' CommandName="naopago" Style="margin-top: 2px;" Text="Desmarcar pagamento" CommandArgument='<%# Eval("Codigo") %>' />
                                        <asp:Button runat="server" ID="btnExcluir" class="btn btn-mini" CommandName="excluir" Style="margin-top: 2px;" Text="Excluir" CommandArgument='<%# Eval("Codigo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
            </div>
        </div>
        <div id="mPgtoDiario" class="modal hide fade">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h3>Fechamento diário</h3>
            </div>
            <div class="modal-body">
                    <asp:GridView runat="server" ID="gvFechamentoDiario" AutoGenerateColumns="false" CssClass="table table-striped" BorderWidth="0px" GridLines="None">
                        <Columns>                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkData" OnClick="lnkData_Click" CommandArgument='<%# Eval("Data") %>' Text='<%# Eval("Data", "{0:dd/MM/yyyy}") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N2}" />                            
                        </Columns>
                    </asp:GridView>
                </div>
            <div class="modal-footer">
            </div>
        </div>
    </form>
</body>
</html>
