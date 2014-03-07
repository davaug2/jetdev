using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JetDev.Control
{
    public partial class ReciboEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var model = new controleEntities())
            {
                var codigoVenda = !string.IsNullOrEmpty(Request.QueryString["cv"]) ? int.Parse(Request.QueryString["cv"]) : 0;
                if (codigoVenda > 0)
                {
                    ReciboProduto.Visible = true;
                    Recibo.Visible = false;
                    
                    var venda = model.Venda.First(i => i.Codigo == codigoVenda);
                    var cliente = model.Cliente.First(i => i.Codigo == venda.CodCliente);
                    var itens = (from i in model.Itens
                                 join p in model.Produto on i.CodProduto equals p.Codigo
                                 where i.CodVenda == codigoVenda
                                 select new { p.Descricao, i.Quantidade, SubTotal = i.Quantidade * i.ValorUnit, i.CodSituacao }).ToList();
                    this.litValor.Text = venda.ValorPago.ToString("N2");
                    this.litNomeCliente.Text = cliente.Nome;
                    this.litNomeCliente2.Text = cliente.Nome;
                    this.litValorExtenso.Text = Extenso.ValorExtenso(double.Parse(venda.ValorPago.ToString()));
                    this.rptProdutos.DataSource = itens;
                    this.rptProdutos.DataBind();
                    this.rptProdutos2.DataSource = itens;
                    this.rptProdutos2.DataBind();
                    if (venda.ValorPago < venda.ValorTotal)
                    {
                        litValorRestante.Text = string.Format("Estou ciente que ficará pendente de pagamento a quantia de R$ {0:N2} ({1}) referente aos produtos acima listados.", venda.ValorTotal - venda.ValorPago, Extenso.ValorExtenso(double.Parse((venda.ValorTotal - venda.ValorPago).ToString())));
                    }
                }

                var codigoPagamento = !string.IsNullOrEmpty(Request.QueryString["cp"]) ? int.Parse(Request.QueryString["cp"]) : 0;

                if (codigoPagamento > 0)
                {
                    ReciboProduto.Visible = false;
                    Recibo.Visible = true;

                    var pagamento = model.Pagamentos.Include("Venda").Include("Venda.Cliente").First(i => i.Codigo == codigoPagamento);
                    var venda = pagamento.Venda;
                    var cliente = venda.Cliente;
                    
                    var itens = (from i in model.Itens
                                 join p in model.Produto on i.CodProduto equals p.Codigo
                                 where i.CodVenda == venda.Codigo
                                 select new { p.Descricao, i.Quantidade, SubTotal = i.Quantidade * i.ValorUnit }).ToList();
                    this.litValor.Text = pagamento.Valor.ToString("N2");
                    this.litNomeCliente2.Text = cliente.Nome;
                    this.litValorExtenso.Text = Extenso.ValorExtenso(double.Parse(pagamento.Valor.ToString()));
                    this.litResponsavel.Text = pagamento.Responsavel;
                    this.rptProdutos2.DataSource = itens;
                    this.rptProdutos2.DataBind();
                    
                    if (venda.ValorPago < venda.ValorTotal)
                    {
                        litValorRestante.Text = string.Format("Estou ciente que ficará pendente de pagamento a quantia de R$ {0:N2} ({1}) referente aos produtos acima listados.", venda.ValorTotal - venda.ValorPago, Extenso.ValorExtenso(double.Parse((venda.ValorTotal - venda.ValorPago).ToString())));
                    }
                }
            }
        }
    }
}