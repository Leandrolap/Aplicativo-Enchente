using SaveItUPX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SaveItUPX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        FirebaseService firebaseLogin = new FirebaseService();

        public double a = 0;
        public double b = 0;

        public double j;
        public double k;

        string nome = "";
        string rotaalternativa = "";
        string idUser = "";

        public Mapa()
        {
            InitializeComponent();

            map.MyLocationEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.IsTrafficEnabled = true;
            local();
            MapaOcorrencia();
        }

        public void local()
        {
            Pin _pin = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(-13.4901761, -53.4144752),
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(_pin.Position, Distance.FromMeters(200000)), true);
        }

        public async void MapaOcorrencia()
        {
            var lista = await firebaseLogin.ObterOcorrencia(DateTime.Now.ToString("dd/MM/yyyy"));

            foreach (var item in lista)
            {
                if (lista != null)
                {
                    j = a = Convert.ToDouble(item.Latitude);
                    k = b = Convert.ToDouble(item.Longitude);
                    nome = item.UsuarioPost;
                    rotaalternativa = item.Rota;
                    idUser = item.Id;


                    Pin _pin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = item.Tipo,
                        Address = item.UsuarioPost,
                        Position = new Position(j, k),
                        Tag = item.Rota,
                    };

                    map.Pins.Add(_pin);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(_pin.Position, Distance.FromMeters(500)), true);
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Que legal", "Não há informações de ocorrência na região!!!", "OK");
            }


        }

        private void map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {

        }

        private void map_PinClicked(object sender, PinClickedEventArgs e)
        {
            if (e.Pin?.Tag != null)
            {
                PDetalhes.IsVisible = true;
                //LblNome.Text = $"{e?.Pin?.Tag}";
                LblRota.Text = $"{e?.Pin?.Tag}";
            }
            else
                PDetalhes.IsVisible = false;
        }
    }
}