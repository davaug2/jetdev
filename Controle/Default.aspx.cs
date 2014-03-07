using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
namespace JetDev.Control
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ListarVendas(false, false);
        }

        public class TVenda
        {
            public string Cliente { get; set; }
            public Venda Venda { get; set; }
            public List<TItemVenda> Itens { get; set; }

            public bool Atrasado { get; set; }

            public bool EntregaHoje { get; set; }
        }
        public class TItemVenda
        {
            public ItemVenda ItemVenda { get; set; }
            public string Produto { get; set; }
            public decimal SubTotal { get; set; }
            public Situacao Situacao { get; set; }
        }

        private void ListarVendas(bool entregaHoje, bool Atrasado)
        {
            ListarVendas(entregaHoje, Atrasado, null);
        }
        private void ListarVendas(bool entregaHoje, bool Atrasado, DateTime? dataPagamento)
        {
            using (var model = new controleEntities())
            {
                var vendas = model.Venda.Where(i => !i.Cancelado);

                if (dataPagamento.HasValue)
                    vendas = vendas.Where(i => i.Pagamentos.Count(p => p.Data == dataPagamento.Value) > 0);

                var listaVendas = vendas
                    //.Where(i => i.Itens.Any(x => x.CodSituacao != 8) && i.ValorTotal != i.ValorPago) 
                    .Select(i => new TVenda() { Venda = i }).ToList();


                foreach (var item in listaVendas)
                {
                    item.Cliente = model.Cliente.Where(i => i.Codigo == item.Venda.CodCliente).Select(i => i.Nome).FirstOrDefault();
                    item.Itens = model.Itens.Where(i => i.CodVenda == item.Venda.Codigo).Select(i => new TItemVenda() { ItemVenda = i }).ToList();
                    foreach (var itemVenda in item.Itens)
                    {
                        itemVenda.Produto = model.Produto.Where(i => i.Codigo == itemVenda.ItemVenda.CodProduto).Select(i => i.Descricao).FirstOrDefault();
                        itemVenda.Situacao = model.Situacao.Where(i => i.Codigo == itemVenda.ItemVenda.CodSituacao).FirstOrDefault();
                        itemVenda.SubTotal = itemVenda.ItemVenda.Quantidade * itemVenda.ItemVenda.ValorUnit;
                    }
                    item.EntregaHoje = item.Itens.Any(i => i.ItemVenda.DataPrevisaoEntrega.HasValue && i.ItemVenda.DataPrevisaoEntrega.Value == DateTime.Today);

                    item.Atrasado = item.Itens.Any(i => 
                            i.ItemVenda.CodSituacao != 8
                            && i.ItemVenda.DataPrevisaoEntrega.HasValue 
                            && i.ItemVenda.DataPrevisaoEntrega.Value <= DateTime.Now);
                }

                if (entregaHoje)
                    listaVendas = listaVendas.Where(i => i.EntregaHoje).ToList();

                if (Atrasado)
                    listaVendas = listaVendas.Where(i => i.Atrasado).ToList();

                gvVendas.DataSource = listaVendas.OrderByDescending(i => i.Venda.Codigo);
                gvVendas.RowDataBound += gvVendas_RowDataBound;
                gvVendas.DataBind();
            }
        }

        void gvVendas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                ((Button)e.Row.FindControl("btnRecibo")).OnClientClick = string.Format("window.open('reciboEntrega.aspx?cv={0}','','width=900,height=400');return false;", ((TVenda)e.Row.DataItem).Venda.Codigo);
        }


        protected void btnFinanceiro_Click(object sender, EventArgs e)
        {

        }

        protected void ClienteDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }

        protected void gvVendas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var codVenda = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "pagamentos")
            {

                using (var model = new controleEntities())
                {
                    gvPagamentos.DataSource = (from v in model.Venda
                                               from p in v.Pagamentos
                                               where v.Codigo == codVenda
                                               select p).ToList();
                    gvPagamentos.DataBind();
                    txtDataPagamento.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtValor.Text = (from v in model.Venda where v.Codigo == codVenda select v.ValorTotal - v.ValorPago).FirstOrDefault().ToString("N2");
                    CodigoVenda = codVenda;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pagamentos", "$(function () { $('#mPagamentos').modal('show'); });", true);
            }
            else if (e.CommandName == "cancelar")
            {
                using (var model = new controleEntities())
                {
                    model.Venda.First(i => i.Codigo == codVenda).Cancelado = true;
                    model.SaveChanges();
                }
                ListarVendas(false, false);
            }
            else if (e.CommandName == "editar")
            {
                Response.Redirect("~/Venda.aspx?c=" + e.CommandArgument.ToString());
            }
        }

        protected void gvPagamentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                ((Button)e.Row.FindControl("btnRecibo")).OnClientClick = string.Format("window.open('reciboEntrega.aspx?cp={0}','','width=900,height=400');return false;", ((Pagamentos)e.Row.DataItem).Codigo);
        }

        public int CodigoVenda { get { return int.Parse(ViewState["CodigoVenda"].ToString()); } set { ViewState["CodigoVenda"] = value.ToString(); } }

        protected void gvPagamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (var model = new controleEntities())
            {
                int codPagamento = int.Parse(e.CommandArgument.ToString());
                var pagamento = model.Pagamentos.Where(i => i.Codigo == codPagamento).First();
                var venda = model.Venda.First(i => i.Codigo == CodigoVenda);

                switch (e.CommandName)
                {
                    case "pago":
                        pagamento.CodSituacao = 2;
                        venda.ValorPago += pagamento.Valor;
                        model.SaveChanges();

                        break;
                    case "naopago":
                        pagamento.CodSituacao = 1;
                        venda.ValorPago -= pagamento.Valor;
                        model.SaveChanges();
                        break;
                    case "excluir":
                        if (pagamento.CodSituacao == 2)
                            venda.ValorPago -= pagamento.Valor;
                        model.Pagamentos.Remove(pagamento);
                        model.SaveChanges();
                        break;
                }
                gvPagamentos.DataSource = (from v in model.Venda
                                           from p in v.Pagamentos
                                           where v.Codigo == CodigoVenda
                                           select p).ToList();
                gvPagamentos.DataBind();
            }

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            DateTime dataPagamento;
            decimal valorPagamento;
            if (!DateTime.TryParse(txtDataPagamento.Text, out dataPagamento))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Data do pagamento é inválida');", true);
                return;
            }
            if (!decimal.TryParse(txtValor.Text, out valorPagamento))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Valor do pagamento é inválido');", true);
                return;
            }

            using (var model = new controleEntities())
            {
                var valorTotal = model.Venda.Where(i => i.Codigo == CodigoVenda).Select(i => i.ValorTotal).First();
                var valorPagamentos = model.Pagamentos.Count(i => i.CodVenda == CodigoVenda) > 0 ? model.Pagamentos.Where(i => i.CodVenda == CodigoVenda).Sum(i => i.Valor) : 0;

                if (valorPagamentos + valorPagamento > valorTotal)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", string.Format("alert('Pagamento excede o valor total da venda R$ {0:N2}');", valorTotal), true);
                    return;
                }

                var pgto = new Pagamentos()
                {
                    CodSituacao = 1,
                    Data = dataPagamento,
                    Valor = valorPagamento,
                    Responsavel = ddlResponsavel.SelectedValue,
                    CodVenda = CodigoVenda
                };
                model.Pagamentos.Add(pgto);
                model.SaveChanges();
            }
            using (var model = new controleEntities())
            {
                var cliente = new Cliente();
                cliente.Nome = "";

                model.Cliente.Add(cliente);
                model.SaveChanges();

                gvPagamentos.DataSource = (from v in model.Venda
                                           from p in v.Pagamentos
                                           where v.Codigo == CodigoVenda
                                           select p).ToList();
                gvPagamentos.DataBind();
            }
        }

        protected void btnPgtoDiario_Click(object sender, EventArgs e)
        {
            using (var model = new controleEntities())
            {
                gvFechamentoDiario.DataSource = (from p in model.Pagamentos
                                                 where p.CodSituacao == 2
                                                group p by p.Data into g
                                                orderby g.Key descending
                                                select new { Data = g.Key, Valor = g.Sum(i => i.Valor) }
                                                ).ToList();
                gvFechamentoDiario.DataBind();
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "pgtoDiario", "$(function () { $('#mPgtoDiario').modal('show'); });", true);
        }

        protected void btnAtrasado_Click(object sender, EventArgs e)
        {
            ListarVendas(false, true);
        }

        protected void btnEntrega_Dia_Click(object sender, EventArgs e)
        {
            ListarVendas(true, false);
        }

        protected void lnkData_Click(object sender, EventArgs e)
        {
            LinkButton lnkData = (LinkButton)sender;
            DateTime data = DateTime.Parse(lnkData.CommandArgument);
            ListarVendas(false, false, data);
        }
    }
}