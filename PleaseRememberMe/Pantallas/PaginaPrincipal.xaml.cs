using PleaseRememberMe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PleaseRememberMe.Pantallas
{
    public partial class PaginaPrincipal : ContentPage
    {
        Metodos metodos = new Metodos();
        public PaginaPrincipal()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var response = await metodos.GetListadoVerbos();
        }

        private void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            LblVerbInPast.IsVisible = true;
            LblVerbInPastSimple.IsVisible = true;
            LblVerbInPastParticiple.IsVisible = true;
            txtVerbInPast.IsVisible = true;
            txtVerbInPastSimple.IsVisible = true;
            txtVerbInPastParticiple.IsVisible = true;
            BtnLetsGo.IsVisible = false;
            BtnAnotherOne.IsVisible = true;
        }
    }
}
