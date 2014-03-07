<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Arquivos.ascx.cs" Inherits="JetDev.Control.ucArquivos" %>
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

    .tabelaArquivos .table {
        margin-bottom: 0px;
    }
</style>
    <div id="mArquivos" class="modal hide">
        <div class="modal-header">
            <asp:LinkButton runat="server" ID="lnkFechar" class="close" OnClick="lnkFechar_Click">
                &times;
            </asp:LinkButton>            
            <h3>Arquivos</h3>
        </div>
        <div class="modal-body">
            
            <div>
                <asp:GridView ID="gvItens" runat="server" AutoGenerateColumns="False" BorderWidth="0px" CssClass="tabelaArquivos table table-striped" GridLines="None" OnRowCommand="gvItens_RowCommand" DataKeyNames="CodItem">
                    <Columns>                        
                        <asp:BoundField HeaderText="Item" DataField="Descricao" />
                        <asp:TemplateField HeaderText="Arquivos" ItemStyle-Width="300px">
                            <ItemTemplate>
                                <table class="tabelaArquivos table table-condensed" style="width: 400px">
                                    <asp:Repeater ID="rptArquivo" DataSource='<%# Bind("Arquivos") %>' runat="server" OnItemCommand="rptArquivo_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div style="width: 250px; overflow: hidden;">
                                                        <asp:LinkButton runat="server" ID="lnkArquivo" CommandArgument='<%# Eval("Codigo") %>' CommandName="DownloadArquivo"><%# Eval("NomeArquivo") %><%# Eval("Extensao") %></asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td><%# Eval("Tamanho") %></td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnExcluir" CommandArgument='<%# Eval("Codigo") %>' CommandName="ExcluirArquivo" Text="X" /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="3">
                                            <asp:LinkButton runat="server" ID="lnkAnexar" CommandName="MostrarAnexo" Text="+ Novo Arquivo" /></td>
                                    </tr>
                                </table>
                                <ajaxToolkit:AsyncFileUpload runat="server" ID="fuAnexo" OnUploadedComplete="fuAnexo_UploadedComplete"
                                    CompleteBackColor="Green" Visible="false" ErrorBackColor="Red"
                                    Width="400px" UploaderStyle="Traditional"
                                    UploadingBackColor="#FFF" ThrobberID="myThrobber" />
                                <asp:Label runat="server" ID="myThrobber" Style="display: none;"> 
                                             <div class="progress progress-striped active">
                                              <div class="bar" style="width: 100%;"></div>
                                            </div>
                                </asp:Label>
                                <asp:Button runat="server" ID="btnCancelar" Text="Voltar" CommandName="CancelarAnexo" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="modal-footer">
        </div>
    </div>