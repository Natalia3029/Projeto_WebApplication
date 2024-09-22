using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Responsavel por criar o Email
using System.Net.Mail;
using System.Net;
namespace Projeto3
{
    public partial class FaleConosco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Validar os Dados
                if (SeuNome.Text.Trim() == "")
                {
                    MensagemErro.Text = "Digite seu Nome";
                }
                else if (SeuEmail.Text.Trim() == "")
                {
                    MensagemErro.Text = "Digite seu e-mail";
                }
                else if (Mensagem.Text.Trim() == "")
                {
                    MensagemErro.Text = "Digite a mensagem";
                }
                else
                {
                    //2. Criar o E-mail

                    MailMessage email = new MailMessage();
                    email.To.Add("contato@seudominio.com.br");
                    MailAddress from = new MailAddress("contato@seudominio.com.br");
                    email.From = from;
                    email.Subject = " E-mail enviado pelo form de contato";
                    email.Body = "Nome;" + SeuNome.Text + "\n";
                    email.Body = "Email;" + SeuEmail.Text + "\n";
                    email.Body = "Mensagem;" + Mensagem.Text + "\n";

                    //3. Transmitir o E-mail

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.seudominio.com.br";
                    smtp.Credentials = new System.Net.NetworkCredential("contato@seudominio.com", "suasenha");
                    smtp.Send(email);
                    smtp.Port = 465;



                }
            }
            catch (Exception)
            {
                MensagemErro.Text = "Houve uma falha no envio do e-mail";
            }


        }
    }
}