using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Plugin.TextToSpeech;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.TextToSpeech.Abstractions;
using System.Globalization;
using System.Threading;
using PleaseRememberMe.Entidad;
using MediaManager;
using PositionChangedEventArgs = MediaManager.Playback.PositionChangedEventArgs;
using MediaManager.Player;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Media;
using PleaseRememberMe.Utilitarios;

namespace PleaseRememberMe.Pantallas
{
    public partial class PrincipalPage : ContentPage
    {
        ModalTournament modalTournament = new ModalTournament();
        public string previousPageValue; //Declare here

        private bool _userTapped;
        public PrincipalPage()
        {
            InitializeComponent();
            App.PrincipalPage = this;
            lblWord.Text = App.Wordsss;
            StackTournament.GestureRecognizers.Add(
             new TapGestureRecognizer()
             {
                 Command = new Command(async () =>
                 {
                     if (_userTapped)
                         return;

                     _userTapped = true;
                     modalTournament = new ModalTournament();
                     modalTournament.OnLLamarOtraPantalla += ModalTournament_OnLLamarOtraPantalla;

                     await PopupNavigation.PushAsync(modalTournament);
                     await Task.Delay(1000);
                     _userTapped = false;
                     Opacity = 1;
                 }),
                 NumberOfTapsRequired = 1

             }
           );

        }


        async private void ModalTournament_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TournamentPage());
            App.SumaTotalDePuntos = 0;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Anuncio.IsVisible = true;
            lblWord.Text = previousPageValue;
            

        }

        public void ObtenerValorLabel()
        {

            lblWord.IsVisible = false;
            lblWord.Text = "";
            lblWord.Text = App.Wordsss;
            lblWord.IsVisible = true;
            App.PrincipalPage = this;

        }

        private async void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Verbs());
        }

        private async void BtnTablaDePosiciones_Clicked(object sender, EventArgs e)
        {
            BtnTablaDePosiciones.IsEnabled = false;
            try
            {
                using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
                {
                    Anuncio.IsVisible = true;
                    await Navigation.PushModalAsync(new Leaderboard());

                }
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                BtnTablaDePosiciones.IsEnabled = true;

                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }

            BtnTablaDePosiciones.IsEnabled = true;
        }



        private async void BtnTournament_Clicked(object sender, EventArgs e)
        {
            if (_userTapped)
                return;

            _userTapped = true;
            modalTournament = new ModalTournament();
            modalTournament.OnLLamarOtraPantalla += ModalTournament_OnLLamarOtraPantalla;
            //modalTournament.Disappearing += ModalTournament_Disappearing;

            await PopupNavigation.PushAsync(modalTournament);
            await Task.Delay(1000);
            _userTapped = false;
            Opacity = 1;
        }



        public async void BtnListOfTheVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                BtnListOfTheVerbs.IsEnabled = false;
                using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
                {
                    Anuncio.IsVisible = true;
                    await Navigation.PushModalAsync(new VerbList());
                }
                BtnListOfTheVerbs.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }
        }
        public async void btnAjustes_Clicked(System.Object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                SettingsPage settingsPage = new SettingsPage();
                Anuncio.IsVisible = false;
                await Navigation.PushModalAsync(new SettingsPage());
            }

        }

        void BtnAtrasSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                Anuncio.IsVisible = true;
                StacklayoutPrincipal.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            }
        }


        private void BtnAtrasOtherTopics_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = false;
            Anuncio.IsVisible = true;
            StacklayoutPrincipal.IsVisible = true;
            BtnLetsGo.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
        }


        private async void BtnCategories_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = true;
            Anuncio.IsVisible = true;
            UserDialogs.Instance.ShowLoading("Wait, Do you want coffee?");
            await Navigation.PushModalAsync(new CategoriesPage());
            UserDialogs.Instance.HideLoading();
        }

    }
}
