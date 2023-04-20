using Plugin.Media;
using Plugin.Media.Abstractions;
using SaveItUPX.Model;
using SaveItUPX.Service;
using SaveItUPX.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaveItUPX.ViewModel
{
    public class PerfilViewModel : BaseViewModel
    {
        FirebaseService firebaseService = new FirebaseService();
        MediaFile arquivoMediaFile;

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

        private string _Endereco;
        public string Endereco
        {
            set
            {
                this._Endereco = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Endereco;
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

        private string _Photo;
        public string Photo
        {
            set
            {
                this._Photo = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Photo;
            }
        }

        private ImageSource _MinhaFoto;
        public ImageSource MinhaFoto
        {
            set
            {
                this._MinhaFoto = value;
                OnPropertyChanged();
            }
            get
            {
                return this._MinhaFoto;
            }
        }

        public Command BTNPhoto { get; set; }
        public Command BTNAtualizar { get; set; }
        public Command BTNSair { get; set; }

        public PerfilViewModel()
        {
            Usuario();
            PegarUsuarioFoto();

            BTNPhoto = new Command(async () => await PhotoCommandAsync());
            BTNAtualizar = new Command(async () => await AtualizarCommandAsync());
            BTNSair = new Command(SairCommandAsync);
        }

        //Carrega informações do usuário
        public async void Usuario()
        {
            var lista = await firebaseService.ObterUserReset(DBSAveIt.Login);

            if (lista != null)
            {
                Nome = lista.Nome;
                Email = lista.Email;
                CPF = lista.CPF;
                Endereco = lista.Endereco;
                Telefone = lista.Telefone;
                Cidade = lista.Cidade;
                Senha = lista.Senha;
                DBSAveIt.NomeImagem = lista.NImagemPerfil;

                PegarUsuarioFoto();
            }
        }

        //Acessa o armazenamento para escolher a foto
        public async Task PhotoCommandAsync()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                arquivoMediaFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                        new Plugin.Media.Abstractions.PickMediaOptions
                        {
                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                        });

                if (arquivoMediaFile == null)
                    return;

                MinhaFoto = ImageSource.FromStream(() =>
                {
                    var imageStram = arquivoMediaFile.GetStream();
                    return imageStram;


                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            CarregarUsuario();
        }

        //Faz upload da foto
        public async void CarregarUsuario()
        {
            await firebaseService.UploadFile(arquivoMediaFile.GetStream(), Path.GetFileName(arquivoMediaFile.Path));

            await AtualizarCommandAsync();
        }

        public async void PegarUsuarioFoto()
        {
            if (DBSAveIt.NomeImagem != "")
            {
                string path = await firebaseService.GetFile(DBSAveIt.NomeImagem);
                if (path != null)
                {
                    MinhaFoto = path;

                }
            }
            else
                MinhaFoto = "perfil.png";
        }

        public void SairCommandAsync()
        {
            DBSAveIt.Login = "";
            DBSAveIt.Local = "";
            DBSAveIt.NomeImagem = "";
            DBSAveIt.Contato01 = "";
            DBSAveIt.Contato02 = "";
            DBSAveIt.Contato03 = "";
            DBSAveIt.Telefone01 = "";
            DBSAveIt.Telefone02 = "";
            DBSAveIt.Telefone03 = "";

            // var lista = await firebaseService.ObterUserReset(Settings.Login);
            //await firebaseService.AttUsuario(Nome, Email, CPF, Endereco, Telefone, lista.Sexo, lista.Favorito, Cidade, Senha, Settings.NomeImagem);

            App.Current.MainPage = new Login();
        }

        private async Task AtualizarCommandAsync()
        {
            var lista = await firebaseService.ObterUserReset(DBSAveIt.Login);
            await firebaseService.AttUsuario(Nome, Email, CPF, Endereco, Telefone, lista.Sexo, lista.Favorito, Cidade, Senha, DBSAveIt.NomeImagem);
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Informações atualizadas!!!", "OK");
            //PegarUsuarioFoto();
        }
    }
}
