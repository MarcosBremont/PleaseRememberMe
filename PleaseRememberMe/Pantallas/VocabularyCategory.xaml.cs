using Acr.UserDialogs;
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
            await Navigation.PopModalAsync();
        }


        private async void btnVocabularyPhrasalVerbs_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Phrasal Verbs";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnFamilyVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Family";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnProfessionsVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Professions";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnClothesVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Clothes";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnNatureVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Nature";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnCommonWordsVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Common Words";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }

        private async void btnClassroomVocabulary_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                App.Vocabulary = "Classroom Objects";
                await Navigation.PushModalAsync(new VocabularyPage());
            }
        }
    }
}

