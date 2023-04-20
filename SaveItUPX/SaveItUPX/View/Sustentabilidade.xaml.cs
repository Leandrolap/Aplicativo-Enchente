using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveItUPX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sustentabilidade : ContentPage
    {
        public Sustentabilidade()
        {
            InitializeComponent();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://brasil.un.org/pt-br/sdgs"), BrowserLaunchMode.SystemPreferred);
        }
    }
}