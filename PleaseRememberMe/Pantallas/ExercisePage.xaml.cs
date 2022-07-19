using Acr.UserDialogs;
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
    public partial class ExercisePage : ContentPage
    {
        List<Entidad.EExercises> listExercises = new List<Entidad.EExercises>();

        string DefinitiveAnswer = "", DefinitiveAnswer2 = "",
          TextoCategoria = "", AnswerRadioButton1 = "",
            AnswerRadioButton2 = "", CorrectAnswerQuantifiers = "";
        Metodos metodos = new Metodos();
        public ExercisePage()
        {
            InitializeComponent();
            CargarInformacionTitlePage();
        }


        public async void CargarInformacionTitlePage()
        {
            UserDialogs.Instance.ShowLoading("Did you drink water today?");
            var datos = await metodos.GetExercisesByCategory(App.Categoria);
            listExercises = datos;

            if (App.Categoria == "QUANTIFIERS")
            {
                StackRadioButtons.IsVisible = true;
                LblFirstOption.Text = listExercises[0].FirstOption;
                LblSecondOption.Text = listExercises[0].SecondOption;
                RadioButtonOption1.IsChecked = false;
                RadioButtonOption2.IsChecked = false;
                TxtAnswer.IsVisible = false;
                CorrectAnswerQuantifiers = listExercises[0].CorrectAnswer;
            }

            LbltitlePageBase.Text = listExercises[0].Title;
            LblAnotherTitle.Text = listExercises[0].AnotherTitle;
            LblSubtitlePageBase.Text = listExercises[0].Subtitle;
            LblDescriptionPageBase.Text = listExercises[0].Description;
            LblSentencesPageBase.Text = listExercises[0].Sentences;
            imgclothe.Source = listExercises[0].Image;
            if (string.IsNullOrEmpty(listExercises[0].Image))
            {
                LayoutImage.IsVisible = false;
            }
            else
            {
                LayoutImage.IsVisible = true;
            }

            string YourAnswer = TxtAnswer.Text;
            DefinitiveAnswer = datos[0].CorrectAnswer;
            DefinitiveAnswer2 = datos[0].CorrectAnswer2;
            UserDialogs.Instance.HideLoading();
        }

        private void RadioButtonOption1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            AnswerRadioButton1 = LblFirstOption.Text;
            AnswerRadioButton2 = "";
        }

        private void RadioButtonOption2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            AnswerRadioButton2 = LblSecondOption.Text;
            AnswerRadioButton1 = "";

        }

        private void BtnCheckMyAnswerPageBase_Clicked(object sender, EventArgs e)
        {
            if (App.Categoria == "QUANTIFIERS")
            {

                if (AnswerRadioButton1 == CorrectAnswerQuantifiers || AnswerRadioButton2 == CorrectAnswerQuantifiers)
                {
                    LblCorrectIncorectPageBase.IsVisible = true;
                    LblCorrectIncorectPageBase.Text = "Correct";
                }
                else
                {
                    LblCorrectIncorectPageBase.IsVisible = true;
                    LblCorrectIncorectPageBase.Text = "Incorrect";

                }
            }
            else
            {
                if (string.IsNullOrEmpty(TxtAnswer.Text))
                {
                    LblCorrectIncorectPageBase.IsVisible = true;
                    LblCorrectIncorectPageBase.Text = "Incorrect";
                }
                else
                {

                    if (DefinitiveAnswer == TxtAnswer.Text.ToUpper().Trim().Replace(".", "").Replace("‘", "'").Replace("’", "'").Replace("`", "'") || DefinitiveAnswer2 == TxtAnswer.Text.ToUpper().Trim().Replace(".", "").Replace("‘", "'").Replace("’", "'").Replace("`", "'"))
                    {
                        LblCorrectIncorectPageBase.IsVisible = true;
                        LblCorrectIncorectPageBase.Text = "Correct";
                    }
                    else
                    {
                        LblCorrectIncorectPageBase.IsVisible = true;
                        LblCorrectIncorectPageBase.Text = "Incorrect";

                    }
                }
            }


          
        }

        private async void BtnAtrasPageBase_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Hey, how's going?");
            await Navigation.PopModalAsync();

            LimpiarPageBase();

            UserDialogs.Instance.HideLoading();

        }

        public void LimpiarPageBase()
        {
            LbltitlePageBase.Text = "";
            LblSubtitlePageBase.Text = "";
            LblAnotherTitle.Text = "";
            LblDescriptionPageBase.Text = "";
            LblSentencesPageBase.Text = "";
            TxtAnswer.Text = "";
            LblSentencesPageBase.Text = "";

        }

        private void BtnOneMorePageBase_Clicked(object sender, EventArgs e)
        {
            var random = new Random().Next(1, listExercises.Count);

            if (listExercises.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var elegido = listExercises[random];
                listExercises.Remove(elegido);
                TxtAnswer.Text = "";
                LblCorrectIncorectPageBase.IsVisible = false;
                LblSentencesPageBase.Text = elegido.Sentences;
                DefinitiveAnswer = elegido.CorrectAnswer;
                DefinitiveAnswer2 = elegido.CorrectAnswer2;
                LblFirstOption.Text = elegido.FirstOption;
                LblSecondOption.Text = elegido.SecondOption;
                CorrectAnswerQuantifiers = elegido.CorrectAnswer;
                RadioButtonOption1.IsChecked = false;
                RadioButtonOption2.IsChecked = false;
                imgclothe.Source = elegido.Image;
            }
           
        }
    }
}