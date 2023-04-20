using Rg.Plugins.Popup.Pages;
using SaveItUPX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveItUPX.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicosCall : PopupPage
    {
        public ServicosCall()
        {
            InitializeComponent();

            Contatos();
        }

        private void OnClickDC(object sender, EventArgs e)
        {
            PhoneDialer.Open("199");
        }

        private void OnClickBB(object sender, EventArgs e)
        {
            PhoneDialer.Open("193");
        }

        private void OnClickSAMU(object sender, EventArgs e)
        {
            PhoneDialer.Open("192");
        }

        private void OnClickContato1(object sender, EventArgs e)
        {
            PhoneDialer.Open(DBSAveIt.Telefone01);
        }

        private void OnClickContato2(object sender, EventArgs e)
        {
            PhoneDialer.Open(DBSAveIt.Telefone02);
        }

        private void OnClickContato3(object sender, EventArgs e)
        {
            PhoneDialer.Open(DBSAveIt.Telefone03);
        }

        public void Contatos()
        {
            if (DBSAveIt.Contato01 != "")
            {
                FContato01.IsVisible = true;
                LblContato01.Text = DBSAveIt.Contato01;

                if (DBSAveIt.Contato02 != "")
                {
                    FContato02.IsVisible = true;
                    LblContato02.Text = DBSAveIt.Contato02;

                    if (DBSAveIt.Contato03 != "")
                    {
                        FContato03.IsVisible = true;
                        LblContato03.Text = DBSAveIt.Contato03;
                    }
                }
            }
        }
    }
}