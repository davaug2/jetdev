<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venda.aspx.cs" Inherits="JetDev.Control.TVenda" %>
<%@ Register Src="~/Arquivos.ascx" TagPrefix="uc1" TagName="Arquivos" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/jquery-1.7.1.intellisense.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/bootstrap.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnablePartialRendering="true"></ajaxToolkit:ToolkitScriptManager>
        
        <div class="container">
            <input type="button" onclick="document.location.href = 'default.aspx'" class="btn" value="Voltar" />
            <div class="accordion" id="accordion2">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#cliente">Cliente</a>
                    </div>
                    <div id="cliente" class="accordion-body collapse in">
                        <div class="accordion-inner">
                            <table>
                                <tr>
                                    <td>Nome</td>
                                    <td>Email</td>
                                    <td>CPF/CNPJ</td>
                                    <td>Celular</td>
                                    <td>Tel Fixo</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtNome" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtEmail" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCPF_CNPJ" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTelefone01" Width="100px" Text="034"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTelefone02" Width="100px" Text="034"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#endereco">Dados Cliente</a>
                    </div>
                    <div id="endereco" class="accordion-body collapse">
                        <div class="accordion-inner">
                            <table>
                                <tr>
                                    <td>Endereço</td>
                                    <td>Número</td>
                                    <td>Cidade</td>
                                    <td>UF</td>
                                    <td>CEP</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtEndereco" Width="400px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtNumero" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCidade" Width="40px" Text="Araxá"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtEstado" Width="30px" Text="MG"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCEP" Width="70px" Text="38180000"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>Inscricão Municipal</td>
                                    <td>Inscricao Estadual</td>
                                    <td>Pessoa Contato</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtInscMunicipal" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtInscEstadual" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPessoaContato" Width="100px" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="accordion" id="acordion4">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#acordion4" href="#venda">Venda</a>
                    </div>
                    <div id="venda" class="accordion-body collapse in">
                        <div class="accordion-inner">
                            <table>
                                <tr>
                                    <td>Produto</td>
                                    <td>Quantidade</td>
                                    <td>Valor Unit (R$)</td>
                                    <td>&nbsp;</td>
                                    <td>Custo (R$)</td>
                                    <td>Fornecedor</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtProduto" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtQuantidade" Width="60px" Text="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtValorUnit" Width="70px" Text=""></asp:TextBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCusto" Width="70px" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFornecedor" Width="150px" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>Situação</td>
                                    <td>Data Previsão Entrega</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSituacao" runat="server" Width="300px">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDataPrevisao" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnInserir" Text="Inserir" class="btn btn-primary" OnClick="btnInserir_Click" />
                                    </td>
                                </tr>
                            </table>
                            <a runat="server" id="aa"></a>
                            <asp:GridView ID="gvVenda" runat="server" CssClass="table table-striped" BorderWidth="0px" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvVenda_RowCommand" DataKeyNames="Identificador">
                                <Columns>
                                    <asp:BoundField HeaderText="Produto" DataField="Produto" />
                                    <asp:BoundField HeaderText="Qtde" DataField="ItemVenda.Quantidade" />
                                    <asp:BoundField HeaderText="Valor" DataField="ItemVenda.ValorUnit" DataFormatString="R$ {0:N2}" />
                                    <asp:TemplateField HeaderText="Situacao">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlSituacao" DataTextField="Descricao" DataValueField="Codigo" AutoPostBack="true" OnSelectedIndexChanged="ddlSituacao_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Fornecedor" DataField="Fornecedor" />                                    
                                    <asp:TemplateField HeaderText="DataEntrega">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="hlAlterarData" Text='<%# Eval("ItemVenda.DataPrevisaoEntrega", "{0:dd/MM/yyyy}") %>'></asp:HyperLink>
                                            <asp:TextBox runat="server" ID="txtDataEntrega" Width="80px" Text='<%# Eval("ItemVenda.DataPrevisaoEntrega", "{0:dd/MM/yyyy}") %>' style="display:none;"></asp:TextBox>
                                            <asp:LinkButton runat="server" ID="lnkSalvaDataEntrega" Text="Salvar" OnClick="lnkSalvaDataEntrega_Click" CommandName="DataEntrega" CommandArgument='<%# Eval("Identificador") %>' style="display:none;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="SubTotal" DataField="SubTotal" DataFormatString="R$ {0:N2}" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnExcluir" Text="Excluir" CommandArgument='<%# Eval("Identificador") %>' CommandName="Excluir" CssClass="btn btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="accordion" id="accordion5">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion5" href="#formapagamento">Forma de Pagamento</a>
                    </div>
                    <div id="formapagamento" class="accordion-body collapse in">
                        <div class="accordion-inner">
                            <table>
                                <tr>
                                    <td>Forma Pagamento</td>
                                    <td>Observações</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFormaPagamento" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtObservacoes" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>Valor total:</td>
                                    <td>Valor pago:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblValorTotal" Width="200px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblValorPago" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button runat="server" ID="btnMostrarArquivos" Text="Arquivos" class="btn" OnClick="btnMostrarArquivos_Click" />
            <asp:Button runat="server" ID="btnSalvarVenda" Text="Salvar Venda" class="btn btn-primary" OnClick="btnSalvarVenda_Click" />
        </div>
        </div>
        </div>
        <uc1:Arquivos runat="server" id="ucArquivos" />
    </form>
</body>
</html>
