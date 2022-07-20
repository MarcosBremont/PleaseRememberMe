using PleaseRememberMe.Entidad;
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
    public partial class AudioList : ContentPage
    {
        Metodos metodos = new Metodos();
        public AudioList()
        {
            InitializeComponent();
            ObtenerAudios();
        }

        public async void ObtenerAudios()
        {
            try
            {
                var datos = await metodos.GetAudios();
                lsv_audios.ItemsSource = datos;
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }

        }

        async private void BtnAudioClick_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;

            var ob = b.CommandParameter as EAudios;

            if (ob != null)

            {

                // retrieve the value from the ‘ob’ and continue your work.

            }

            App.AudioTitle = ob.title;
            App.AudioLink = ob.link;
            await Navigation.PushModalAsync(new AudioPage());
        }

        private async void BtnAtrasAudios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}