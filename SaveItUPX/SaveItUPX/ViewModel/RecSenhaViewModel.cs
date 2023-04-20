using SaveItUPX.Model;
using SaveItUPX.Service;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaveItUPX.ViewModel
{
    public class RecSenhaViewModel : BaseViewModel
    {
        FirebaseService firebaseService = new FirebaseService();

        string RecEmail = "";


        public Usuario usuario { get; set; }

        private string _Email;
        public string Email
        {
            set
            {
                this._Email = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Email;
            }
        }

        private string _Codigo;
        public string Codigo
        {
            set
            {
                this._Codigo = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Codigo;
            }
        }

        private string _NSenha;
        public string NSenha
        {
            set
            {
                this._NSenha = value;
                OnPropertyChanged();
            }
            get
            {
                return this._NSenha;
            }
        }

        public Command EnviarCodigo { get; set; }
        public Command EnviarSenha { get; set; }

        public RecSenhaViewModel()
        {
            EnviarCodigo = new Command(async () => await EnviarCod());
            EnviarSenha = new Command(async () => await EnviarSen());
        }

        private async Task EnviarCod()
        {
            RecEmail = Email;
            var lista = await firebaseService.ObterUserReset(RecEmail);
            usuario = lista;

            if (lista != null)
            {
                string senha = GerarSenhas();

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                    mail.From = new MailAddress("emaildecontato@outlook.com", "Save It");
                    mail.To.Add(_Email);
                    mail.Subject = "Código de recuperação de senha";
                    mail.Body = "Olá, esse é seu código: " + senha.ToLower();

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.office365.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("emaildecontato@outlook.com", "@nomedeperfil");

                    SmtpServer.Send(mail);

                    DBSAveIt.RecCodigo = senha;
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Código enviado para seu e-mail", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Faild", ex.Message, "OK");

                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Usuário não encontrado!!!", "Fechar");
            }
        }

        private async Task EnviarSen()
        {
            var lista = await firebaseService.ObterUserReset(RecEmail);

            usuario = lista;

            if (lista != null)
            {
                if (String.IsNullOrEmpty(Codigo) || DBSAveIt.RecCodigo != Codigo)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Os campos devem estar preenchidos", "Fechar");
                }
                else
                {
                    await firebaseService.ResetSenha(lista.Nome, lista.Email, lista.CPF, lista.Endereco, lista.Telefone, lista.Sexo, lista.Favorito, lista.Cidade, NSenha);
                    await Application.Current.MainPage.DisplayAlert("Parabéns", "Senha trocada com sucesso!!!", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Código inválido", "Fechar");
            }
        }

        public string GerarSenhas()
        {
            int Tamanho = 10; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }
    }
}
