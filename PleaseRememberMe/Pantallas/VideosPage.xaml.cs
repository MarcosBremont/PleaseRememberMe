using PleaseRememberMe.Entidad;
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
    public partial class VideosPage : ContentPage
    {
        public VideosPage()
        {
            InitializeComponent();
            StackLayoutVideoPage.IsVisible = true;
            LblTitle.Text = App.TituloVideo;
            LblDescriptionVideo.Text = App.VideoDescription;
            mediaElement.Source = App.LinkVideo;
        }

       
        private async void BtnAtrasVideoPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            mediaElement.Pause();
        }
    }
}