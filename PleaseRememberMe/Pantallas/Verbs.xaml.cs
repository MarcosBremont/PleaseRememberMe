using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Verbs : ContentPage
    {
        List<Entidad.EVerbos> listverbos = new List<Entidad.EVerbos>();
        Metodos metodos = new Metodos();
        string VerbInSimplePast = "", VerbInPastParticiple = "", Traduccion = "", CorrectAnswer = "",
            CorrectAnswerPronoun = "", CorrectAnswerVerb = "", CorrectAnswerQuestionWithHow = "", CorrectAnswerPrepositionsOfTime = "",
            CorrectAnswerFamily = "", CorrectAnswerAnySome = "", CorrectAnswerVerbToBe1 = "",
            CorrectAnswerVerbToBe2 = "", CorrectAnswerQuantifiers = "", AnswerRadioButton1 = "",
            AnswerRadioButton2 = "", audioVerboFormaBase = "", audioverboSimplePast = "", audioverboPasParticiple = "",
            ExamplePastSimple = "", ExamplePastParticiple = "", audiolink = "", DefinitiveAnswer = "", DefinitiveAnswer2 = "",
            TextoCategoria = "";

        IEnumerable<Locale> locales;
        static CrossLocale? localeee = null;
        public Verbs()
        {
            InitializeComponent();

           
        }


        protected async override void OnAppearing()
        {

            base.OnAppearing();
            //Anuncio.IsVisible = true;

            try
            {
                var lista = listverbos;
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
                listverbos = await metodos.GetListadoVerbos();

                locales = await TextToSpeech.GetLocalesAsync();
                GetRandomVerb();
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");
            }

        }

        public async void CargarIdioma()
        {
            var lenguaje = await CrossTextToSpeech.Current.GetInstalledLanguages();
            var items = lenguaje.Select(a => a.ToString()).ToArray();

            if (Device.RuntimePlatform == Device.Android)
            {

                localeee = lenguaje.FirstOrDefault(l => l.ToString() == "en-US");
            }
            else
            {
                localeee = new CrossLocale { Language = "en-US" };
            }
        }


        private async void BtnAtras_Clicked(object sender, EventArgs e)
        {
            BtnAtras.IsEnabled = false;
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
            await Navigation.PushModalAsync(new PrincipalPage());
            UserDialogs.Instance.HideLoading();
            App.Torneo = "N";
        }

        private async void BtnVerbInPast_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioVerboFormaBase, crossLocale: localeee);
        }


        private async void BtnAnotherOne_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Looking for verbs...");
            BtnVerbInPastSAudio.IsVisible = false;
            BtnVerbInPastPartAudio.IsVisible = false;
            if (App.Torneo == "S")
            {
                //GetRandomVerbForTheTournament();
            }
            else
            {
                await GetRandomVerb();

            }
            LimpiarText();
            StackExamples.HorizontalOptions = LayoutOptions.CenterAndExpand;
            UserDialogs.Instance.HideLoading();

        }

        public async Task GetRandomVerb()
        {
            try
            {
                LblVerbInPastSimpleCheck.Text = "";
                LblVerbInPastParticipleCheck.Text = "";
                var lista = listverbos;

                if (App.YaPasastePorGetRandomVerb == "Yes")
                {
                    listverbos = await metodos.GetListadoVerbos();

                }
                else
                {
                    if (App.Torneo == "N" || string.IsNullOrEmpty(App.Torneo))
                    {
                        listverbos = await metodos.GetListadoVerbos();
                        App.YaPasastePorGetRandomVerb = "Yes";

                    }
                    else
                    {
                        BtnVerbInPastAudio.IsVisible = true;
                        BtnVerbInPastSAudio.IsVisible = false;
                        BtnVerbInPastPartAudio.IsVisible = false;

                    }
                }

                var random = new System.Random().Next(1, listverbos.Count);
                var datos = listverbos[random];
                txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
                VerbInSimplePast = datos.verboSimplePast.ToUpper();
                VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
                Traduccion = datos.traduccion.ToUpper();
                audioVerboFormaBase = datos.VerboFormaBase.ToString();
                audioverboSimplePast = datos.verboSimplePast.ToString();
                audioverboPasParticiple = datos.verboPasParticiple.ToString();
                lblExamplePast.Text = datos.examplesInBaseForm;
                lblExamplePastSimple.Text = datos.examplesInSimplePast;
                ExamplePastSimple = datos.examplesInSimplePast;
                ExamplePastParticiple = datos.examplesInPastParticiple;
                lblExamplePastParticiple.Text = datos.examplesInPastParticiple;
                listverbos.Remove(datos);

                if (LblVerbInPastSimpleCheck.Text != "Correct")
                {
                    lblExamplePastSimple.Text = "...";
                }
                else
                {
                    lblExamplePastSimple.Text = ExamplePastSimple;

                }

                if (LblVerbInPastParticipleCheck.Text != "Correct")
                {
                    lblExamplePastParticiple.Text = "...";
                }
                else
                {
                    lblExamplePastParticiple.Text = ExamplePastParticiple;

                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

        }

        

        private void BtnGiveMeSomeExamples_Clicked(object sender, EventArgs e)
        {
            StackExamples.IsVisible = true;
            if (LblVerbInPastSimpleCheck.Text != "Correct")
            {
                lblExamplePastSimple.Text = "...";
            }
            else
            {
                lblExamplePastSimple.Text = ExamplePastSimple;

            }

            if (LblVerbInPastParticipleCheck.Text != "Correct")
            {
                lblExamplePastParticiple.Text = "...";
            }
            else
            {
                lblExamplePastParticiple.Text = ExamplePastParticiple;

            }
            StackExamples.HorizontalOptions = LayoutOptions.Fill;
        }

       
        private void BtnCheck_Clicked(object sender, EventArgs e)
        {
            try
            {
                //StackTraducciones.IsVisible = false;

                if (App.Torneo != "S")
                {
                    // StackTraducciones.IsVisible = true;
                    //LblTraduction.Text = Traduccion;
                    LblVerbInPastSimpleCheck.IsVisible = true;
                    LblVerbInPastParticipleCheck.IsVisible = true;

                }



                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                if (string.IsNullOrEmpty(txtVerbInPastSimple.Text))
                {
                    LblVerbInPastSimpleCheck.Text = "Incorrect";
                    player.Load("WrongSound.mp3");

                }
                else
                {
                    if (txtVerbInPastSimple.Text.ToUpper().Trim().Replace(".", "") == VerbInSimplePast)
                    {

                        LblVerbInPastSimpleCheck.Text = "Correct";
                        player.Load("CorrectSoundReady.mp3");
                        if (App.Torneo == "S")
                        {

                        }
                        else
                        {
                            BtnVerbInPastSAudio.IsVisible = true;
                        }

                    }
                    else
                    {

                        LblVerbInPastSimpleCheck.Text = "Incorrect";
                        player.Load("WrongSound.mp3");

                    }




                }

                if (string.IsNullOrEmpty(txtVerbInPastParticiple.Text))
                {
                    LblVerbInPastParticipleCheck.Text = "Incorrect";
                    player.Load("WrongSound.mp3");

                }
                else
                {
                    if (txtVerbInPastParticiple.Text.ToUpper().Trim().Replace(".", "") == VerbInPastParticiple)
                    {
                        LblVerbInPastParticipleCheck.Text = "Correct";
                        player.Load("CorrectSoundReady.mp3");
                        if (App.Torneo == "S")
                        {

                        }
                        else
                        {
                            BtnVerbInPastPartAudio.IsVisible = true;
                        }

                    }
                    else
                    {
                        if (player.CanSeek == false)
                        {
                            player.Load("WrongSound.xmp3");
                        }

                        LblVerbInPastParticipleCheck.Text = "Incorrect";

                    }


                }

                if (LblVerbInPastSimpleCheck.Text == "Correct" && LblVerbInPastParticipleCheck.Text == "Correct" && App.Torneo == "S")
                {
                    App.SumaTotalDePuntos = App.SumaTotalDePuntos + 1;
                    BtnVerbInPastAudio.IsVisible = true;
                    BtnVerbInPastPartAudio.IsVisible = true;
                    player.Load("CorrectSoundReady.mp3");
                }



                if (LblVerbInPastSimpleCheck.Text == "Incorrect" || LblVerbInPastParticipleCheck.Text == "Incorrect")
                {
                    player.Load("WrongSound.mp3");
                }

                if (App.Torneo == "S")
                {
                    //GetRandomVerbForTheTournament();
                    LimpiarText();
                    BtnVerbInPastAudio.IsVisible = true;
                    BtnVerbInPastSAudio.IsVisible = false;
                    BtnVerbInPastPartAudio.IsVisible = false;

                }

                if (LblVerbInPastSimpleCheck.Text != "Correct")
                {
                    lblExamplePastSimple.Text = "...";
                }
                else
                {
                    lblExamplePastSimple.Text = ExamplePastSimple;

                }


                if (LblVerbInPastParticipleCheck.Text != "Correct")
                {
                    lblExamplePastParticiple.Text = "...";
                }
                else
                {
                    lblExamplePastParticiple.Text = ExamplePastParticiple;

                }

                player.Play();

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

        }


        private async void BtnVerbInPastS_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioverboSimplePast, crossLocale: localeee);
        }

        private async void BtnVerbInPastPart_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioverboPasParticiple, crossLocale: localeee);

        }




        public void LimpiarText()
        {
            txtVerbInPastSimple.Text = "";
            txtVerbInPastParticiple.Text = "";
            LblVerbInPastSimpleCheck.IsVisible = false;
            LblVerbInPastParticipleCheck.IsVisible = false;
            //StackTraducciones.IsVisible = false;


            StackExamples.IsVisible = false;
        }
    }
}