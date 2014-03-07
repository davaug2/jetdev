<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tarefa.aspx.cs" Inherits="JetDev.Control.Tarefa" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <span>Responsável:</span>
            <asp:DropDownList runat="server" ID="ddlResponsavel" Width="240px" AutoPostBack="true" />
            <div class="accordion" id="accordion2">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#tarefa">Nova Tarefa</a>
                    </div>
                    <div id="tarefa" class="accordion-body collapse in">
                        <table>
                            <tr>
                                <td>Descrição</td>
                                <td>Data Limite</td>
                                <td style="text-align:right;" rowspan="2">
                                    <asp:Button runat="server" ID="btnCadastrar" OnClick="btnCadastrar_Click" class="btn btn-mini btn-primary" Style="margin-top: 2px;" Text="Cadastrar" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDescricao" Width="600px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDataLimite" Text='<%# DateTime.Now.ToString("dd/MM/yyyy hh:mm") %>' Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
            <asp:GridView ID="gvTarefas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" BorderWidth="0px" GridLines="None" OnRowCommand="gvTarefas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CodTarefa" HeaderText="#" />
                    <asp:TemplateField HeaderText="Tarefa" ItemStyle-Width="300px">
                        <ItemTemplate>
                            <%# Eval("Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Limite" ItemStyle-Width="300px">
                        <ItemTemplate>
                            <%# Eval("DataLimite", "{0:dd/MM/yyyy hh:mm}") %>
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

        </div>
    </form>
</body>
</html>
