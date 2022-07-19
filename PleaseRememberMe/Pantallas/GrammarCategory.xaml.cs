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
    public partial class GrammarCategory : ContentPage
    {
        public GrammarCategory()
        {
            InitializeComponent();
        }

        private async void BtnVolverAtrasGrammarCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoriesPage());
        }

        private async void BtnMatch_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "PROFESSIONS";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        async void btnSClothes_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Categoria = "CLOTHES";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        async void BtnSimplePast_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Categoria = "SIMPLE PAST";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void BtnSimplePresentCategory_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "SIMPLE PRESENT";
            await Navigation.PushModalAsync(new HowToUsePage());
        }


        private async void btnVerbToBe_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "VERB TO BE";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btncomparatives_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "COMPARATIVE";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnSuperlatives_Clicked(object sender, EventArgs e)
        {

            App.Categoria = "SUPERLATIVE";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnComparisons_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "COMPARISONS";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnPronouns_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "DEMONSTRATIVE PRONOUNS";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnQuantifiers_Clicked(object sender, EventArgs e)
        {
                App.Categoria = "QUANTIFIERS";
                await Navigation.PushModalAsync(new HowToUsePage());
        }


        private async void btnQuestionsHow_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "QUESTIONS WITH HOW";
            await Navigation.PushModalAsync(new HowToUsePage());
        }


        private async void btnPrepositionsoftime_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "PREPOSITIONS OF TIME";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnPresentPerfect_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "PRESENT PERFECT";
            await Navigation.PushModalAsync(new HowToUsePage());
        }


        private async void btnEvaluationsAndComparions_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "EVALUATIONS AND COMPARISONS";
            await Navigation.PushModalAsync(new HowToUsePage());
        }


        private async void btnAnySome_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "ANY AND SOME";
            await Navigation.PushModalAsync(new HowToUsePage());
        }


    }
}