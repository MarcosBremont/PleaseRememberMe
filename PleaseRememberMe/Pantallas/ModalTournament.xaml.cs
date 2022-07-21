using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PleaseRememberMe.Models;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalTournament : Rg.Plugins.Popup.Pages.PopupPage
    {
        Metodos metodos = new Metodos();

        public event EventHandler OnLLamarOtraPantalla;

        public ModalTournament()
        {
            InitializeComponent();
            OnLLamarOtraPantalla?.Invoke(this, EventArgs.Empty);
        }


        private async void BtnEnter_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Starting the tournament");
            App.nombrePersona = txtnickname.Text;
            App.direccion = txtcity.Text;
            //var apiResult = await metodos.EnterToTheTournament(txtnickname.Text, App.SumaTotalDePuntos, txtcity.Text);
            UserDialogs.Instance.HideLoading();
            App.Torneo = "S";
            txtcity.Text = "";
            txtnickname.Text = "";
            OnLLamarOtraPantalla(sender, e);
            await PopupNavigation.PopAsync();


        }

        private async void BtnCancell_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

            txtcity.Text = "";
            txtnickname.Text = "";


        }
    }
}