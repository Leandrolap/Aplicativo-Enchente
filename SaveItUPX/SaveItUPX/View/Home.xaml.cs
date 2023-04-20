using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SaveItUPX.Popup;
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
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            VerificaPermissao();
        }

        private async void onFrameMapa(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MapaAoVivo());
        }

        private async void onFrameTempo(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PrevisaodoTempo());
        }

        private async void onFrameApoio(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CasadeApoio());
        }

        private void onFrameEmergencia(object sender, EventArgs e)
        {

        }

        private async void OnClickTel(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ServicosCall());
        }

        public async void VerificaPermissao()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //location denied
                }
            }
            catch (Exception)
            {
                //Something went wrong
            }
        }
    }
}