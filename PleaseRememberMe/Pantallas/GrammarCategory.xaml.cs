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


        private async void btnComparisons_Clicked(object sender, EventArgs e)
        {
            App.Categoria = "COMPARISONS";
            await Navigation.PushModalAsync(new HowToUsePage());
        }

        private async void btnPronouns_Clicked(object sender, EventArgs e)
        {
            try
            {

                //StackLayoutPronouns.IsVisible = true;
                //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                //UserDialogs.Instance.ShowLoading("Did you drink water today?");
                //var datos = await metodos.GetPronouns();
                //listpronouns = datos;
                //var random = new Random().Next(1, listpronouns.Count);
                //var elegido = listpronouns[random];
                //lblPronouns.Text = listpronouns[0].PronounsSentences;
                //CorrectAnswerPronoun = listpronouns[0].CorrectAnswer;
                //TxtCorrectAnswerPronouns.Text = "";
                //LblCorrectAnswerPronouns.IsVisible = false;
                ////listpronouns.Remove(elegido);
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private async void btnQuantifiers_Clicked(object sender, EventArgs e)
        {
            try
            {

                //StackLayoutQuantifiers.IsVisible = true;
                //LblCorrectAnswerQuantifiers.IsVisible = false;
                //RadioButtonOption1.IsChecked = false;
                //RadioButtonOption2.IsChecked = false;
                //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                //UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a tea");
                //var datos = await metodos.GetQuantifiers();
                //listQuantifiers = datos;
                //var random = new Random().Next(1, listQuantifiers.Count);
                //var elegido = listQuantifiers[random];
                //LblQuantifiers.Text = listQuantifiers[0].QuantifiersSentence;
                //LblFirstOption.Text = listQuantifiers[0].FirstOption;
                //LblSecondOption.Text = listQuantifiers[0].SecondOption;
                //CorrectAnswerQuantifiers = listQuantifiers[0].CorrectAnswer;
                //listQuantifiers.Remove(elegido);
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }


        private async void btnQuestionsHow_Clicked(object sender, EventArgs e)
        {
            try
            {

                //StackLayoutQuestionsWithHow.IsVisible = true;
                //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                //UserDialogs.Instance.ShowLoading("Hey, do you like tomatoes?");
                //var datos = await metodos.GetQuestionsWithHow();
                //listQuestionsWithHow = datos;
                //var random = new Random().Next(1, listQuestionsWithHow.Count);
                //var elegido = listQuestionsWithHow[random];
                //lblQuestionWithHow.Text = listQuestionsWithHow[0].Questionswithhow_sentences;
                //CorrectAnswerQuestionWithHow = listQuestionsWithHow[0].CorrectAnswer;
                ////listQuestionsWithHow.Remove(elegido);
                //TxtxCorrectAnswerQuestionWithHow.Text = "";
                //LblCorrectAnswerQuestionWithHow.IsVisible = false;
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }


        private async void btnPrepositionsoftime_Clicked(object sender, EventArgs e)
        {
            try
            {

                //StackLayoutPreposisionsOfTime.IsVisible = true;
                //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                //UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                //var datos = await metodos.GetPrepositionsOfTimeSentences();
                //listprepositionsOfTime = datos;
                //var random = new Random().Next(1, listprepositionsOfTime.Count);
                //var elegido = listprepositionsOfTime[random];
                //lblPrepositionsOfTime.Text = listprepositionsOfTime[0].PrepositionsOfTimeSentence;
                //CorrectAnswerPrepositionsOfTime = listprepositionsOfTime[0].CorrectAnswer;
                ////listprepositionsOfTime.Remove(elegido);
                //TxtPrepositionsOfTime.Text = "";
                //LblCorrectAnswerPrepositionsOfTime.IsVisible = false;

                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void btnPresentPerfect_Clicked(object sender, EventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("Why are you studying english?");

            //TextoCategoria = "Present Perfect";
            //CargarInformacionHowToUse(TextoCategoria);
            //StackLayoutHowToUse.IsVisible = true;
            //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            //UserDialogs.Instance.HideLoading();
        }


        private void btnEvaluationsAndComparions_Clicked(object sender, EventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("How was your day?");
            //TextoCategoria = "Evaluations And Comparisons";
            //StackLayoutHowToUse.IsVisible = true;
            ////CargarInformacionTitlePage("EvaluationsAndComparions");
            //CargarInformacionHowToUse(TextoCategoria);

            //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            //UserDialogs.Instance.HideLoading();
        }


        private async void btnAnySome_Clicked(object sender, EventArgs e)
        {
            try
            {

                //StackLayoutAnySome.IsVisible = true;
                //ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                //UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating pizza");
                //var datos = await metodos.GetAnySome();
                //listAnySome = datos;
                //var random = new Random().Next(1, listAnySome.Count);
                //var elegido = listAnySome[random];
                //LblAnySome.Text = listAnySome[0].AnySomeSentences;
                //CorrectAnswerAnySome = listAnySome[0].CorrectAnswer;
                ////listAnySome.Remove(elegido);
                //TxtAnySome.Text = "";
                //LblCorrectAnswerAnySome.IsVisible = false;
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }


    }
}