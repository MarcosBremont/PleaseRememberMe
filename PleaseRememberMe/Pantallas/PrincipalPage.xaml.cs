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
using System.Threading;
using PleaseRememberMe.Entidad;
using MediaManager;
using PositionChangedEventArgs = MediaManager.Playback.PositionChangedEventArgs;
using MediaManager.Player;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Media;

namespace PleaseRememberMe.Pantallas
{
    public partial class PrincipalPage : ContentPage
    {
        #region Variables String
        string VerbInSimplePast = "", VerbInPastParticiple = "", Traduccion = "", CorrectAnswer = "",
            CorrectAnswerPronoun = "", CorrectAnswerVerb = "", CorrectAnswerQuestionWithHow = "", CorrectAnswerPrepositionsOfTime = "",
            CorrectAnswerFamily = "", CorrectAnswerAnySome = "", CorrectAnswerVerbToBe1 = "",
            CorrectAnswerVerbToBe2 = "", audioVerboFormaBase = "", audioverboSimplePast = "", audioverboPasParticiple = "",
            ExamplePastSimple = "", ExamplePastParticiple = "", audiolink = "", DefinitiveAnswer = "", DefinitiveAnswer2 = "",
            TextoCategoria = "";
        #endregion
        #region Listas
        List<Entidad.EVerbos> listadodelosverbos = new List<Entidad.EVerbos>();

        List<Entidad.EWasWereDid> listSentences = new List<Entidad.EWasWereDid>();
        List<Entidad.EQuestionsWithHow> listQuestionsWithHow = new List<Entidad.EQuestionsWithHow>();
        List<Entidad.ECompleteSentences> listComplete = new List<Entidad.ECompleteSentences>();
        List<Entidad.ESuperlativesSentence> listSuperlatives = new List<Entidad.ESuperlativesSentence>();
        List<Entidad.EClothes> listclothes = new List<Entidad.EClothes>();
        List<Entidad.EPronouns> listpronouns = new List<Entidad.EPronouns>();
        List<Entidad.ESimplePresent> listsimplepresent = new List<Entidad.ESimplePresent>();
        List<Entidad.EPrepositionsOfTime> listprepositionsOfTime = new List<Entidad.EPrepositionsOfTime>();
        List<Entidad.EFamily> listFamily = new List<Entidad.EFamily>();
        List<Entidad.EAnySome> listAnySome = new List<Entidad.EAnySome>();
        List<Entidad.EVerbToBe> listVerbToBe = new List<Entidad.EVerbToBe>();
        List<Entidad.EQuantifiers> listQuantifiers = new List<Entidad.EQuantifiers>();
        List<Entidad.EExercises> listexercises = new List<Entidad.EExercises>();
        #endregion
        #region Declaraciones
        ModalTournament modalTournament = new ModalTournament();
        Metodos metodos = new Metodos();
        IEnumerable<Locale> locales;
        static CrossLocale? localeee = null;
        #endregion
        private bool _userTapped;

        public PrincipalPage()
        {
            InitializeComponent();

            //LblTorneoEnCurso.IsVisible = false;
            //LblTorneo.IsVisible = false;
            //LblPuntos.IsVisible = false;
            //BtnTerminarTorneo.IsVisible = false;


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

                     await PopupNavigation.PushAsync(modalTournament);
                     await Task.Delay(1000);
                     _userTapped = false;
                     Opacity = 1;
                 }),
                 NumberOfTapsRequired = 1

             }
           );

        }

        async private void ModalTournament_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TournamentPage());
            App.SumaTotalDePuntos = 0;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            Anuncio.IsVisible = true;
        }

        private async void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Verbs());
        }

        private async void BtnTablaDePosiciones_Clicked(object sender, EventArgs e)
        {
            BtnTablaDePosiciones.IsEnabled = false;
            try
            {
                using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
                {
                    Anuncio.IsVisible = true;
                    await Navigation.PushModalAsync(new Leaderboard());
                   
                }
                //UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                BtnTablaDePosiciones.IsEnabled = true;

                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }

            BtnTablaDePosiciones.IsEnabled = true;
        }

        

        private async void BtnTournament_Clicked(object sender, EventArgs e)
        {
            if (_userTapped)
                return;

            _userTapped = true;
            modalTournament = new ModalTournament();
            modalTournament.OnLLamarOtraPantalla += ModalTournament_OnLLamarOtraPantalla;
            //modalTournament.Disappearing += ModalTournament_Disappearing;

            await PopupNavigation.PushAsync(modalTournament);
            await Task.Delay(1000);
            _userTapped = false;
            Opacity = 1;
        }



        public async void BtnListOfTheVerbs_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                BtnListOfTheVerbs.IsEnabled = false;
                using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
                {
                    Anuncio.IsVisible = true;
                    await Navigation.PushModalAsync(new VerbList());
                }
                BtnListOfTheVerbs.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }
        public async void btnAjustes_Clicked(System.Object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                SettingsPage settingsPage = new SettingsPage();
                Anuncio.IsVisible = false;
                await Navigation.PushModalAsync(new SettingsPage());
            }

        }

        void BtnAtrasSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading("loading", null, null, true, MaskType.Black))
            {
                Anuncio.IsVisible = true;
                StacklayoutPrincipal.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
            }
        }


        private void BtnAtrasOtherTopics_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = false;
            Anuncio.IsVisible = true;
            StacklayoutPrincipal.IsVisible = true;
            BtnLetsGo.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#80FFB6");
        }



        private async void btnComplete_Clicked(object sender, EventArgs e)
        {
            try
            {
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

        

        void BtnAtrasClothes_Clicked(System.Object sender, System.EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasPronouns_Clicked(object sender, EventArgs e)
        {
            StackLayoutPronouns.IsVisible = false;
            
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
            

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Conexión no establecida, verifica tu conexión a internet");

            }
        }

        private void BtnAtrasSimplePresent_Clicked(object sender, EventArgs e)
        {
            StackLayoutSimplePresent.IsVisible = false;
            
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
            
            StackLayoutPreposisionsOfTime.IsVisible = false;
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
     
      

        private void BtnAtrasSimplePastCategory_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnCategories_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = true;
            Anuncio.IsVisible = true;
            UserDialogs.Instance.ShowLoading("Wait, Do you want coffee?");
            await Navigation.PushModalAsync(new CategoriesPage());
            UserDialogs.Instance.HideLoading();
        }



        private void BtnAtrasProfessionsActivities_Clicked(object sender, EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasProfessionsPage_Clicked(object sender, EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasClothesPage_Clicked(object sender, EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnActivitiesClothes_Clicked(object sender, EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasClothessActivities_Clicked(object sender, EventArgs e)
        {
            
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }
        private void BtnAtrasFamilyPage_Clicked(object sender, EventArgs e)
        {
            
            StackLayoutFamilyPage.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnVocabularyFamily_Clicked(object sender, EventArgs e)
        {
            try
            {
                Anuncio.IsVisible = false;

                StackLayoutFamilyPage.IsVisible = false;
                //StacklayoutVocabularyFamily.IsVisible = true;
                ContenPage.BackgroundColor = Color.FromHex("#2196F3");
                UserDialogs.Instance.ShowLoading("Wait a minute, I'm eating a cookie");
                var datos = await metodos.GetVocabularyFamily();
                //lsv_VocabularyFamilyWords.ItemsSource = datos;
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

        private void btnsimplepastexerci_Clicked(object sender, EventArgs e)
        {
        }

        private void BtnAtrasSimplePastExample_Clicked(object sender, EventArgs e)
        {
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void btnsimplepresentexerci_Clicked(object sender, EventArgs e)
        {
            StackLayoutSimplePresent.IsVisible = true;


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


            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasSimplePresentExample_Clicked(object sender, EventArgs e)
        {
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void btnsimplepresentexerciGo_Clicked(object sender, EventArgs e)
        {
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasSimplePresentExercises_Clicked(object sender, EventArgs e)
        {
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

       

       
        private void BtnAtrasComparisons_Clicked(object sender, EventArgs e)
        {

            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasComparisonsActivities_Clicked(object sender, EventArgs e)
        {

            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

      

        private void BtncorrectSuperlative_Clicked(object sender, EventArgs e)
        {
            foreach (var item in listSuperlatives)
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
            lsv_superlatives.ItemsSource = null;
            lsv_superlatives.ItemsSource = listSuperlatives;
        }

        private void BtnAtrasSuperlativesExercises_Clicked(object sender, EventArgs e)
        {
            StackLayoutSuperlativesExercises.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }
       
        private void BtnAtrasFamilysActivities_Clicked(object sender, EventArgs e)
        {
            StackLayoutFamilyActivities.IsVisible = false;
            StackLayoutFamilyPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnOneMoreChoose_Clicked(object sender, EventArgs e)
        {
            CargarInformacionTitlePage(TextoCategoria);
        }

        public async void CargarInformacionTitlePage(string category)
        {
            UserDialogs.Instance.ShowLoading("Did you drink water today?");
            var datos = await metodos.GetExercisesByCategory(category);
            //LbltitlePageBase.Text = datos[0].Title;
            //LblAnotherTitle.Text = datos[0].AnotherTitle;
            //LblSubtitlePageBase.Text = datos[0].Subtitle;
            //LblDescriptionPageBase.Text = datos[0].Description;
            //LblSentencesPageBase.Text = datos[0].Sentences;
            //string YourAnswer = TxtAnswer.Text;
            DefinitiveAnswer = datos[0].CorrectAnswer;
            DefinitiveAnswer2 = datos[0].CorrectAnswer2;
            UserDialogs.Instance.HideLoading();

        }

        

        private void BtnAtrasQuestionsWithHow_Clicked(object sender, EventArgs e)
        {
            StackLayoutQuestionsWithHow.IsVisible = false;
            
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

        
       

    }
}
