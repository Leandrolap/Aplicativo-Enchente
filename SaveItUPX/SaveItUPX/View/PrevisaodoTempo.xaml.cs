using SaveItUPX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SaveItUPX.Model.Previsao;

namespace SaveItUPX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrevisaodoTempo : ContentPage
    {
        public PrevisaodoTempo()
        {
            InitializeComponent();

            //this.Title = "Previsão do Tempo";
            //define o databinding para os objetos padrão iniciais
            this.BindingContext = new Tempo();
            //Temperatura();
        }

        protected async override void OnAppearing()
        {
            try
            {
                if (!String.IsNullOrEmpty(DBSAveIt.Local))
                {
                    Tempo previsaoDoTempo = await WeatherAPI.GetPrevisaoDoTempo(DBSAveIt.Local);
                    double Tdouble = previsaoDoTempo.Temperature;
                    var intVal = System.Convert.ToInt32(System.Math.Floor(Tdouble));

                    tempLabel.Text = intVal.ToString();
                    this.BindingContext = previsaoDoTempo;
                }
                // else
                //await DisplayAlert("Atenção", "Acesse as configurações e adicione uma cidade.", "OK");
                //await Navigation.PushPopupAsync(new AdicionarCidade());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "OK");
            }
        }
    }
}