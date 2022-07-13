using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Rg.Plugins.Popup.Services;
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
    public partial class SettingsPage : ContentPage
    {

        Metodos metodos = new Metodos();
        private bool _userTapped;
        ModalAboutMe modalAboutMe = new ModalAboutMe();

        public SettingsPage()
        {
            //OnCloseForm += SettingsPage_OnCloseForm;

            InitializeComponent();
            StackLayoutAboutMe.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(async () =>
                    {
                        if (_userTapped)
                            return;

                        _userTapped = true;
                        modalAboutMe = new ModalAboutMe();
                        modalAboutMe.OnLLamarOtraPantalla += ModalAboutMe_OnLLamarOtraPantalla;
                        modalAboutMe.Disappearing += ModalAboutMe_Disappearing;

                        await PopupNavigation.PushAsync(modalAboutMe);
                        await Task.Delay(1000);
                        _userTapped = false;
                        Opacity = 1;
                    }),
                    NumberOfTapsRequired = 1

                }
              );
        }

        private void SettingsPage_OnCloseForm(object sender, EventArgs e)
        {
            
        }

        async void BtnSaveChanges_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Saving Email, give me a few seconds");
                var apiResult = await metodos.SendEmails(txtEmail.Text);
                if (apiResult.Respuesta == "OK")
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Email Saved, you are going to receive all the news in your email");
                    txtEmail.Text = "";
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Error, check your internet connection");

                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }



        }

        private void ModalAboutMe_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
        }

        private void ModalAboutMe_Disappearing(object sender, EventArgs e)
        {
        }

        private async void BtnReport_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Sending report, give me a few seconds");
                var apiResult = await metodos.SendReport(txtReport.Text);
                if (apiResult.Respuesta == "OK")
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Report Sent, thanks for report a problem");
                    txtReport.Text = "";
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Error, check your internet connection");

                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }
        async void BtnAtrasSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
            await Navigation.PushModalAsync(new PrincipalPage());
            UserDialogs.Instance.HideLoading();

        }
    }
}