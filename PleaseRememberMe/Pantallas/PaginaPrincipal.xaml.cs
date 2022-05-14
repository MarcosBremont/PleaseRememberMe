using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Plugin.TextToSpeech;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.TextToSpeech.Abstractions;
using System.Globalization;

namespace PleaseRememberMe.Pantallas
{
    public partial class PaginaPrincipal : ContentPage
    {
        #region Variables String
        string VerbInSimplePast = "", VerbInPastParticiple = "", Traduccion = "", CorrectAnswer = "",
            CorrectAnswerPronoun = "", CorrectAnswerVerb = "", CorrectAnswerQuestionWithHow = "", CorrectAnswerPrepositionsOfTime = "",
            CorrectAnswerFamily = "", CorrectAnswerAnySome = "", CorrectAnswerVerbToBe1 = "",
            CorrectAnswerVerbToBe2 = "", CorrectAnswerQuantifiers = "", AnswerRadioButton1 = "",
            AnswerRadioButton2 = "", audioVerboFormaBase = "", audioverboSimplePast = "", audioverboPasParticiple = "", ExamplePastSimple = "", ExamplePastParticiple = "";
        #endregion
        #region Listas
        List<Entidad.EVerbos> listadodelosverbos = new List<Entidad.EVerbos>();
        List<Entidad.EVerbos> listverbos = new List<Entidad.EVerbos>();
        List<Entidad.EWasWereDid> listSentences = new List<Entidad.EWasWereDid>();
        List<Entidad.EQuestionsWithHow> listQuestionsWithHow = new List<Entidad.EQuestionsWithHow>();
        List<Entidad.EMatchSentences> listMatch = new List<Entidad.EMatchSentences>();
        List<Entidad.ECompleteSentences> listComplete = new List<Entidad.ECompleteSentences>();
        List<Entidad.EClothes> listclothes = new List<Entidad.EClothes>();
        List<Entidad.EPronouns> listpronouns = new List<Entidad.EPronouns>();
        List<Entidad.ESimplePresent> listsimplepresent = new List<Entidad.ESimplePresent>();
        List<Entidad.EPrepositionsOfTime> listprepositionsOfTime = new List<Entidad.EPrepositionsOfTime>();
        List<Entidad.EFamily> listFamily = new List<Entidad.EFamily>();
        List<Entidad.EAnySome> listAnySome = new List<Entidad.EAnySome>();
        List<Entidad.EVerbToBe> listVerbToBe = new List<Entidad.EVerbToBe>();
        List<Entidad.EQuantifiers> listQuantifiers = new List<Entidad.EQuantifiers>();
        #endregion
        #region Declaraciones
        ModalTournament modalTournament = new ModalTournament();
        ModalAboutMe modalAboutMe = new ModalAboutMe();
        Metodos metodos = new Metodos();
        IEnumerable<Locale> locales;
        static CrossLocale? localeee = null;
        #endregion
        private bool _userTapped;

        public PaginaPrincipal()
        {
            InitializeComponent();
            LblTorneoEnCurso.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblPuntos.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");

            StackTournament.GestureRecognizers.Add(
             new TapGestureRecognizer()
             {
                 Command = new Command(async () =>
                 {
                     if (_userTapped)
                         return;

                     _userTapped = true;
                     modalTournament = new ModalTournament();
                     modalTournament.OnLLamarOtraPantalla += ModalTournament_OnLLamarOtraPantalla;
                     modalTournament.Disappearing += ModalTournament_Disappearing;

                     await PopupNavigation.PushAsync(modalTournament);
                     await Task.Delay(1000);
                     _userTapped = false;
                     Opacity = 1;
                 }),
                 NumberOfTapsRequired = 1

             }
           );



            StackLayoutAboutMe.GestureRecognizers.Add(
             new TapGestureRecognizer()
             {
                 Command = new Command(async () =>
                 {
                     if (_userTapped)
                         return;

                     _userTapped = true;
                     modalAboutMe = new ModalAboutMe();
                     modalAboutMe.OnLLamarOtraPantalla += ModalAboutMe_OnLLamarOtraPantalla;
                     modalAboutMe.Disappearing += ModalAboutMe_Disappearing;

                     await PopupNavigation.PushAsync(modalAboutMe);
                     await Task.Delay(1000);
                     _userTapped = false;
                     Opacity = 1;
                 }),
                 NumberOfTapsRequired = 1

             }
           );

            CargarIdioma();

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
        private void ModalAboutMe_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
        }

        private void ModalAboutMe_Disappearing(object sender, EventArgs e)
        {
        }

        private void ModalTournament_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
        }

        private async void ModalTournament_Disappearing(object sender, EventArgs e)
        {
            if (App.Torneo == "S")
            {
                StackTorneo.IsVisible = true;
                App.SumaTotalDePuntos = 0;
                listverbos = await metodos.GetListadoVerbos();
                BtnLetsGo_Clicked(new object(), new EventArgs());
                BtnGiveMeSomeExamples.IsVisible = false;
                BtnAnotherOne.IsVisible = false;
                LblTorneo.IsVisible = true;
                LblTorneoEnCurso.IsVisible = true;
                BtnTerminarTorneo.IsVisible = true;
                txtVerbInPastSimple.Text = "";
                txtVerbInPastParticiple.Text = "";
                LblPuntos.Text = "0";
                LblPuntos.IsVisible = true;


            }
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                var lista = listverbos;
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking water");
                listverbos = await metodos.GetListadoVerbos();

                locales = await TextToSpeech.GetLocalesAsync();

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");
            }


        }

        public async void GetRandomVerbForTheTournament()
        {
            try
            {
                if (listverbos.Count == 1 && App.YaPasoPorAqui == "Yes")
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡You finish!, your result will be send to the leaderboard");
                    LblNumerosDeVerbosRestantes.Text = "0";
                    App.SumaTotalDePuntos = 0;
                    UserDialogs.Instance.ShowLoading("I'm eating a cookie, give me a few seconds");
                    var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                    UserDialogs.Instance.HideLoading();
                }
                else
                {


                    if (App.YaPasoPorAqui == "Yes")
                    {
                        var random = new Random().Next(1, listverbos.Count);
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
                        var random = new Random().Next(1, listverbos.Count);
                        var datos = listverbos[random];
                        //listverbos.Remove(datos);
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
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

        }

        public async void GetRandomVerb()
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
                        StackTorneo.IsVisible = true;
                        BtnVerbInPastAudio.IsVisible = true;
                        BtnVerbInPastSAudio.IsVisible = false;
                        BtnVerbInPastPartAudio.IsVisible = false;

                    }
                }

                var random = new Random().Next(1, listverbos.Count);
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
                LblNumerosDeVerbosRestantes.Text = listverbos.Count().ToString();

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

        private void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            //DependencyService.Get<IAppSettingsHelper>().OpenAppSettings();
            StackTorneo.IsVisible = false;
            Anuncio.IsVisible = true;

            BtnGiveMeSomeExamples.IsVisible = true;
            BtnAnotherOne.IsVisible = true;
            BtnVerbInPastAudio.IsVisible = true;
            BtnTerminarTorneo.IsVisible = false;
            BtnVerbInPastSAudio.IsVisible = false;
            BtnVerbInPastPartAudio.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            StacklayoutPrincipal.IsVisible = false;
            StackExamples.IsVisible = false;
            //StackTraducciones.IsVisible = false;
            StacklayoutLetsGo.IsVisible = true;
            LblVerbInPastSimpleCheck.IsVisible = false;
            LblVerbInPastParticipleCheck.IsVisible = false;
            txtVerbInPastSimple.Text = "";
            txtVerbInPastParticiple.Text = "";
            if (App.Torneo == "S")
            {
                StackTorneo.IsVisible = true;
            }

            GetRandomVerb();
        }

        private void BtnAnotherOne_Clicked(object sender, EventArgs e)
        {
            BtnVerbInPastSAudio.IsVisible = false;
            BtnVerbInPastPartAudio.IsVisible = false;
            if (App.Torneo == "S")
            {
                GetRandomVerbForTheTournament();
            }
            else
            {
                GetRandomVerb();

            }
            LimpiarText();
            StackExamples.HorizontalOptions = LayoutOptions.CenterAndExpand;
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
                    LblPuntos.IsVisible = true;
                    App.SumaTotalDePuntos = App.SumaTotalDePuntos + 1;
                    LblPuntos.Text = App.SumaTotalDePuntos.ToString();
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
                    GetRandomVerbForTheTournament();
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

        private void BtnAtras_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;

            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            StacklayoutLetsGo.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            LblPuntos.Text = "0";
            App.Torneo = "N";
        }

        private async void BtnTablaDePosiciones_Clicked(object sender, EventArgs e)
        {
            try
            {
                Anuncio.IsVisible = false;

                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
                StacklayoutPrincipal.IsVisible = false;
                StackLayoutTablaPosiciones.IsVisible = true;
                GridVolverAtrasPosiciones.IsVisible = true;

                var datos = await metodos.GetListadoDePosiciones();
                lsv_TablaPuntuacion.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }


        }

        private void BtnAtrasPosiciones_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;

            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            GridVolverAtrasPosiciones.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;

            GridVerbos.IsVisible = false;
            BtnLetsGo.IsVisible = true;



            LblPuntos.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;

            StackLayoutVerbList.IsVisible = false;



        }

        private async void BtnTournament_Clicked(object sender, EventArgs e)
        {
            if (_userTapped)
                return;

            _userTapped = true;
            modalTournament = new ModalTournament();
            modalTournament.OnLLamarOtraPantalla += ModalTournament_OnLLamarOtraPantalla;
            modalTournament.Disappearing += ModalTournament_Disappearing;

            await PopupNavigation.PushAsync(modalTournament);
            await Task.Delay(1000);
            _userTapped = false;
            Opacity = 1;
        }

        private async void BtnTerminarTorneo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Information", "¿Do you want to finish the tournament?", "Yes", "No"))
                {
                    UserDialogs.Instance.ShowLoading("I'm reading a book, give me a few seconds");
                    BtnAtras_Clicked(new object(), new EventArgs());
                    BtnTerminarTorneo.IsVisible = false;
                    App.Torneo = "N";
                    var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                    App.SumaTotalDePuntos = 0;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡You finish!, your result will be send to the leaderboard");
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }


        }

        public async void BtnListOfTheVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Anuncio.IsVisible = false;

                GridVolverAtrasVerbList.IsVisible = true;
                StackLayoutVerbList.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                StacklayoutPrincipal.IsVisible = false;
                var datos = await metodos.GetListadoVerbos();
                lsv_ListaDeVerbos.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }



        }

        public void BtnAtrasVerbList_Clicked(System.Object sender, System.EventArgs e)
        {
            Anuncio.IsVisible = true;

            GridVolverAtrasVerbList.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            BtnLetsGo.IsVisible = true;


            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");



        }

        public void btnAjustes_Clicked(System.Object sender, System.EventArgs e)
        {
            Anuncio.IsVisible = false;

            GridVolverAtrasSettings.IsVisible = true;
            StackLayoutSettings.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            StacklayoutPrincipal.IsVisible = false;

        }

        void BtnAtrasSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            Anuncio.IsVisible = true;

            GridVolverAtrasSettings.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutSettings.IsVisible = false;
            BtnLetsGo.IsVisible = true;


            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");


        }

        async void BtnSaveChanges_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Saving Email, give me a few seconds");
                var apiResult = await metodos.SendEmails(txtEmail.Text);
                if (apiResult.Respuesta == "OK")
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Email Saved, you are going to receive all the news in your email");
                    txtEmail.Text = "";
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Error, check your internet connection");

                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }



        }
        private void BtnAtrasOtherTopics_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = false;
            Anuncio.IsVisible = true;


            GridVolverAtrasVerbList.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            BtnLetsGo.IsVisible = true;


            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");


        }

        private async void btnWasWereDid_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutsimplePastCategory.IsVisible = false;
                StackLayoutWasWereDid.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Decide, black coffee or coffee with milk");
                var datos = await metodos.GetWasWereSentences();
                listSentences = datos;
                var random = new Random().Next(1, listSentences.Count);
                var elegido = listSentences[random];
                lblWasWereDid.Text = listSentences[0].WasWereSentence;
                CorrectAnswer = listSentences[0].correctanswer;
                //listSentences.Remove(elegido);
                TxtxCorrectAnswer.Text = "";
                LblCorrectAnswer.IsVisible = false;

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

        }

        private void BtnAtrasWasWereDid_Clicked(object sender, EventArgs e)
        {
            StackLayoutWasWereDid.IsVisible = false;

            GridVolverAtrasVerbList.IsVisible = false;
            StackLayoutsimplePastCategory.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswer_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtxCorrectAnswer.Text))
            {
                LblCorrectAnswer.IsVisible = true;
                LblCorrectAnswer.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswer == TxtxCorrectAnswer.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswer.IsVisible = true;
                    LblCorrectAnswer.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswer.IsVisible = true;
                    LblCorrectAnswer.Text = "Incorrect";

                }
            }



        }

        private void BtnOneMore_Clicked(object sender, EventArgs e)
        {
            if (listSentences.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listSentences.Count);
                var elegido = listSentences[random];
                lblWasWereDid.Text = elegido.WasWereSentence;
                CorrectAnswer = elegido.correctanswer;
                listSentences.Remove(elegido);
                TxtxCorrectAnswer.Text = "";
                LblCorrectAnswer.IsVisible = false;
            }
        }

        private async void BtnMatch_Clicked(object sender, EventArgs e)
        {
            StackLayoutVocabularyCategory.IsVisible = false;
            StackLayoutProfessionsPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasMatch_Clicked(object sender, EventArgs e)
        {
            StackLayoutMatch.IsVisible = false;
            StackLayoutProfessionsActivities.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void Btncorrects_Clicked(object sender, EventArgs e)
        {

            foreach (var item in listMatch)
            {
                if (string.IsNullOrEmpty(item.textbox))
                {
                    item.Imagenes = "remove.png";
                }
                else
                {
                    if (item.textbox.ToUpper().Trim().Replace(".", "") == item.CorrectAnswer)
                    {
                        item.Imagenes = "correct.png";
                    }
                    else
                    {
                        item.Imagenes = "remove.png";
                    }
                }

            }
            lsv_Math.ItemsSource = null;
            lsv_Math.ItemsSource = listMatch;

        }

        private async void btnComplete_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutComplete.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, Do you want some cookies?");
                var datos = await metodos.GetCompleteSentences();
                var response = await metodos.GetAdjectives();
                lsv_complete.ItemsSource = datos;
                CollectionViewAdjective.ItemsSource = response;
                listComplete = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

        }

        private void BtnAtrasComplete_Clicked(object sender, EventArgs e)
        {
            StackLayoutComplete.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void Btncorrectadjective_Clicked(object sender, EventArgs e)
        {
            foreach (var item in listComplete)
            {
                if (string.IsNullOrEmpty(item.txtcomplete))
                {
                    item.imagen = "remove.png";
                }
                else
                {
                    if (item.txtcomplete.ToUpper() == item.CorrectAnswer)
                    {
                        item.imagen = "correct.png";
                    }
                    else
                    {
                        item.imagen = "remove.png";
                    }
                }

            }
            lsv_complete.ItemsSource = null;
            lsv_complete.ItemsSource = listComplete;
        }

        async void btnSClothes_Clicked(System.Object sender, System.EventArgs e)
        {
            StackLayoutVocabularyCategory.IsVisible = false;
            StackLayoutClothesPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        void BtnAtrasClothes_Clicked(System.Object sender, System.EventArgs e)
        {
            StackLayoutClothes.IsVisible = false;
            StackLayoutClothesActivities.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        void Btncorrectclothes_Clicked(System.Object sender, System.EventArgs e)
        {
            foreach (var item in listclothes)
            {
                if (string.IsNullOrEmpty(item.txtclothes))
                {
                    item.imagen = "remove.png";
                }
                else
                {
                    if (item.txtclothes.ToUpper().Trim().Replace(".", "") == item.CorrectAnswer)
                    {
                        item.imagen = "correct.png";
                    }
                    else
                    {
                        item.imagen = "remove.png";
                    }
                }

            }
            lsv_clothes.ItemsSource = null;
            lsv_clothes.ItemsSource = listclothes;
        }

        private async void btnPronouns_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutPronouns.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Did you drink water today?");
                var datos = await metodos.GetPronouns();
                listpronouns = datos;
                var random = new Random().Next(1, listpronouns.Count);
                var elegido = listpronouns[random];
                lblPronouns.Text = listpronouns[0].PronounsSentences;
                CorrectAnswerPronoun = listpronouns[0].CorrectAnswer;
                TxtCorrectAnswerPronouns.Text = "";
                LblCorrectAnswerPronouns.IsVisible = false;
                //listpronouns.Remove(elegido);
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasPronouns_Clicked(object sender, EventArgs e)
        {
            StackLayoutPronouns.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerPronouns_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCorrectAnswerPronouns.Text))
            {
                LblCorrectAnswerPronouns.IsVisible = true;
                LblCorrectAnswerPronouns.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerPronoun == TxtCorrectAnswerPronouns.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerPronouns.IsVisible = true;
                    LblCorrectAnswerPronouns.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerPronouns.IsVisible = true;
                    LblCorrectAnswerPronouns.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMorePronoun_Clicked(object sender, EventArgs e)
        {
            if (listpronouns.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listpronouns.Count);
                var elegido = listpronouns[random];
                lblPronouns.Text = elegido.PronounsSentences;
                CorrectAnswerPronoun = elegido.CorrectAnswer;
                listpronouns.Remove(elegido);
                TxtCorrectAnswerPronouns.Text = "";
                LblCorrectAnswerPronouns.IsVisible = false;
            }
        }

        private async void btnSimplePresent_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutsimplePresentCategory.IsVisible = false;
                StackLayoutSimplePresent.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Remember to drink water");
                var datos = await metodos.GetSimplePresent();
                listsimplepresent = datos;
                var random = new Random().Next(1, listsimplepresent.Count);
                var elegido = listsimplepresent[random];
                lblSimplePresent.Text = listsimplepresent[0].SimplePresentExercises;
                CorrectAnswerVerb = listsimplepresent[0].CorrectAnswer;
                //listsimplepresent.Remove(elegido);
                TxtVerbInCorrecForm.Text = "";
                LblCorrectAnswerVerb.IsVisible = false;

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasSimplePresent_Clicked(object sender, EventArgs e)
        {
            StackLayoutSimplePresent.IsVisible = false;
            StackLayoutWasWereDid.IsVisible = false;
            StackLayoutComplete.IsVisible = false;
            GridVolverAtrasVerbList.IsVisible = false;
            StackLayoutsimplePresentCategory.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerSimplePresent_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtVerbInCorrecForm.Text))
            {
                LblCorrectAnswerVerb.IsVisible = true;
                LblCorrectAnswerVerb.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerVerb == TxtVerbInCorrecForm.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerVerb.IsVisible = true;
                    LblCorrectAnswerVerb.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerVerb.IsVisible = true;
                    LblCorrectAnswerVerb.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMoreSimplePresent_Clicked(object sender, EventArgs e)
        {
            if (listsimplepresent.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listsimplepresent.Count);
                var elegido = listsimplepresent[random];
                lblSimplePresent.Text = elegido.SimplePresentExercises;
                CorrectAnswerVerb = elegido.CorrectAnswer;
                listsimplepresent.Remove(elegido);
                TxtVerbInCorrecForm.Text = "";
                LblCorrectAnswerVerb.IsVisible = false;
            }
        }

        private async void btnPrepositionsoftime_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutPreposisionsOfTime.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                var datos = await metodos.GetPrepositionsOfTimeSentences();
                listprepositionsOfTime = datos;
                var random = new Random().Next(1, listprepositionsOfTime.Count);
                var elegido = listprepositionsOfTime[random];
                lblPrepositionsOfTime.Text = listprepositionsOfTime[0].PrepositionsOfTimeSentence;
                CorrectAnswerPrepositionsOfTime = listprepositionsOfTime[0].CorrectAnswer;
                //listprepositionsOfTime.Remove(elegido);
                TxtPrepositionsOfTime.Text = "";
                LblCorrectAnswerPrepositionsOfTime.IsVisible = false;

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnCheckMyAnswerPrepositionsOfTime_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtPrepositionsOfTime.Text))
            {
                LblCorrectAnswerPrepositionsOfTime.IsVisible = true;
                LblCorrectAnswerPrepositionsOfTime.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerPrepositionsOfTime == TxtPrepositionsOfTime.Text.ToUpper())
                {
                    LblCorrectAnswerPrepositionsOfTime.IsVisible = true;
                    LblCorrectAnswerPrepositionsOfTime.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerPrepositionsOfTime.IsVisible = true;
                    LblCorrectAnswerPrepositionsOfTime.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMorePrepositionsOfTime_Clicked(object sender, EventArgs e)
        {
            if (listprepositionsOfTime.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listprepositionsOfTime.Count);
                var elegido = listprepositionsOfTime[random];
                lblPrepositionsOfTime.Text = elegido.PrepositionsOfTimeSentence;
                CorrectAnswerPrepositionsOfTime = elegido.CorrectAnswer;
                listprepositionsOfTime.Remove(elegido);
                TxtVerbInCorrecForm.Text = "";
                LblCorrectAnswerPrepositionsOfTime.IsVisible = false;
            }
        }

        private void BtnAtrasPreposisionsOfTime_Clicked(object sender, EventArgs e)
        {
            StackLayoutGramarCategory.IsVisible = true;
            StackLayoutPreposisionsOfTime.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }
        private async void btnFamilyVocabulary_Clicked(object sender, EventArgs e)
        {
            StackLayoutVocabularyCategory.IsVisible = false;
            StackLayoutFamilyPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerFamily_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFamilyMember.Text))
            {
                LblCorrectAnswerFamily.IsVisible = true;
                LblCorrectAnswerFamily.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerFamily == TxtFamilyMember.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerFamily.IsVisible = true;
                    LblCorrectAnswerFamily.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerFamily.IsVisible = true;
                    LblCorrectAnswerFamily.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMoreFamily_Clicked(object sender, EventArgs e)
        {
            if (listFamily.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listFamily.Count);
                var elegido = listFamily[random];
                LblFamilySentences.Text = elegido.FamilySentences;
                CorrectAnswerFamily = elegido.CorrectAnswer;
                listFamily.Remove(elegido);
                TxtFamilyMember.Text = "";
                LblCorrectAnswerFamily.IsVisible = false;
            }
        }

        private void BtnAtrasFamily_Clicked(object sender, EventArgs e)
        {
            StackLayoutFamilyActivities.IsVisible = true;
            StackLayoutFamily.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void btnAnySome_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutAnySome.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating pizza");
                var datos = await metodos.GetAnySome();
                listAnySome = datos;
                var random = new Random().Next(1, listAnySome.Count);
                var elegido = listAnySome[random];
                LblAnySome.Text = listAnySome[0].AnySomeSentences;
                CorrectAnswerAnySome = listAnySome[0].CorrectAnswer;
                //listAnySome.Remove(elegido);
                TxtAnySome.Text = "";
                LblCorrectAnswerAnySome.IsVisible = false;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasAnySome_Clicked(object sender, EventArgs e)
        {
            StackLayoutGramarCategory.IsVisible = true;
            StackLayoutAnySome.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerAnySome_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtAnySome.Text))
            {
                LblCorrectAnswerAnySome.IsVisible = true;
                LblCorrectAnswerAnySome.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerAnySome == TxtAnySome.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerAnySome.IsVisible = true;
                    LblCorrectAnswerAnySome.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerAnySome.IsVisible = true;
                    LblCorrectAnswerAnySome.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMoreAnySome_Clicked(object sender, EventArgs e)
        {
            if (listAnySome.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listAnySome.Count);
                var elegido = listAnySome[random];
                LblAnySome.Text = elegido.AnySomeSentences;
                CorrectAnswerAnySome = elegido.CorrectAnswer;
                listAnySome.Remove(elegido);
                TxtAnySome.Text = "";
                LblCorrectAnswerAnySome.IsVisible = false;
            }
        }

        private void BtnSimplePresentCategory_Clicked(object sender, EventArgs e)
        {
            StackLayoutGramarCategory.IsVisible = false;
            StackLayoutsimplePresentCategory.IsVisible = true;

        }

        private void BtnGrammarCategory_Clicked(object sender, EventArgs e)
        {
            StackLayoutCategory.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
        }

        private void BtnVocabularyCategory_Clicked(object sender, EventArgs e)
        {
            StackLayoutCategory.IsVisible = false;
            StackLayoutVocabularyCategory.IsVisible = true;
        }

        private void BtnAtrasSimplePastCategory_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            StackLayoutsimplePastCategory.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
        }

        private void BtnAtrasCategory_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;

            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            StackLayoutCategory.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
        }

        private void BtnVolverAtrasGrammarCategory_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            StackLayoutGramarCategory.IsVisible = false;
            StackLayoutCategory.IsVisible = true;
        }

        private async void btnVerbToBe_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutsimplePresentCategory.IsVisible = false;
                StackLayoutVerbToBe.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a burrito");
                var datos = await metodos.GetVerbToBe();
                listVerbToBe = datos;
                var random = new Random().Next(1, listVerbToBe.Count);
                var elegido = listVerbToBe[random];
                LblVerbToBeExercise.Text = listVerbToBe[0].verbtobeSentence;
                CorrectAnswerVerbToBe1 = listVerbToBe[0].CorrectAnswer1;
                CorrectAnswerVerbToBe2 = listVerbToBe[0].CorrectAnswer2;
                //listVerbToBe.Remove(elegido);
                TxtVerbToBe.Text = "";
                TxtVerbToBe2.Text = "";
                LblCorrectAnswerVerbToBe.IsVisible = false;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasSimplePresentCategory_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

            StackLayoutsimplePresentCategory.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
        }

        private void BtnAtrasVocabulary_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

            StackLayoutCategory.IsVisible = true;
            StackLayoutVocabularyCategory.IsVisible = false;
        }

        private void BtnCategories_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = true;
            Anuncio.IsVisible = true;
            StackLayoutCategory.IsVisible = true;
            //Lsv_Categories.ItemsSource = await metodos.GetCategories();
            StackLayoutVerbList.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait, Do you want coffee?");
            StacklayoutPrincipal.IsVisible = false;
            UserDialogs.Instance.HideLoading();
        }


        private async void BtnVerbInPastS_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioverboSimplePast, crossLocale: localeee);
        }

        private async void BtnVerbInPastPart_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioverboPasParticiple, crossLocale: localeee);

        }

        private void BtnActivitiesProfessions_Clicked(object sender, EventArgs e)
        {
            StackLayoutProfessionsPage.IsVisible = false;
            StackLayoutProfessionsActivities.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnProfessionsMatch_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutProfessionsActivities.IsVisible = false;
                StackLayoutMatch.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm looking the blue sky");
                var datos = await metodos.GetMatchSentences();
                listMatch = datos;
                lsv_Math.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private async void BtnVocabularyProfessions_Clicked(object sender, EventArgs e)
        {
            StackLayoutProfessionsPage.IsVisible = false;
            StacklayoutVocabularyWords.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

            try
            {
                Anuncio.IsVisible = false;

                StackLayoutVocabularyCategory.IsVisible = false;
                StacklayoutVocabularyWords.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                var datos = await metodos.GetVocabulary();
                lsv_VocabularyWords.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasProfessionsActivities_Clicked(object sender, EventArgs e)
        {
            StackLayoutProfessionsActivities.IsVisible = false;
            StackLayoutProfessionsPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasProfessionsPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutProfessionsPage.IsVisible = false;
            StackLayoutVocabularyCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasClothesPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutVocabularyCategory.IsVisible = true;
            StackLayoutClothesPage.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnActivitiesClothes_Clicked(object sender, EventArgs e)
        {
            StackLayoutClothesPage.IsVisible = false;
            StackLayoutClothesActivities.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnVocabularyClothes_Clicked(object sender, EventArgs e)
        {
            try
            {
                Anuncio.IsVisible = false;

                StackLayoutClothesPage.IsVisible = false;
                StacklayoutVocabularyClothes.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                var datos = await metodos.GetVocabularyClothes();
                lsv_VocabularyClothesWords.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasClothessActivities_Clicked(object sender, EventArgs e)
        {
            StackLayoutClothesActivities.IsVisible = false;
            StackLayoutClothesPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnClothesExercises_Clicked(object sender, EventArgs e)
        {
            StackLayoutClothesActivities.IsVisible = false;
            StackLayoutClothes.IsVisible = true;

            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Are you happy? I hope so");
            var datos = await metodos.GetClothes();
            listclothes = datos;
            lsv_clothes.ItemsSource = datos;
            UserDialogs.Instance.HideLoading();
        }

        private void BtnAtrasVocabularyClothesWords_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;
            StacklayoutVocabularyClothes.IsVisible = false;
            StackLayoutClothesPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasFamilyPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutVocabularyCategory.IsVisible = true;
            StackLayoutFamilyPage.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnVocabularyFamily_Clicked(object sender, EventArgs e)
        {
            try
            {
                Anuncio.IsVisible = false;

                StackLayoutFamilyPage.IsVisible = false;
                StacklayoutVocabularyFamily.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                var datos = await metodos.GetVocabularyFamily();
                lsv_VocabularyFamilyWords.ItemsSource = datos;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnActivitiesFamily_Clicked(object sender, EventArgs e)
        {
            StackLayoutFamilyPage.IsVisible = false;
            StackLayoutFamilyActivities.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasVocabularyFamilyWords_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;
            StacklayoutVocabularyFamily.IsVisible = false;
            StackLayoutFamilyPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnFamilyExercises_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutFamilyActivities.IsVisible = false;
                StackLayoutFamily.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Remember, study for your quiz");
                var datos = await metodos.GetFamily();
                listFamily = datos;
                var random = new Random().Next(1, listFamily.Count);
                var elegido = listFamily[random];
                LblFamilySentences.Text = listFamily[0].FamilySentences;
                CorrectAnswerFamily = listFamily[0].CorrectAnswer;
                //listFamily.Remove(elegido);
                TxtFamilyMember.Text = "";
                LblCorrectAnswerFamily.IsVisible = false;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasFamilysActivities_Clicked(object sender, EventArgs e)
        {
            StackLayoutFamilyActivities.IsVisible = false;
            StackLayoutFamilyPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }


        private void BtnOrganizeConversation_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnAtrasVocabularyWords_Clicked(object sender, EventArgs e)
        {
            Anuncio.IsVisible = true;
            StacklayoutVocabularyWords.IsVisible = false;
            StackLayoutProfessionsPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnVerbInPast_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(audioVerboFormaBase, crossLocale: localeee);
        }

        private void BtnCheckMyAnswerVerbToBe_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtVerbToBe.Text) || string.IsNullOrEmpty(TxtVerbToBe2.Text))
            {
                LblCorrectAnswerVerbToBe.IsVisible = true;
                LblCorrectAnswerVerbToBe.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerVerbToBe1 == TxtVerbToBe.Text.ToUpper().Trim().Replace(".", "") && CorrectAnswerVerbToBe2 == TxtVerbToBe2.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerVerbToBe.IsVisible = true;
                    LblCorrectAnswerVerbToBe.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerVerbToBe.IsVisible = true;
                    LblCorrectAnswerVerbToBe.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMoreVerbToBe_Clicked(object sender, EventArgs e)
        {
            if (listVerbToBe.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listVerbToBe.Count);
                var elegido = listVerbToBe[random];
                LblVerbToBeExercise.Text = elegido.verbtobeSentence;
                CorrectAnswerVerbToBe1 = elegido.CorrectAnswer1;
                CorrectAnswerVerbToBe2 = elegido.CorrectAnswer2;
                listVerbToBe.Remove(elegido);
                TxtVerbToBe.Text = "";
                TxtVerbToBe2.Text = "";
                LblCorrectAnswerVerbToBe.IsVisible = false;
            }
        }

        private void BtnAtrasVerbToBe_Clicked(object sender, EventArgs e)
        {
            StackLayoutVerbToBe.IsVisible = false;
            StackLayoutsimplePresentCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerChoose_Clicked(object sender, EventArgs e)
        {

            if (AnswerRadioButton1 == CorrectAnswerQuantifiers || AnswerRadioButton2 == CorrectAnswerQuantifiers)
            {
                LblCorrectAnswerQuantifiers.IsVisible = true;
                LblCorrectAnswerQuantifiers.Text = "Correct";
            }
            else
            {
                LblCorrectAnswerQuantifiers.IsVisible = true;
                LblCorrectAnswerQuantifiers.Text = "Incorrect";

            }

        }

        private void BtnOneMoreChoose_Clicked(object sender, EventArgs e)
        {
            if (listQuantifiers.Count == 1)
            {
                UserDialogs.Instance.Toast("There aren’t any more sentences");
            }
            else
            {
                var random = new Random().Next(1, listQuantifiers.Count);
                var elegido = listQuantifiers[random];
                LblQuantifiers.Text = elegido.QuantifiersSentence;
                LblFirstOption.Text = elegido.FirstOption;
                LblSecondOption.Text = elegido.SecondOption;
                CorrectAnswerQuantifiers = elegido.CorrectAnswer;
                listQuantifiers.Remove(elegido);
                RadioButtonOption1.IsChecked = false;
                RadioButtonOption2.IsChecked = false;
                LblCorrectAnswerQuantifiers.IsVisible = false;
            }
        }

        private async void btnQuantifiers_Clicked(object sender, EventArgs e)
        {
            try
            {
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutQuantifiers.IsVisible = true;
                LblCorrectAnswerQuantifiers.IsVisible = false;
                RadioButtonOption1.IsChecked = false;
                RadioButtonOption2.IsChecked = false;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a tea");
                var datos = await metodos.GetQuantifiers();
                listQuantifiers = datos;
                var random = new Random().Next(1, listQuantifiers.Count);
                var elegido = listQuantifiers[random];
                LblQuantifiers.Text = listQuantifiers[0].QuantifiersSentence;
                LblFirstOption.Text = listQuantifiers[0].FirstOption;
                LblSecondOption.Text = listQuantifiers[0].SecondOption;
                CorrectAnswerQuantifiers = listQuantifiers[0].CorrectAnswer;
                listQuantifiers.Remove(elegido);
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasQuantifiers_Clicked(object sender, EventArgs e)
        {
            StackLayoutQuantifiers.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
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

        private void BtnAtrasQuestionsWithHow_Clicked(object sender, EventArgs e)
        {
            StackLayoutQuestionsWithHow.IsVisible = false;
            StackLayoutGramarCategory.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnCheckMyAnswerQuestionWithHow_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtxCorrectAnswerQuestionWithHow.Text))
            {
                LblCorrectAnswerQuestionWithHow.IsVisible = true;
                LblCorrectAnswerQuestionWithHow.Text = "Incorrect";
            }
            else
            {
                if (CorrectAnswerQuestionWithHow == TxtxCorrectAnswerQuestionWithHow.Text.ToUpper().Trim().Replace(".", ""))
                {
                    LblCorrectAnswerQuestionWithHow.IsVisible = true;
                    LblCorrectAnswerQuestionWithHow.Text = "Correct";
                }
                else
                {
                    LblCorrectAnswerQuestionWithHow.IsVisible = true;
                    LblCorrectAnswerQuestionWithHow.Text = "Incorrect";

                }
            }
        }

        private void BtnOneMoreQuestionWithHow_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (listQuestionsWithHow.Count == 0)
                {
                    UserDialogs.Instance.Toast("There aren’t any more sentences");
                }
                else
                {
                    var random = new Random().Next(1, listQuestionsWithHow.Count);
                    var elegido = listQuestionsWithHow[random];
                    lblQuestionWithHow.Text = elegido.Questionswithhow_sentences;
                    CorrectAnswerQuestionWithHow = elegido.CorrectAnswer;
                    listQuestionsWithHow.Remove(elegido);
                    TxtxCorrectAnswerQuestionWithHow.Text = "";
                    LblCorrectAnswerQuestionWithHow.IsVisible = false;
                }
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
                StackLayoutGramarCategory.IsVisible = false;
                StackLayoutQuestionsWithHow.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Hey, do you like tomatoes?");
                var datos = await metodos.GetQuestionsWithHow();
                listQuestionsWithHow = datos;
                var random = new Random().Next(1, listQuestionsWithHow.Count);
                var elegido = listQuestionsWithHow[random];
                lblQuestionWithHow.Text = listQuestionsWithHow[0].Questionswithhow_sentences;
                CorrectAnswerQuestionWithHow = listQuestionsWithHow[0].CorrectAnswer;
                //listQuestionsWithHow.Remove(elegido);
                TxtxCorrectAnswerQuestionWithHow.Text = "";
                LblCorrectAnswerQuestionWithHow.IsVisible = false;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        void BtnSimplePast_Clicked(System.Object sender, System.EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

            StackLayoutGramarCategory.IsVisible = false;
            StackLayoutsimplePastCategory.IsVisible = true;
        }


        private async void BtnReport_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Sending report, give me a few seconds");
                var apiResult = await metodos.SendReport(txtReport.Text);
                if (apiResult.Respuesta == "OK")
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Report Sent, thanks for report a problem");
                    txtReport.Text = "";
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Error, check your internet connection");

                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }
    }
}
