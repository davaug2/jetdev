using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JetDev.Control
{
    public partial class Faturas : System.Web.UI.Page
    {
        public class ItemFatura
        {
            public string Descricao { get; set; }
            public decimal ValorUnit { get; set; }
            public decimal SubTotal { get; set; }
            public decimal Quantidade { get; set; }
            public string Codigo { get; set; }
        }

        public decimal ValorTotal
        {
            get
            {
                return this.Itens.Sum(i => i.SubTotal);
            }
        }  

        public List<ItemFatura> Itens 
        {
            get 
            {
                if(Session["Fatura"] == null)
                    Session["Fatura"] = new List<ItemFatura>();
                return Session["Fatura"] as List<ItemFatura>;
            }
        }    
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PreRender += Faturas_PreRender;
            label.InnerText = "";
            if (!IsPostBack)
            {
                //var lista = WHMCS.WHMCS.ListarProdutos();
                //ddlProdutos.DataSource = lista.Select(i => new { i.Codigo, Descricao = string.Format("{1} - R${2:N2}", i.Codigo, i.Nome, i.Preco) }).ToList();
                //ddlProdutos.DataTextField = "Descricao";
                //ddlProdutos.DataValueField = "Codigo";
                //ddlProdutos.DataBind();
                //ddlProdutos.Items.Insert(0, new ListItem("Selecione", ""));
                //ddlProdutos.Attributes.Add("onchange", "$('#" + txtValor.ClientID + "').val($('option:selected', this).attr('Preco'));");

                //foreach (ListItem item in ddlProdutos.Items)
                //{
                //    if(!string.IsNullOrEmpty(item.Value))
                //        item.Attributes.Add("Preco", lista.Where(i => i.Codigo == item.Value).FirstOrDefault().Preco.ToString("N2"));
                //}
            }
        }

        void Faturas_PreRender(object sender, EventArgs e)
        {
            label.Visible = label.InnerText != "";
            gvNovaFatura.DataSource = this.Itens;
            gvNovaFatura.DataBind();
                
        }

        protected void btnOutroProduto_Click(object sender, EventArgs e)
        {
            ddlProdutos.Visible = false;
            txtProduto.Visible = true;
            btnOutroProduto.Visible = false;
            btnProdutoCadastrado.Visible = true;
        }

        protected void btnProdutoCadastrado_Click(object sender, EventArgs e)
        {
            ddlProdutos.Visible = true;
            txtProduto.Visible = false;
            btnOutroProduto.Visible = true;
            btnProdutoCadastrado.Visible = false;
        }

        protected void lnkInserir_Click(object sender, EventArgs e)
        {
            var item = new ItemFatura();
            if (ddlProdutos.Visible)
                item.Descricao = ddlProdutos.SelectedItem.Text;
            else
                item.Descricao = txtProduto.Text;
            decimal cast;
            if (decimal.TryParse(txtValor.Text, out cast))
                item.ValorUnit = cast;
            else
            {
                label.InnerText = "Formato do valor é inválido";
                return;
            }
            if (decimal.TryParse(txtQtde.Text, out cast))
                item.Quantidade = cast;
            else
            {
                label.InnerText = "Formato da quantidade é inválido";
                return;
            }
            item.SubTotal = item.ValorUnit * item.Quantidade;
            item.Codigo = Guid.NewGuid().ToString();
            this.Itens.Add(item);                
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var item = this.Itens.FirstOrDefault(i => i.Codigo == e.CommandArgument.ToString());
            this.Itens.Remove(item);
        }
    }
}