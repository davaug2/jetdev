using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JetDev.Control
{
    public partial class TVenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PreRender += Venda_PreRender;
            if (!IsPostBack)
            {
                Session["Itens"] = null;
                CarregarVenda();
                txtDataPrevisao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                using (var model = new controleEntities())
                {
                    ddlSituacao.DataSource = model.Situacao.OrderBy(i => i.Ordem).ToList();
                    ddlSituacao.DataTextField = "Descricao";
                    ddlSituacao.DataValueField = "Codigo";
                    ddlSituacao.DataBind();
                }
            }
            var script = @"$(function () {
                    var produtos = [" + ListarProdutos() + @"];
                    var fornecedores = [" + ListarFornecedores() + @"];
                    var clientes = [" + ListarClientes() + @"];
                    var formasPagamento = [" + ListarFormaPagamento() + @"];
                    $(""#" + txtProduto.ClientID + @""").autocomplete({ source: produtos, select: function(evn,item) { if(item.item){ $('#" + txtValorUnit.ClientID + @"').val(item.item.valor); $('#" + txtCusto.ClientID + @"').val(item.item.valorCusto); } } }); 
                    $(""#" + txtFornecedor.ClientID + @""").autocomplete({ source: fornecedores }); 
                    $(""#" + txtFormaPagamento.ClientID + @""").autocomplete({ source: formasPagamento });

                    $(""#" + txtNome.ClientID + @""").autocomplete({ source: clientes, select: function(evn,item) { 
                        if(item.item){ 
                            $('#" + txtNome.ClientID + @"').val(item.item.Nome); 
                            $('#" + txtCEP.ClientID + @"').val(item.item.CEP);
                            $('#" + txtCidade.ClientID + @"').val(item.item.Cidade);
                            $('#" + txtCPF_CNPJ.ClientID + @"').val(item.item.CPF_CNPJ);
                            $('#" + txtEndereco.ClientID + @"').val(item.item.Endereco);
                            $('#" + txtEstado.ClientID + @"').val(item.item.Estado);
                            $('#" + txtInscEstadual.ClientID + @"').val(item.item.InscricaoEstadual);
                            $('#" + txtInscMunicipal.ClientID + @"').val(item.item.InscricaoMunicipal);
                            $('#" + txtNumero.ClientID + @"').val(item.item.Numero);
                            $('#" + txtPessoaContato.ClientID + @"').val(item.item.PessoaContato);
                            $('#" + txtTelefone01.ClientID + @"').val(item.item.Telefone1); 
                            $('#" + txtTelefone02.ClientID + @"').val(item.item.Telefone2);
                            $('#" + txtEmail.ClientID + @"').val(item.item.Email);
                        } } }); 
                    });";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "autocomplete", script, true);
        }

        void Venda_PreRender(object sender, EventArgs e)
        {
            this.gvVenda.DataSource = Itens;
            this.gvVenda.RowDataBound += gvVenda_RowDataBound;
            this.gvVenda.DataBind();
            lblValorTotal.Text = string.Format("{0:N2}", Itens.Sum(i => i.SubTotal));
        }

        private List<Situacao> Situacoes { get; set; }
        void gvVenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            if (Situacoes == null)
            {
                using (var model = new controleEntities())
                    Situacoes = model.Situacao.ToList();
            }
            var ddlSituacao = e.Row.FindControl("ddlSituacao") as DropDownList;
            ddlSituacao.DataSource = Situacoes;
            ddlSituacao.DataBound += ddlSituacao_DataBound;
            ddlSituacao.DataBind();
            ddlSituacao.SelectedValue = (((TItemVenda)e.Row.DataItem).ItemVenda.CodSituacao).ToString();

            var txtDataEntrega = e.Row.FindControl("txtDataEntrega") as TextBox;
            var lnkSalvaDataEntrega = e.Row.FindControl("lnkSalvaDataEntrega") as LinkButton;
            var hlAlterarData = e.Row.FindControl("hlAlterarData") as HyperLink;
            hlAlterarData.NavigateUrl = "#";
            hlAlterarData.Attributes.Add("onclick", string.Format("$('#{0}').show();$('#{1}').show(); $(this).hide();", txtDataEntrega.ClientID, lnkSalvaDataEntrega.ClientID));
            lnkSalvaDataEntrega.Click += lnkSalvaDataEntrega_Click;
        }

        protected void lnkSalvaDataEntrega_Click(object sender, EventArgs e)
        {
            var lnkSalvaDataEntrega = sender as LinkButton;
            var txtDataEntrega = lnkSalvaDataEntrega.NamingContainer.FindControl("txtDataEntrega") as TextBox;
            var hlAlterarData = lnkSalvaDataEntrega.NamingContainer.FindControl("hlAlterarData") as HyperLink;
            
            DateTime castDateTime;
            if (DateTime.TryParse(txtDataEntrega.Text, out castDateTime))
            {
                hlAlterarData.Text = txtDataEntrega.Text;
                var item = Itens.FirstOrDefault(i => i.Identificador == lnkSalvaDataEntrega.CommandArgument.ToString());
                item.ItemVenda.DataPrevisaoEntrega = castDateTime;
                if (item.ItemVenda.Codigo > 0)
                {
                    
                    using (var model = new controleEntities())
                    {
                        model.Itens.Where(i => i.Codigo == item.ItemVenda.Codigo).FirstOrDefault().DataPrevisaoEntrega = castDateTime;
                        model.SaveChanges();
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Data inválida.');", true);
            }

            hlAlterarData.Attributes.Add("style", "display:;");
            txtDataEntrega.Attributes.Add("style", "display:none;");
            lnkSalvaDataEntrega.Attributes.Add("style", "display:none;");
        }

        void ddlSituacao_DataBound(object sender, EventArgs e)
        {
            var ddlSituacao = sender as DropDownList;
            foreach (ListItem item in ddlSituacao.Items)
            {
                var cor = Situacoes.Where(i => i.Codigo.ToString() == item.Value).First().Cor;
                item.Attributes.Add("style", "color:" + cor + "");
            }
        }

        public string ListarProdutos()
        {
            var retorno = string.Empty;
            using (var model = new controleEntities())
            {
                retorno += string.Join(",", model.Produto.ToList().Select(i =>
                    string.Format("{{ valorCusto:\"{3:N2}\", valor:\"{2:N2}\", value:\"{1}\", label:\"{1}\" }}", i.Codigo, i.Descricao, i.Valor, i.ValorCusto)).ToArray());
            }
            return retorno;
        }
        public string ListarFornecedores()
        {
            var retorno = string.Empty;
            using (var model = new controleEntities())
            {
                retorno += string.Join(",", model.Fornecedor.Select(i => "\"" + i.Descricao + "\"").ToArray());
            }
            return retorno;
        }
        public string ListarFormaPagamento()
        {
            var retorno = string.Empty;
            using (var model = new controleEntities())
            {
                retorno += string.Join(",", model.FormaPagamento.Select(i => "\"" + i.Descricao + "\"").ToArray());
            }
            return retorno;
        }

        public string ListarClientes()
        {
            var retorno = string.Empty;
            using (var model = new controleEntities())
            {
                retorno += string.Join(",", model.Cliente.ToList().Select(i =>
                    string.Format("{{ field:\"{1}\",value:\"{1}\", Codigo:\"{0}\",Nome:\"{1}\",CEP:\"{2}\",Cidade:\"{3}\",CPF_CNPJ:\"{4}\",Endereco:\"{5}\",Estado:\"{6}\",InscricaoEstadual:\"{7}\",InscricaoMunicipal:\"{8}\",Numero:\"{9}\",PessoaContato:\"{10}\",Telefone1:\"{11}\",Telefone2:\"{12}\", Email:\"{13}\" }}",
                    i.Codigo,
                    i.Nome,
                    i.CEP,
                    i.Cidade,
                    i.CPF_CNPJ,
                    i.Endereco,
                    i.Estado,
                    i.InscricaoEstadual,
                    i.InscricaoMunicipal,
                    i.Numero,
                    i.PessoaContato,
                    i.Telefone1,
                    i.Telefone2,
                    i.Email)).ToArray());
            }
            return retorno;
        }


        protected void btnInserir_Click(object sender, EventArgs e)
        {
            var Item = new TItemVenda();
            int codProduto = 0, codFornecedor = 0, quantidade = 0;
            decimal valorUnitario = 0, valorCusto = 0;
            DateTime dataEntrega;
            if (!decimal.TryParse(txtValorUnit.Text, out valorUnitario))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Valor do item inválido.');", true);
                return;
            }
            if (!decimal.TryParse(txtCusto.Text, out valorCusto))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Valor de custo inválido.');", true);
                return;
            }
            if (!int.TryParse(txtQuantidade.Text, out quantidade))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Quantidade com valor inválido.');", true);
                return;
            }
            if (!DateTime.TryParse(txtDataPrevisao.Text, out dataEntrega))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Data previsão com valor inválido.');", true);
                return;
            }
            using (var model = new controleEntities())
            {
                var produto = model.Produto.Where(i => i.Descricao == txtProduto.Text).FirstOrDefault();
                if (produto == null)
                {
                    var entity = new Produto() { Descricao = txtProduto.Text, Valor = valorUnitario, ValorCusto = valorCusto };
                    model.Produto.Add(entity);
                    model.SaveChanges();
                    codProduto = entity.Codigo;
                }
                else
                {
                    codProduto = produto.Codigo;
                }

                var fornecedor = model.Fornecedor.Where(i => i.Descricao == txtFornecedor.Text).FirstOrDefault();
                if (fornecedor == null)
                {
                    var entity = new Fornecedor() { Descricao = txtFornecedor.Text };
                    model.Fornecedor.Add(entity);
                    model.SaveChanges();
                    codFornecedor = entity.Codigo;
                }
                else
                {
                    codFornecedor = fornecedor.Codigo;
                }


            }
            Item.ItemVenda = new ItemVenda();
            Item.ItemVenda.CodFornecedor = codFornecedor;
            Item.ItemVenda.CodSituacao = int.Parse(ddlSituacao.SelectedValue);
            Item.ItemVenda.CodProduto = codProduto;
            Item.ItemVenda.Quantidade = quantidade;
            Item.ItemVenda.ValorCusto = valorCusto;
            Item.ItemVenda.ValorUnit = valorUnitario;
            Item.ItemVenda.DataPrevisaoEntrega = dataEntrega;
            ObterDados(Item);
            this.Itens.Add(Item);
            LimparItem();
        }

        public void LimparItem()
        {
            txtProduto.Text = null;
            txtQuantidade.Text = "1";
            txtValorUnit.Text = null;
            txtCusto.Text = null;
            txtFornecedor.Text = null;
        }

        public void ObterDados(TItemVenda item)
        {
            using (var model = new controleEntities())
            {
                item.Fornecedor = model.Fornecedor.Where(i => i.Codigo == item.ItemVenda.CodFornecedor).Select(i => i.Descricao).FirstOrDefault();
                item.Situacao = model.Situacao.Where(i => i.Codigo == item.ItemVenda.CodSituacao).Select(i => i.Descricao).FirstOrDefault();
                item.Produto = model.Produto.Where(i => i.Codigo == item.ItemVenda.CodProduto).Select(i => i.Descricao).FirstOrDefault();
                item.Identificador = Guid.NewGuid().ToString();
            }
        }



        public class TItemVenda
        {
            public ItemVenda ItemVenda { get; set; }
            public string Identificador { get; set; }
            public decimal SubTotal { get { return this.ItemVenda.Quantidade * this.ItemVenda.ValorUnit; } }
            public string Situacao { get; set; }
            public string Fornecedor { get; set; }
            public string Produto { get; set; }
        }
        public int? CodigoVenda
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["c"]) ? (int?)int.Parse(Request.QueryString["c"]) : null;
            }
        }
        public List<TItemVenda> Itens
        {
            get
            {
                if (Session["Itens"] == null)
                    Session["Itens"] = new List<TItemVenda>();
                return Session["Itens"] as List<TItemVenda>;
            }
        }

        private void CarregarVenda()
        {
            if (CodigoVenda.HasValue)
            {
                using (var model = new controleEntities())
                {
                    var entidade_venda = model.Venda.Where(i => i.Codigo == CodigoVenda.Value).FirstOrDefault();
                    var entidade_cliente = model.Cliente.Where(i => i.Codigo == entidade_venda.CodCliente).FirstOrDefault();
                    var itemTabela = model.Itens.Where(i => i.CodVenda == CodigoVenda.Value).ToList();
                    var itemFormaPagamento = model.FormaPagamento.FirstOrDefault(i => i.Codigo == entidade_venda.CodFormaPagamento);
                    this.Itens.Clear();
                    foreach (var item in itemTabela)
                    {
                        var tItem = new TItemVenda();
                        tItem.ItemVenda = item;
                        ObterDados(tItem);
                        this.Itens.Add(tItem);
                    }

                    txtNome.Text = entidade_cliente.Nome;
                    txtCPF_CNPJ.Text = entidade_cliente.CPF_CNPJ;
                    txtCEP.Text = entidade_cliente.CEP;
                    txtCidade.Text = entidade_cliente.Cidade;
                    txtEndereco.Text = entidade_cliente.Endereco;
                    txtEstado.Text = entidade_cliente.Estado;
                    txtInscEstadual.Text = entidade_cliente.InscricaoEstadual;
                    txtInscMunicipal.Text = entidade_cliente.InscricaoMunicipal;
                    txtNumero.Text = entidade_cliente.Numero;
                    txtPessoaContato.Text = entidade_cliente.PessoaContato;
                    txtTelefone01.Text = entidade_cliente.Telefone1;
                    txtTelefone02.Text = entidade_cliente.Telefone2;
                    txtEmail.Text = entidade_cliente.Email;
                    txtFormaPagamento.Text = itemFormaPagamento != null ? itemFormaPagamento.Descricao : null;
                    txtObservacoes.Text = entidade_venda.Observacao;
                    lblValorPago.Text = string.Format("{0:N2}", entidade_venda.ValorPago);
                }
            }
        }

        protected void btnSalvarVenda_Click(object sender, EventArgs e)
        {
            //decimal valorPago;
            //if (!decimal.TryParse(lblValorPago.Text, out valorPago))
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Valor pago é inválido.');", true);
            //    return;
            //}

            using (var model = new controleEntities())
            {
                Venda entidade_venda = null;
                if (CodigoVenda.HasValue)
                    entidade_venda = model.Venda.Where(i => i.Codigo == CodigoVenda.Value).FirstOrDefault();
                else
                {
                    entidade_venda = new Venda();
                    entidade_venda.Data = DateTime.Now;
                    model.Venda.Add(entidade_venda);
                }

                var cliente = model.Cliente.Where(i => i.Nome == txtNome.Text).FirstOrDefault();
                if (cliente == null)
                {
                    var entity = new Cliente() { Nome = txtNome.Text, CPF_CNPJ = txtCPF_CNPJ.Text };
                    model.Cliente.Add(entity);
                    model.SaveChanges();
                    cliente = entity;
                }
                cliente.CPF_CNPJ = txtCPF_CNPJ.Text;
                cliente.CEP = txtCEP.Text;
                cliente.Cidade = txtCidade.Text;
                cliente.Endereco = txtEndereco.Text;
                cliente.Estado = txtEstado.Text;
                cliente.InscricaoEstadual = txtInscEstadual.Text;
                cliente.InscricaoMunicipal = txtInscMunicipal.Text;
                cliente.Numero = txtNumero.Text;
                cliente.Email = txtEmail.Text;
                cliente.PessoaContato = txtPessoaContato.Text;
                cliente.Telefone1 = txtTelefone01.Text;
                cliente.Telefone2 = txtTelefone02.Text;
                entidade_venda.CodCliente = cliente.Codigo;

                var formaPagamento = model.FormaPagamento.Where(i => i.Descricao == txtFormaPagamento.Text).FirstOrDefault();
                if (formaPagamento == null)
                {
                    var entity = new FormaPagamento() { Descricao = txtFormaPagamento.Text };
                    model.FormaPagamento.Add(entity);
                    model.SaveChanges();
                    entidade_venda.CodFormaPagamento = entity.Codigo;
                }
                else
                {
                    entidade_venda.CodFormaPagamento = formaPagamento.Codigo;
                }

                entidade_venda.ValorTotal = Itens.Sum(i => i.SubTotal);
                //entidade_venda.ValorPago = valorPago;
                entidade_venda.Observacao = txtObservacoes.Text;

                model.SaveChanges();
                foreach (var item in Itens)
                {
                    item.ItemVenda.CodVenda = entidade_venda.Codigo;
                    if (item.ItemVenda.Codigo == 0)
                        model.Itens.Add(item.ItemVenda);
                }
                model.SaveChanges();
            }
            Response.Redirect("/");
        }

        protected void gvVenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Excluir") return;

            var item = Itens.FirstOrDefault(i => i.Identificador == e.CommandArgument.ToString());
            if (item.ItemVenda.Codigo > 0)
            {
                using (var model = new controleEntities())
                {
                    model.Itens.Remove(model.Itens.FirstOrDefault(i => i.Codigo == item.ItemVenda.Codigo));
                    model.SaveChanges();
                }
            }
            Itens.Remove(item);


        }

        protected void ddlSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (sender) as DropDownList;
            var row = ddl.NamingContainer as GridViewRow;
            var id = gvVenda.DataKeys[row.RowIndex]["Identificador"].ToString();
            var item = Itens.FirstOrDefault(i => i.Identificador == id);
            if (item != null)
            {
                item.Situacao = ddl.SelectedItem.Text;
                item.ItemVenda.CodSituacao = int.Parse(ddl.SelectedItem.Value);
                if (item.ItemVenda.Codigo > 0)
                {
                    using (var model = new controleEntities())
                    {
                        model.Itens.First(i => i.Codigo == item.ItemVenda.Codigo).CodSituacao = item.ItemVenda.CodSituacao;
                        model.SaveChanges();
                    }
                }

            }
        }

        protected void btnMostrarArquivos_Click(object sender, EventArgs e)
        {
            if(CodigoVenda.HasValue)
                ucArquivos.CarregarDados(CodigoVenda.Value);
        }
    }
}