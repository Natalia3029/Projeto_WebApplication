using DataServices.DataBase;
using System;

namespace Projeto3
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LerUsuarios();
        }
        /// <summary>
        /// Lê todos os usuarios da tabela "usuarios" para exibir no grid 
        /// </summary>
        protected void LerUsuarios()
        {
            string comandoSQL = "Select UsuarioID,NomeCompleto,Email,NomeAcesso FROM Usuarios WHERE Status=1 ORDER BY NomeCompleto ASC;";
            DAO db = new DAO();
            //Atribui a string de conexão á classe de acesso ao Bnaco de Dados.
            db.ConnectionString = App_code.AppSettings.ConexaoDB;
            //Define o Banco de Dados e será acessado.
            db.DataProviderName = DAO.ProviderName.OleDb;
            System.Data.DataTable tb = (System.Data.DataTable)db.Query(comandoSQL);
            ExibirUsuarios.DataSource = tb;
            ExibirUsuarios.DataBind();

            tb.Dispose();
        }
        protected void InserirEditar_Click(object sender, EventArgs e)
        {

            // 1. Validar dos Dados
            if (NomeCompleto.Text.Trim() == "")
            {
                Mensagem.Text = "Digite seu Nome";
            }
            else if (Email.Text.Trim() == "")
            {
                Mensagem.Text = "Digite seu Email";
            }
            else if (NomeAcesso.Text.Trim() == "")
            {
                Mensagem.Text = "Digite o nome de acesso";
            }
            else if (Senha.Text.Trim() == "")
            {
                Mensagem.Text = "Digite a senha";
            }
            else
            {

                // 2. Instancia do Model da tabela usuarios.
                Model.Usuarios usuario = new Model.Usuarios();
                // Classe de transação com o Banco de Dados( INSERT,UPDATE,DELETE,SELECT).
                DAO db = new DAO();
                //Atribui a string de conexão á classe de acesso ao Banco de Dados.
                db.ConnectionString = App_code.AppSettings.ConexaoDB;
                //Define o Banco de Dados e será acessado.
                db.DataProviderName = DAO.ProviderName.OleDb;

                if (UsuarioID.Text == "")
                {
                    usuario.NomeCompleto = NomeCompleto.Text;
                    usuario.Email = Email.Text;
                    usuario.NomeAcesso = NomeAcesso.Text;
                    usuario.Senha = Senha.Text;
                    usuario.Status = 1;
                    db.Insert(usuario, "UsuarioID");

                }
                else
                {
                    usuario.NomeCompleto = NomeCompleto.Text;
                    usuario.Email = Email.Text;
                    usuario.NomeAcesso = NomeAcesso.Text;
                    usuario.Senha = Senha.Text;
                    usuario.Status = 1;
                    db.Update(usuario, "UsuarioID", UsuarioID.Text);
                }
                LimparControles();
                LerUsuarios();
            }
        }
        /// <summary>
        ///Limpar os Controles do Formulario
        /// </summary>
        protected void LimparControles()
        {
            UsuarioID.Text = "";
            NomeCompleto.Text = "";
            NomeAcesso.Text = "";
            Senha.Text = "";
            Email.Text = "";
            InserirEditar.Text = "Inserir";
            Excluir.Visible = false;
        }
        protected void Excluir_Click(object sender, EventArgs e)
        {
            Model.Usuarios usuario = new Model.Usuarios();
            DAO db = new DAO();
            //Atribui a string de conexão á classe de acesso ao Banco de Dados.
            db.ConnectionString = App_code.AppSettings.ConexaoDB;
            //Define o Banco de Dados e será acessado.
            db.DataProviderName = DAO.ProviderName.OleDb;

            usuario.Status = 0;
            db.Update(usuario, "UsuarioID", UsuarioID.Text);
            LimparControles();
            LerUsuarios();
        }

        protected void ExibirUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // recupera a chave primaria da linha clicada 
            UsuarioID.Text = ExibirUsuarios.SelectedRow.Cells[1].Text;

            string comandoSQL = "SELECT * FROM Usuarios WHERE UsuarioID=" + UsuarioID.Text;
            DAO db = new DAO();
            //Atribui a string de conexão á classe de acesso ao Banco de Dados.
            db.ConnectionString = App_code.AppSettings.ConexaoDB;
            //Define o Banco de Dados e será acessado.
            db.DataProviderName = DAO.ProviderName.OleDb;

            System.Data.DataTable tb = (System.Data.DataTable)db.Query(comandoSQL);
            NomeCompleto.Text = tb.Rows[1]["NomeCompleto"].ToString();
            NomeAcesso.Text = tb.Rows[1]["NomeAcesso"].ToString();
            Email.Text = tb.Rows[1]["Email"].ToString();
            Senha.Text = tb.Rows[1]["Senha"].ToString();

            Excluir.Visible = true;
            InserirEditar.Text = "Editar";
            tb.Dispose();
        }

     

       

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Cancelar.Visible = false;
            BuscarUsuario.Text = "";
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            string comandoSQL = "SELECT UsuarioID,NomeCompleto FROM Usuarios WHERE Status=1 AND NomeCompleto LIKE '%" + BuscarUsuario.Text + "%'";

            DAO db = new DAO();
            //Atribui a string de conexão á classe de acesso ao Banco de Dados.
            db.ConnectionString = App_code.AppSettings.ConexaoDB;
            //Define o Banco de Dados e será acessado.
            db.DataProviderName = DAO.ProviderName.OleDb;

            ExibirUsuarios.DataSource = db.Query(comandoSQL);
            ExibirUsuarios.DataBind();
            Cancelar.Visible = true;

        }
    }
}


