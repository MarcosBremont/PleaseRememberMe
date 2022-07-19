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


        private async void btnVocabularyPhrasalVerbs_Clicked(object sender, EventArgs e)
        {
            App.Vocabulary = "Phrasal Verbs";
            await Navigation.PushModalAsync(new VocabularyPage());

        }

        private async void btnFamilyVocabulary_Clicked(object sender, EventArgs e)
        {
            App.Vocabulary = "Family";
            await Navigation.PushModalAsync(new VocabularyPage());
        }

        private async void btnProfessionsVocabulary_Clicked(object sender, EventArgs e)
        {
            App.Vocabulary = "Professions";
            await Navigation.PushModalAsync(new VocabularyPage());
        }

        private async void btnClothesVocabulary_Clicked(object sender, EventArgs e)
        {
            App.Vocabulary = "Clothes";
            await Navigation.PushModalAsync(new VocabularyPage());
        }
    }
}

