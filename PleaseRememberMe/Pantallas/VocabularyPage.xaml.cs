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
            var datos = await metodos.GetVocabularyGlobal(App.Vocabulary);
            lsv_vocabulary.ItemsSource = datos;
            LblVocabulary.Text = datos[0].Vocabulary_category + " Vocabulary";
            LblCategoryTitle.Text = datos[0].Vocabulary_category;
        }

        private async void BtnAtrasVocabulary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new VocabularyCategory());
        }
    }

}

