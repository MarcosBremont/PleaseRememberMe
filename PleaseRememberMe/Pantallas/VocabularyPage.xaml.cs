using Acr.UserDialogs;
using PleaseRememberMe.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PleaseRememberMe.Pantallas
{
    public partial class VocabularyPage : ContentPage
    {
        Metodos metodos = new Metodos();
        public VocabularyPage()
        {
            InitializeComponent();
            llenarVocabulary();
        }

        public async void llenarVocabulary()
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                var datos = await metodos.GetVocabularyGlobal(App.Vocabulary);
                lsv_vocabulary.ItemsSource = datos;
                LblVocabulary.Text = datos[0].Vocabulary_category + " Vocabulary";
                LblCategoryTitle.Text = datos[0].Vocabulary_category;
            }
        }

        private async void BtnAtrasVocabulary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

}

