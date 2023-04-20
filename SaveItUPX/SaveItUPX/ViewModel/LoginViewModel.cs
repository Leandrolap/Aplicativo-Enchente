using SaveItUPX.Model;
using SaveItUPX.Service;
using SaveItUPX.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaveItUPX.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        FirebaseService firebaseService = new FirebaseService();

        List<Usuario> Usuarios { get; set; }

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
        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
        }

        private async Task LoginCommandAsync()
        {
            //Verificação dos campos vazios
            if (String.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo email vazio", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Senha))
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campo senha vazio", "Ok");
                return;
            }

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new FirebaseService();
                Result = await userService.LoginUser(Email, Senha);

                var lista = await firebaseService.ObterUserReset(Email);

                if (Result)
                {
                    DBSAveIt.Login = Email;
                    DBSAveIt.NomedoUsuario = lista.Nome;
                    DBSAveIt.Local = lista.Cidade;
                    Preferences.Set("Username", Email);
                    App.Current.MainPage = new TelaInicial();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Email ou Senha inválido(s)", "Ok");
                }
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

        public ICommand ClickTermo => new Command<string>((url) =>
        {
            Launcher.OpenAsync(new Uri(url));
        });
    }
}
