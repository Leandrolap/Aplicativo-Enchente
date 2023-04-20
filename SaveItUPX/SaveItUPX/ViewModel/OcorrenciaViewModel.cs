using Plugin.Geolocator;
using SaveItUPX.Model;
using SaveItUPX.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaveItUPX.ViewModel
{
    public class OcorrenciaViewModel : BaseViewModel
    {
        FirebaseService firebaseService = new FirebaseService();

        public double Latitude = 0;
        public double Longitude = 0;

        public string DtAtual = DateTime.Now.ToString("dd/MM/yyyy");

        private string _Situacao;
        public string Situacao
        {
            set
            {
                this._Situacao = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Situacao;
            }
        }

        private string _Rota;
        public string Rota
        {
            set
            {
                this._Rota = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Rota;
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

        public Command EnviarOcorrencia { get; set; }

        public OcorrenciaViewModel()
        {
            EnviarOcorrencia = new Command(async () => await EnviarOcorrenciaAsync());
            LocalizacaoAtual();
        }

        private async Task EnviarOcorrenciaAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new FirebaseService();

                var lista = await firebaseService.ObterUserReset(DBSAveIt.Login);

                Result = await userService.RegisterOcorrencia(Situacao, Latitude.ToString(), Longitude.ToString(), Rota, DtAtual, lista.Nome);

                if (Result)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Ocorrência Registrada com sucesso", "Ok");
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

        public async void LocalizacaoAtual()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();

            Latitude = position.Latitude;
            Longitude = position.Longitude;
        }
    }
}
