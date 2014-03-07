using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JetDev.Control
{
    public partial class Tarefa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var entity = new controleEntities())
                {
                    ddlResponsavel.DataSource = entity.Responsavel.ToList();
                    ddlResponsavel.DataTextField = "Nome";
                    ddlResponsavel.DataValueField = "CodResponsavel";
                    ddlResponsavel.DataBind();
                }
                CarregarTarefas();
            }
        }

        protected void gvTarefas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        public void CarregarTarefas()
        {
            using (var entity = new controleEntities())
            {
                var codResponsavel = int.Parse(ddlResponsavel.SelectedValue);
                var tarefas = entity.Tarefa.Where(i => i.Responsavel.CodResponsavel == codResponsavel).ToList();

                gvTarefas.DataSource = tarefas;
                gvTarefas.DataBind();

            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            var codResponsavel = int.Parse(ddlResponsavel.SelectedValue);

            using (var entity = new controleEntities())
            {                 
                var novaTarefa = new Tarefa();
                novaTarefa.Descrição = txtDescricao.Text;
                novaTarefa.DataCadastro = DateTime.Now;
                novaTarefa.DataLimite = DateTime.Parse(txtDataLimite.Text);
                novaTarefa.Responsavel = entity.Responsavel.FirstOrDefault(i => i.CodResponsavel == codResponsavel);
                entity.Tarefa.Add(novaTarefa);
                entity.SaveChanges();
            }
            CarregarTarefas();
        }
    }
}