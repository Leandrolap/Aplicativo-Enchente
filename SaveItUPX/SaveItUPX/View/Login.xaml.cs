using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveItUPX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void OnClickRecuperarSenha(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RecSenha());
        }

        private async void OnClickRegistro(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CadastrarUsuario());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}