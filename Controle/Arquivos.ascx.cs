using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JetDev.Control
{
    public partial class ucArquivos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PreRender += ucArquivos_PreRender;
        }

        void ucArquivos_PreRender(object sender, EventArgs e)
        {
            if(Aberto)
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mArquivos", "$(function () { $('#mArquivos').modal('show'); });", true); 
        }

        private int CodigoVenda
        {
            get { return (int)ViewState["CodigoVenda"]; }
            set { ViewState["CodigoVenda"] = value; }
        }

        private bool Aberto
        {
            get { return ViewState["Aberto"] is bool? (bool)ViewState["Aberto"] : false; }
            set { ViewState["Aberto"] = value; }
        }

        public void CarregarDados(int codigoVenda)
        {
            CodigoVenda = codigoVenda;
            CarregarDados();
            Aberto = true;
            
        }

        private void CarregarDados()
        {
            using (var model = new controleEntities())
            {
                var itensArquivos = (from i in model.Itens
                                     where i.CodVenda == this.CodigoVenda
                                     select new
                                     {
                                         CodItem = i.Codigo,
                                         Descricao = i.Produto.Descricao,
                                         QtdeArquivos = i.Arquivo.Count(),
                                         Arquivos = i.Arquivo.Where(x => !x.Excluido).Select(x => new { x.NomeArquivo, x.Extensao, x.Codigo, x.Tamanho })
                                     }).ToList();
                gvItens.DataSource = itensArquivos;
                gvItens.DataBind();
                gvItens.RowCommand += gvItens_RowCommand;
            }
        }

        private void MostraEscondeAnexo(GridViewRow row, bool mostrar)
        {
            var rptArquivo = row.FindControl("rptArquivo") as Repeater;
            var lnkAnexar = row.FindControl("lnkAnexar") as LinkButton;
            var fuAnexo = row.FindControl("fuAnexo") as AjaxControlToolkit.AsyncFileUpload;
            var btnCancelar = row.FindControl("btnCancelar") as Button;

            if (mostrar)
            {
                rptArquivo.Visible = lnkAnexar.Visible = false;
                fuAnexo.Visible = btnCancelar.Visible = true;
            }
            else
            {
                rptArquivo.Visible = lnkAnexar.Visible = true;
                fuAnexo.Visible = btnCancelar.Visible = false;
            }
        }

        protected void gvItens_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            var row = ((GridViewRow)((WebControl)e.CommandSource).NamingContainer);

            if (e.CommandName == "MostrarAnexo")
            {
                foreach (GridViewRow gvRow in gvItens.Rows)
                    MostraEscondeAnexo(gvRow, false);
                MostraEscondeAnexo(row, true);

                Session["ItemSelecionado"] = gvItens.DataKeys[row.DataItemIndex]["CodItem"];
            }
            else if (e.CommandName == "CancelarAnexo")
            {
                MostraEscondeAnexo(row, false);
                CarregarDados();
            }
        }

        private string ConverterTamanho(string tamanho)
        {
            decimal tamanhoBytes = decimal.Parse(tamanho);
            tamanhoBytes = tamanhoBytes / 1024M;
            if (tamanhoBytes > 512)
                return string.Format("{0} Mbps", Math.Round(tamanhoBytes / 1024M, 2));
            else
                return string.Format("{0} Kbps", Math.Round(tamanhoBytes, 2));

        }

        protected void fuAnexo_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            var fuAnexo = sender as AjaxControlToolkit.AsyncFileUpload;
            var codigoItem = (int)Session["ItemSelecionado"];
            using (var model = new controleEntities())
            {
                var arq = new Arquivo()
                {
                    NomeArquivo = Path.GetFileNameWithoutExtension(e.FileName),
                    Extensao = Path.GetExtension(e.FileName),
                    Itens = model.Itens.FirstOrDefault(i => i.Codigo == codigoItem),
                    Tamanho = ConverterTamanho(e.FileSize)
                };
                model.Arquivo.Add(arq);
                model.SaveChanges();
                fuAnexo.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(".\\Arquivos"), arq.Codigo.ToString()));
            }
        }

        protected void rptArquivo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DownloadArquivo")
            {
                using (var model = new controleEntities())
                {
                    var codArquivo = new Guid(e.CommandArgument.ToString());
                    var arq = model.Arquivo.FirstOrDefault(i => i.Codigo == codArquivo);
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + arq.NomeArquivo + arq.Extensao);
                    Response.BinaryWrite(File.ReadAllBytes(Path.Combine(HttpContext.Current.Server.MapPath(".\\Arquivos"), arq.Codigo.ToString())));
                    Response.End();
                }
            }
            else if (e.CommandName == "ExcluirArquivo")
            {
                using (var model = new controleEntities())
                {
                    var codArquivo = new Guid(e.CommandArgument.ToString());
                    var arq = model.Arquivo.FirstOrDefault(i => i.Codigo == codArquivo);
                    arq.Excluido = true;
                    model.SaveChanges();
                }
                CarregarDados();
            }
        }

        protected void lnkFechar_Click(object sender, EventArgs e)
        {
            Aberto = false;
        }
    }
}