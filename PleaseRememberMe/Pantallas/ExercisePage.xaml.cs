using PleaseRememberMe.Entidad;
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
        string TextoCategoria = "";
        public ExercisePage(ECategories TextoCategoria)
        {
            InitializeComponent();
        }


        public async void CargarInformacionTitlePage(string category)
        {
            UserDialogs.Instance.ShowLoading("Did you drink water today?");
            var datos = await metodos.GetExercisesByCategory(category);
            LbltitlePageBase.Text = datos[0].Title;
            LblAnotherTitle.Text = datos[0].AnotherTitle;
            LblSubtitlePageBase.Text = datos[0].Subtitle;
            LblDescriptionPageBase.Text = datos[0].Description;
            LblSentencesPageBase.Text = datos[0].Sentences;
            string YourAnswer = TxtAnswer.Text;
            DefinitiveAnswer = datos[0].CorrectAnswer;
            DefinitiveAnswer2 = datos[0].CorrectAnswer2;
            UserDialogs.Instance.HideLoading();

        }

        private void BtnCheckMyAnswerPageBase_Clicked(object sender, EventArgs e)
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

        private void BtnAtrasPageBase_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Hey, how's going?");
            StackLayoutHowToUse.IsVisible = true;
            LimpiarPageBase();
            CargarInformacionHowToUse(TextoCategoria);
            StackLayoutBase.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
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
            UserDialogs.Instance.ShowLoading("What time is it?");
            CargarInformacionTitlePage(TextoCategoria);
            TxtAnswer.Text = "";
            LblCorrectIncorectPageBase.IsVisible = false;
            UserDialogs.Instance.HideLoading();
        }
    }
}