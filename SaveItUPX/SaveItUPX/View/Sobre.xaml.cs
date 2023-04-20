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
    public partial class Sobre : ContentPage
    {
        public Sobre()
        {
            InitializeComponent();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://sites.google.com/view/trocafaciltemos/in%C3%ADcio"), BrowserLaunchMode.SystemPreferred);
        }
    }
}