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
    public partial class VideosList : ContentPage
    {
        Metodos metodos = new Metodos();
        public VideosList()
        {
            InitializeComponent();
            ObtenerVideos();
        }

        public async void ObtenerVideos()
        {
            var datos = await metodos.GetVideos();
            lsv_Videos.ItemsSource = datos;
        }

        private async void BtnAtrasVideos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnVideoClick_Clicked(object sender, EventArgs e)
        {

            var b = (Button)sender;

            var ob = b.CommandParameter as EVideos;

            if (ob != null)
            {

                // retrieve the value from the ‘ob’ and continue your work.

            }

            App.LinkVideo = ob.link;
            App.TituloVideo = ob.title;
            App.VideoDescription = ob.videodescription;

            //CrossMediaManager.Current.Play(App.LinkVideo, MediaFileType.Video);

            await Navigation.PushModalAsync(new VideosPage());

        }
    }
}