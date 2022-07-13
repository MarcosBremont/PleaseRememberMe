using Acr.UserDialogs;
using PleaseRememberMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Leaderboard : ContentPage
    {
        Metodos metodos = new Metodos();
        public Leaderboard()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            //Anuncio.IsVisible = true;

            try
            {
                var datos = await metodos.GetListadoDePosiciones();
                lsv_TablaPuntuacion.ItemsSource = datos;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");
            }

        }

        private async void BtnAtrasPosiciones_Clicked(object sender, EventArgs e)
        {
            BtnAtrasPosiciones.IsEnabled = false;
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                await Navigation.PushModalAsync(new PrincipalPage());
            }
            BtnAtrasPosiciones.IsEnabled = true;
        }
    }
}