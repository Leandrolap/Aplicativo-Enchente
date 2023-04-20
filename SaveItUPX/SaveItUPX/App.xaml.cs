using SaveItUPX.Service;
using SaveItUPX.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveItUPX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (DBSAveIt.Login != "")
            {
                MainPage = new TelaInicial();
            }
            else

                MainPage = new Login();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
