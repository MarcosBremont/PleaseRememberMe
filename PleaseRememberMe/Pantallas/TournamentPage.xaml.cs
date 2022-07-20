using Acr.UserDialogs;
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
    public partial class TournamentPage : ContentPage
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
        public TournamentPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var lista = listverbos;
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
            listverbos = await metodos.GetListadoVerbos();
            GetRandomVerbForTheTournament();
            UserDialogs.Instance.HideLoading();

        }

        public async void GetRandomVerbForTheTournament()
        {
            try
            {
                if (listverbos.Count == 1 && App.YaPasoPorAqui == "Yes")
                {
                    UserDialogs.Instance.ShowLoading("I'm eating a cookie, give me a few seconds");
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡You finish!, your result will be send to the leaderboard");
                    var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                    App.SumaTotalDePuntos = 0;
                    LblNumerosDeVerbosRestantes.Text = "0";
                    await Navigation.PushModalAsync(new PrincipalPage());
                    UserDialogs.Instance.HideLoading();
                }
                else
                {


                    if (App.YaPasoPorAqui == "Yes")
                    {
                        var random = new System.Random().Next(1, listverbos.Count);
                        var datos = listverbos[random];
                        listverbos.Remove(datos);
                        VerbInSimplePast = datos.verboSimplePast.ToUpper();
                        VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
                        LblNumerosDeVerbosRestantes.Text = listverbos.Count.ToString();
                        LblNumerosDeVerbosRestantes.IsVisible = true;
                        txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
                        audioVerboFormaBase = datos.VerboFormaBase.ToString();

                    }
                    else
                    {
                        var random = new System.Random().Next(1, listverbos.Count);
                        var datos = listverbos[random];
                        listverbos.Remove(datos);
                        txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
                        VerbInSimplePast = datos.verboSimplePast.ToUpper();
                        VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
                        LblNumerosDeVerbosRestantes.Text = listverbos.Count.ToString();
                        LblNumerosDeVerbosRestantes.IsVisible = true;
                        App.YaPasoPorAqui = "Yes";
                        audioVerboFormaBase = datos.VerboFormaBase.ToString();


                    }

                }

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }

        }

        private async void BtnAtras_Clicked(object sender, EventArgs e)
        {
            BtnAtras.IsEnabled = false;
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
            await Navigation.PopModalAsync();
            UserDialogs.Instance.HideLoading();
            App.Torneo = "N";
        }

        private async void BtnTerminarTorneo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Information", "¿Do you want to finish the tournament?", "Yes", "No"))
                {
                    UserDialogs.Instance.ShowLoading("I'm reading a book, give me a few seconds");
                    await Navigation.PushModalAsync(new PrincipalPage());

                    BtnTerminarTorneo.IsVisible = false;
                    App.Torneo = "N";
                    var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                    App.SumaTotalDePuntos = 0;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡You finish!, your result will be send to the leaderboard");
                    await Navigation.PushModalAsync(new PrincipalPage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }


        }

        private async void BtnAnotherOne_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Looking for verbs...");

            if (App.Torneo == "S")
            {
                GetRandomVerbForTheTournament();
            }
            else
            {
                //await GetRandomVerb();

            }
            //LimpiarText();
            StackExamples.HorizontalOptions = LayoutOptions.CenterAndExpand;
            UserDialogs.Instance.HideLoading();

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
                            //BtnVerbInPastSAudio.IsVisible = true;
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
                            //BtnVerbInPastPartAudio.IsVisible = true;
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
                    LblPuntos.IsVisible = true;
                    App.SumaTotalDePuntos = App.SumaTotalDePuntos + 1;
                    LblPuntos.Text = App.SumaTotalDePuntos.ToString();
                    //BtnVerbInPastAudio.IsVisible = true;
                    //BtnVerbInPastPartAudio.IsVisible = true;
                    player.Load("CorrectSoundReady.mp3");
                }



                if (LblVerbInPastSimpleCheck.Text == "Incorrect" || LblVerbInPastParticipleCheck.Text == "Incorrect")
                {
                    player.Load("WrongSound.mp3");
                }

                if (App.Torneo == "S")
                {
                    GetRandomVerbForTheTournament();
                    LimpiarText();
                    //BtnVerbInPastAudio.IsVisible = true;
                    //BtnVerbInPastSAudio.IsVisible = false;
                    //BtnVerbInPastPartAudio.IsVisible = false;

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
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please check your internet connection");

            }




        }

        public void LimpiarText()
        {
            txtVerbInPastSimple.Text = "";
            txtVerbInPastParticiple.Text = "";
            LblVerbInPastSimpleCheck.IsVisible = false;
            LblVerbInPastParticipleCheck.IsVisible = false;
        }
    }
}