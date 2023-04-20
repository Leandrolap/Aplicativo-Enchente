using SaveItUPX.Service;
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
    public partial class Configuracoes : ContentPage
    {
        string[] contato = new string[3];
        public Configuracoes()
        {
            InitializeComponent();
            CarregarContato();
        }

        private void OnClickAdd(object sender, EventArgs e)
        {
            if (AddContato.IsVisible == false)
            {
                AddContato.IsVisible = true;
            }
            else
                AddContato.IsVisible = false;
        }

        private void OnClickAddContato(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DBSAveIt.Contato01))
            {
                DBSAveIt.Contato01 = EntryContato.Text;
                DBSAveIt.Telefone01 = EntryTelefone.Text;

                FContato01.IsVisible = true;
                LblContato01.Text = DBSAveIt.Contato01;
                LblTelefone01.Text = DBSAveIt.Telefone01;
            }
            else if (string.IsNullOrEmpty(DBSAveIt.Contato02))
            {
                DBSAveIt.Contato02 = EntryContato.Text;
                DBSAveIt.Telefone02 = EntryTelefone.Text;

                FContato02.IsVisible = true;
                LblContato02.Text = DBSAveIt.Contato02;
                LblTelefone02.Text = DBSAveIt.Telefone02;
            }
            else if (string.IsNullOrEmpty(DBSAveIt.Contato03))
            {
                DBSAveIt.Contato03 = EntryContato.Text;
                DBSAveIt.Telefone03 = EntryTelefone.Text;

                FContato03.IsVisible = true;
                LblContato03.Text = DBSAveIt.Contato03;
                LblTelefone03.Text = DBSAveIt.Telefone03;
            }
        }

        public void CarregarContato()
        {
            if (!string.IsNullOrEmpty(DBSAveIt.Contato01) && !string.IsNullOrEmpty(DBSAveIt.Contato02) && !string.IsNullOrEmpty(DBSAveIt.Contato03))
            {
                FContato01.IsVisible = true;
                FContato02.IsVisible = true;
                FContato03.IsVisible = true;
                LblContato01.Text = DBSAveIt.Contato01;
                LblTelefone01.Text = DBSAveIt.Telefone01;
                LblContato02.Text = DBSAveIt.Contato02;
                LblTelefone02.Text = DBSAveIt.Telefone02;
                LblContato03.Text = DBSAveIt.Contato03;
                LblTelefone03.Text = DBSAveIt.Telefone03;
            }
        }



        private void OnClickLixo01(object sender, EventArgs e)
        {
            DBSAveIt.Contato01 = "";
            DBSAveIt.Telefone01 = "";
            FContato01.IsVisible = false;

        }

        private void OnClickLixo02(object sender, EventArgs e)
        {
            DBSAveIt.Contato02 = "";
            DBSAveIt.Telefone02 = "";
            FContato02.IsVisible = false;
        }

        private void OnClickLixo03(object sender, EventArgs e)
        {
            DBSAveIt.Contato03 = "";
            DBSAveIt.Telefone03 = "";
            FContato03.IsVisible = false;
        }
    }
}