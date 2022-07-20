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
    public partial class CategoriesPage : ContentPage
    {
        private bool _userTapped;
        ModalRememberMeAWord modalRememberMeAWord = new ModalRememberMeAWord();
        public CategoriesPage()
        {
            InitializeComponent();
        }


        private async void BtnAtrasCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnGrammarCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GrammarCategory());
        }


        private async void BtnVocabularyCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new VocabularyCategory());
        }

        private async void BtnVideos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new VideosList());
        }


        private async void BtnAudios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AudioList());
            //StackLayoutAudios.IsVisible = true;
            //var datos = await metodos.GetAudios();
            //lsv_audios.ItemsSource = datos;
        }

        private async void BtnRememberMeAWord_Clicked(object sender, EventArgs e)
        {
            if (_userTapped)
                return;

            _userTapped = true;
            modalRememberMeAWord = new ModalRememberMeAWord();
            modalRememberMeAWord.OnLLamarOtraPantalla += ModalRememberMeAWord_OnLLamarOtraPantalla; ;
            //modalTournament.Disappearing += ModalTournament_Disappearing;

            await PopupNavigation.PushAsync(modalRememberMeAWord);
            await Task.Delay(1000);
            _userTapped = false;
            Opacity = 1;
        }

        private void ModalRememberMeAWord_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
            
        }
    }
}