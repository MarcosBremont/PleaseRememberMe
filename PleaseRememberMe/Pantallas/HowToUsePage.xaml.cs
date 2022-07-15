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
    public partial class HowToUsePage : ContentPage
    {
        Metodos metodos = new Metodos();
        public HowToUsePage()
        {
            InitializeComponent();
            CargarInformacionHowToUse();
        }

        public async void CargarInformacionHowToUse()
        {
            UserDialogs.Instance.ShowLoading("What's the weather like?");
            var datos = await metodos.GetExamplesByCategory(App.Categoria);
            LblDescriptionHowToUseBase.Text = datos[0].Howtouse;
            LblTitleHowToUse.Text = datos[0].howtouse_category;
            btnGoToTheExercises.Text = datos[0].howtouse_category;
            UserDialogs.Instance.HideLoading();
        }


        private async void btnGoToTheExercises_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Can you give me a hand?");
            await Navigation.PushModalAsync(new ExercisePage());
            UserDialogs.Instance.HideLoading();
        }

        public void LimpiarCamposHowToUse()
        {
            LblTitleHowToUse.Text = "";
            LblDescriptionHowToUseBase.Text = "";
        }


        private async void BtnAtrasHowToUse_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Is everything ok?");
            await Navigation.PopModalAsync();

            LimpiarCamposHowToUse();
            UserDialogs.Instance.HideLoading();
        }

    }
}