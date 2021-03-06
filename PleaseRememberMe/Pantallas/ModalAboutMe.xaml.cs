using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms;

namespace PleaseRememberMe.Pantallas
{
    public partial class ModalAboutMe : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        public ModalAboutMe()
        {
            InitializeComponent();
        }

        async void BtnGoBack_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void BtnInstagram_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("instagram://user?username=marcosbremont"));
        }


        async void BtnWhatsapp_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Chat.Open("+18099073244", "Hii");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        void BtnInstagram2_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("instagram://user?username=app.rememberme"));

        }
    }
}
