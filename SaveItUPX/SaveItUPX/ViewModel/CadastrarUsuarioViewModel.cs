using SaveItUPX.Model;
using SaveItUPX.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaveItUPX.ViewModel
{
    public class CadastrarUsuarioViewModel : BaseViewModel
    {
        private string _Nome;
        public string Nome
        {
            set
            {
                this._Nome = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Nome;
            }
        }

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

        private string _CPF;
        public string CPF
        {
            set
            {
                this._CPF = value;
                OnPropertyChanged();
            }
            get
            {
                return this._CPF;
            }
        }

        private string _Cidade;
        public string Cidade
        {
            set
            {
                this._Cidade = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Cidade;
            }
        }

        private string _Telefone;
        public string Telefone
        {
            set
            {
                this._Telefone = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Telefone;
            }
        }

        private string _Sexo;
        public string Sexo
        {
            set
            {
                this._Sexo = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Sexo;
            }
        }

        private string _Senha;
        public string Senha
        {
            set
            {
                this._Senha = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Senha;
            }
        }

        private string _VSenha;
        public string VSenha
        {
            set
            {
                this._VSenha = value;
                OnPropertyChanged();
            }
            get
            {
                return this._VSenha;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }
        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Result;
            }
        }

        public Command CadastrarUsuario { get; set; }

        public CadastrarUsuarioViewModel()
        {
            CadastrarUsuario = new Command(async () => await CadastrarCommandAsync());
        }

        private async Task CadastrarCommandAsync()
        {
            if (String.IsNullOrEmpty(Nome))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo nome vazio", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo email vazio", "Ok");
                return;
            }

            if (String.IsNullOrEmpty(CPF))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo CPF vazio", "Ok");
                return;
            }

            if (String.IsNullOrEmpty(Telefone))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo telefone vazio", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Sexo))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Opção sexo não escolhido", "Ok");
                return;
            }

            if (String.IsNullOrEmpty(Cidade))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Opção sexo não escolhido", "Ok");
                return;
            }

            if (String.IsNullOrEmpty(Senha))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo senha vazio", "Ok");
                return;
            }

            if (Senha == VSenha)
            {
                if (IsBusy)
                    return;
                try
                {
                    IsBusy = true;
                    var userService = new FirebaseService();
                    Result = await userService.RegisterUser(Nome, Email, CPF, Telefone, Sexo, Cidade, Senha);
                    if (Result)
                    {
                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário Registrado com sucesso", "Ok");
                        App.Current.MainPage = new NavigationPage(new Login());
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Ops!", "CPF já existente", "Ok");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Senhas não confere.", "Ok");
            }
        }
    }
}
