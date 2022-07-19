using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PleaseRememberMe.Pantallas
{
    public partial class VocabularyCategory : ContentPage
    {
        public VocabularyCategory()
        {
            InitializeComponent();
        }


        private async void BtnAtrasVocabulary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoriesPage());
        }


        private void btnVocabularyPhrasalVerbs_Clicked(object sender, EventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("Can you give me a hand?");

            //TextoCategoria = "Phrasal Verbs";
            //CargarInformacionVocabularyGlobalPage(TextoCategoria);
            //StacklayoutVocabularyGlobal.IsVisible = true;
            //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            // UserDialogs.Instance.HideLoading();

        }

        private async void btnFamilyVocabulary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new VocabularyPage());
        }
    }
}

