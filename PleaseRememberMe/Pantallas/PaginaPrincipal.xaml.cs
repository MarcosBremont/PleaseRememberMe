using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PleaseRememberMe.Pantallas
{
    public partial class PaginaPrincipal : ContentPage
    {
        string VerbInSimplePast = "";
        string VerbInPastParticiple = "";
        string Traduccion = "";
        string CorrectAnswer = "";
        private bool _userTapped;
        ModalTournament modalTournament = new ModalTournament();
        ModalAboutMe modalAboutMe = new ModalAboutMe();
        List<Entidad.EVerbos> listadodelosverbos = new List<Entidad.EVerbos>();
        List<Entidad.EVerbos> listverbos = new List<Entidad.EVerbos>();
        List<Entidad.EWasWereDid> listSentences = new List<Entidad.EWasWereDid>();
        List<Entidad.EMatchSentences> listMatch = new List<Entidad.EMatchSentences>();

        Metodos metodos = new Metodos();
        public PaginaPrincipal()
        {
            InitializeComponent();
            GridVolverAtras.IsVisible = false;
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
            var lista = listverbos;
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            listverbos = await metodos.GetListadoVerbos();
            UserDialogs.Instance.HideLoading();

        }

        public async void GetRandomVerbForTheTournament()
        {
            if (listverbos.Count == 1 && App.YaPasoPorAqui == "Yes")
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("¡You finish!, your result will be send to the leaderboard");
                LblNumerosDeVerbosRestantes.Text = "0";
                //App.SumaTotalDePuntos = 0;
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
                }
                else
                {
                    var random = new Random().Next(1, listverbos.Count);
                    var datos = listverbos[random];
                    txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
                    VerbInSimplePast = datos.verboSimplePast.ToUpper();
                    VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
                    LblNumerosDeVerbosRestantes.Text = listverbos.Count.ToString();
                    LblNumerosDeVerbosRestantes.IsVisible = true;
                    App.YaPasoPorAqui = "Yes";

                }

            }

        }

        public async void GetRandomVerb()
        {
            var lista = listverbos;

            if (App.YaPasastePorGetRandomVerb == "Yes")
            {

            }
            else
            {
                if (App.Torneo == "N")
                {
                    listverbos = await metodos.GetListadoVerbos();
                    App.YaPasastePorGetRandomVerb = "Yes";

                }
                else
                {

                }
            }

            var random = new Random().Next(1, listverbos.Count);
            var datos = listverbos[random];
            txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
            VerbInSimplePast = datos.verboSimplePast.ToUpper();
            VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
            Traduccion = datos.traduccion.ToUpper();
            lblExamplePast.Text = datos.examplesInBaseForm;
            lblExamplePastSimple.Text = datos.examplesInSimplePast;
            lblExamplePastParticiple.Text = datos.examplesInPastParticiple;


        }

        private void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            BtnTerminarTorneo.IsVisible = false;
            StackPleaseRememberTextAndImages.IsVisible = false;
            BtnListOfTheVerbs.IsVisible = false;
            LblVerbInPastSimpleCheck.IsVisible = false;
            LblVerbInPastParticipleCheck.IsVisible = false;
            txtVerbInPastSimple.Text = "";
            txtVerbInPastParticiple.Text = "";
            btnAjustes.IsVisible = false;
            GetRandomVerb();
            AparecerLabelYTextbox();


        }


        public void AparecerLabelYTextbox()
        {
            LblVerbInPast.IsVisible = true;
            LblVerbInPastSimple.IsVisible = true;
            LblVerbInPastParticiple.IsVisible = true;
            txtVerbInPast.IsVisible = true;
            txtVerbInPastSimple.IsVisible = true;
            txtVerbInPastParticiple.IsVisible = true;
            BtnLetsGo.IsVisible = false;
            BtnOtherTopics.IsVisible = false;
            BtnAnotherOne.IsVisible = true;
            BtnCheck.IsVisible = true;
            BtnGiveMeSomeExamples.IsVisible = true;
            lblVersion.IsVisible = true;
            GridVolverAtras.IsVisible = true;
            GridVerbos.IsVisible = true;
            BtnTablaDePosiciones.IsVisible = false;
            StackTournament.IsVisible = false;


        }

        private void BtnAnotherOne_Clicked(object sender, EventArgs e)
        {
            if (App.Torneo == "S")
            {
                GetRandomVerbForTheTournament();
            }
            else
            {
                GetRandomVerb();

            }
            LimpiarText();
        }

        public void LimpiarText()
        {
            txtVerbInPastSimple.Text = "";
            txtVerbInPastParticiple.Text = "";
            LblVerbInPastSimpleCheck.IsVisible = false;
            LblVerbInPastParticipleCheck.IsVisible = false;
            LblTrad.IsVisible = false;
            LblTraduction.IsVisible = false;

            lblexample1.IsVisible = false;
            lblexample2.IsVisible = false;
            lblexample3.IsVisible = false;
            lblExamplePast.IsVisible = false;
            lblExamplePastSimple.IsVisible = false;
            lblExamplePastParticiple.IsVisible = false;
        }

        private void BtnCheck_Clicked(object sender, EventArgs e)
        {
            if (App.Torneo != "S")
            {
                LblTrad.IsVisible = true;
                LblTraduction.IsVisible = true;
                LblTraduction.Text = Traduccion;
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

            }

            player.Play();

        }

        private void BtnGiveMeSomeExamples_Clicked(object sender, EventArgs e)
        {
            lblexample1.IsVisible = true;
            lblexample2.IsVisible = true;
            lblexample3.IsVisible = true;
            lblExamplePast.IsVisible = true;
            lblExamplePastSimple.IsVisible = true;
            lblExamplePastParticiple.IsVisible = true;
        }

        private async void BtnAtras_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            GridVerbos.IsVisible = false;
            GridVolverAtras.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnOtherTopics.IsVisible = true;

            BtnCheck.IsVisible = false;
            BtnAnotherOne.IsVisible = false;
            BtnGiveMeSomeExamples.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = false;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            LblPuntos.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;
            StackPleaseRememberTextAndImages.IsVisible = true;
            lblexample1.IsVisible = false;
            lblexample2.IsVisible = false;
            lblexample3.IsVisible = false;
            lblExamplePast.IsVisible = false;
            lblExamplePastSimple.IsVisible = false;
            LblTrad.IsVisible = false;
            LblTraduction.IsVisible = false;
            lblExamplePastParticiple.IsVisible = false;
            LblNumerosDeVerbosRestantes.IsVisible = false;
            BtnListOfTheVerbs.IsVisible = true;
            LblPuntos.Text = "0";
            btnAjustes.IsVisible = true;
            App.Torneo = "N";
        }

        private async void BtnTablaDePosiciones_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            StacklayoutPrincipal.IsVisible = false;
            btnAjustes.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = true;
            GridVolverAtrasPosiciones.IsVisible = true;

            var datos = await metodos.GetListadoDePosiciones();
            lsv_TablaPuntuacion.ItemsSource = datos;
            UserDialogs.Instance.HideLoading();

        }

        private void BtnAtrasPosiciones_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            GridVolverAtrasPosiciones.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            GridVolverAtras.IsVisible = false;
            GridVerbos.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnOtherTopics.IsVisible = true;

            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            LblPuntos.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;
            StackPleaseRememberTextAndImages.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            btnAjustes.IsVisible = true;



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
            if (await DisplayAlert("Information", "¿Do you want to finish the tournament?", "Of course", "Noo"))
            {
                UserDialogs.Instance.ShowLoading("I'm eating a cookie, give me a few seconds");
                BtnAtras_Clicked(new object(), new EventArgs());
                BtnTerminarTorneo.IsVisible = false;
                App.Torneo = "N";
                var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                App.SumaTotalDePuntos = 0;
                UserDialogs.Instance.HideLoading();
            }

        }

        public async void BtnListOfTheVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            GridVolverAtrasVerbList.IsVisible = true;
            StackLayoutVerbList.IsVisible = true;
            btnAjustes.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            StacklayoutPrincipal.IsVisible = false;
            var datos = await metodos.GetListadoVerbos();
            lsv_ListaDeVerbos.ItemsSource = datos;
            UserDialogs.Instance.HideLoading();

        }

        public void BtnAtrasVerbList_Clicked(System.Object sender, System.EventArgs e)
        {
            GridVolverAtrasVerbList.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnOtherTopics.IsVisible = true;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            StackPleaseRememberTextAndImages.IsVisible = true;
            btnAjustes.IsVisible = true;


        }

        public void btnAjustes_Clicked(System.Object sender, System.EventArgs e)
        {
            GridVolverAtrasSettings.IsVisible = true;
            StackLayoutSettings.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            StacklayoutPrincipal.IsVisible = false;

        }

        void BtnAtrasSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            GridVolverAtrasSettings.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutSettings.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnOtherTopics.IsVisible = true;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            StackPleaseRememberTextAndImages.IsVisible = true;
            btnAjustes.IsVisible = true;

        }

        async void BtnSaveChanges_Clicked(System.Object sender, System.EventArgs e)
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

        private void BtnOtherTopics_Clicked(object sender, EventArgs e)
        {
            StackLayoutOtherTopics.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            btnAjustes.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            StacklayoutPrincipal.IsVisible = false;

            UserDialogs.Instance.HideLoading();
        }

        private void BtnAtrasOtherTopics_Clicked(object sender, EventArgs e)
        {
            StackLayoutOtherTopics.IsVisible = false;

            GridVolverAtrasVerbList.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnOtherTopics.IsVisible = true;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            StackPleaseRememberTextAndImages.IsVisible = true;
            btnAjustes.IsVisible = true;

        }

        private async void btnWasWereDid_Clicked(object sender, EventArgs e)
        {
            StackLayoutOtherTopics.IsVisible = false;
            StackLayoutWasWereDid.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            var datos = await metodos.GetWasWereSentences();
            listSentences = datos;
            var random = new Random().Next(1, listSentences.Count);
            var elegido = listSentences[random];
            lblWasWereDid.Text = listSentences[0].WasWereSentence;
            CorrectAnswer = listSentences[0].correctanswer;
            listSentences.Remove(elegido);
            UserDialogs.Instance.HideLoading();
        }

        private void BtnAtrasWasWereDid_Clicked(object sender, EventArgs e)
        {
            StackLayoutWasWereDid.IsVisible = false;

            GridVolverAtrasVerbList.IsVisible = false;
            StackLayoutOtherTopics.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
        }

        private void BtnCheckMyAnswer_Clicked(object sender, EventArgs e)
        {
            if (CorrectAnswer == TxtxCorrectAnswer.Text.ToUpper())
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

        private void BtnOneMore_Clicked(object sender, EventArgs e)
        {
            if (listSentences.Count == 1)
            {
                UserDialogs.Instance.Toast("There's not more sentences.");
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
            StackLayoutOtherTopics.IsVisible = false;
            StackLayoutMatch.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking a coffee");
            var datos = await metodos.GetMatchSentences();
            listMatch = datos;
            lsv_Math.ItemsSource = datos;
            UserDialogs.Instance.HideLoading();
        }

        private void BtnAtrasMatch_Clicked(object sender, EventArgs e)
        {
            StackLayoutWasWereDid.IsVisible = false;
            StackLayoutMatch.IsVisible = false;
            GridVolverAtrasVerbList.IsVisible = false;
            StackLayoutOtherTopics.IsVisible = true;
            StackLayoutVerbList.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
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
                    if (item.textbox.ToUpper() == item.CorrectAnswer)
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

    }
}
