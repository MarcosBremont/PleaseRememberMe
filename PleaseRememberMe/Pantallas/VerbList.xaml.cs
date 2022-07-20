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
    public partial class VerbList : ContentPage
    {
        Metodos metodos = new Metodos();
        public VerbList()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {

            base.OnAppearing();
            Anuncio.IsVisible = true;

            try
            {
                var datos = await metodos.GetListadoVerbos();
                lsv_ListaDeVerbos.ItemsSource = datos;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");
            }

        }
        public async void BtnAtrasVerbList_Clicked(System.Object sender, System.EventArgs e)
        {
            BtnAtrasVerbList.IsEnabled = false;
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                await Navigation.PopModalAsync();
            }
            BtnAtrasVerbList.IsEnabled = true;
        }
    }
}