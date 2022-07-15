﻿using Acr.UserDialogs;
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
            CorrectAnswerVerbToBe2 = "", CorrectAnswerQuantifiers = "", AnswerRadioButton1 = "",
            AnswerRadioButton2 = "", audioVerboFormaBase = "", audioverboSimplePast = "", audioverboPasParticiple = "",
            ExamplePastSimple = "", ExamplePastParticiple = "", audiolink = "", DefinitiveAnswer = "", DefinitiveAnswer2 = "",
            TextoCategoria = "";
        #endregion
        #region Listas
        List<Entidad.EVerbos> listadodelosverbos = new List<Entidad.EVerbos>();

        List<Entidad.EWasWereDid> listSentences = new List<Entidad.EWasWereDid>();
        List<Entidad.EQuestionsWithHow> listQuestionsWithHow = new List<Entidad.EQuestionsWithHow>();
        List<Entidad.EMatchSentences> listMatch = new List<Entidad.EMatchSentences>();
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
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.StateChanged += Current_OnStateChanged;

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

        private void BtnMatch_Clicked(object sender, EventArgs e)
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

      

        private void BtnAtrasAnySome_Clicked(object sender, EventArgs e)
        {
            
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

        
     
      

        private void BtnAtrasSimplePastCategory_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }


     
        private async void btnVerbToBe_Clicked(object sender, EventArgs e)
        {
            try
            {
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

            
        }

        private async void BtnAtrasVocabulary_Clicked(object sender, EventArgs e)
        {
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

            await Navigation.PushModalAsync(new CategoriesPage());
            StackLayoutVocabularyCategory.IsVisible = false;
        }

        private async void BtnCategories_Clicked(object sender, EventArgs e)
        {
            //AnuncioParaCategories.IsVisible = true;
            Anuncio.IsVisible = true;
            UserDialogs.Instance.ShowLoading("Wait, Do you want coffee?");
            await Navigation.PushModalAsync(new CategoriesPage());
            UserDialogs.Instance.HideLoading();
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

        private void btnsimplepastexerci_Clicked(object sender, EventArgs e)
        {
        }

        private void BtnAtrasSimplePastExample_Clicked(object sender, EventArgs e)
        {
            
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void btnsimplepresentexerci_Clicked(object sender, EventArgs e)
        {
            StackLayoutsimplePresentExercises.IsVisible = false;
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
            
            StackLayoutsimplePresentExercises.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private void BtnAtrasSimplePresentExercises_Clicked(object sender, EventArgs e)
        {
            StackLayoutsimplePresentExercises.IsVisible = false;
            
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

        private async void BtnAtrasVideos_Clicked(object sender, EventArgs e)
        {
            StackLayoutVideos.IsVisible = false;
            await Navigation.PushModalAsync(new CategoriesPage());
            Anuncio.IsVisible = true;
        }

        private async void BtnVideos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoriesPage());
            StackLayoutVideos.IsVisible = true;
            var datos = await metodos.GetVideos();
            lsv_Videos.ItemsSource = datos;
        }

        private void BtnVideoClick_Clicked(object sender, EventArgs e)
        {
            StackLayoutVideos.IsVisible = false;

            var b = (Button)sender;

            var ob = b.CommandParameter as EVideos;

            if (ob != null)
            {

                // retrieve the value from the ‘ob’ and continue your work.

            }

            App.LinkVideo = ob.link;
            App.TituloVideo = ob.title;
            StackLayoutVideoPage.IsVisible = true;
            LblTitle.Text = App.TituloVideo;
            LblDescriptionVideo.Text = ob.videodescription;

            mediaElement.Source = App.LinkVideo;
            //CrossMediaManager.Current.Play(App.LinkVideo, MediaFileType.Video);
        }

        private void BtnAtrasVideoPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutVideoPage.IsVisible = false;
            mediaElement.Pause();
            StackLayoutVideos.IsVisible = true;
        }

        private async void BtnAudios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoriesPage());
            StackLayoutAudios.IsVisible = true;
            var datos = await metodos.GetAudios();
            lsv_audios.ItemsSource = datos;
        }

        private async void BtnAtrasAudios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CategoriesPage());
            StackLayoutAudios.IsVisible = false;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

        }

        private void BtnAtrasAudioPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutAudioPage.IsVisible = false;
            StackLayoutAudios.IsVisible = true;
            BtnReiniciarAudio_Clicked(new object(), new EventArgs());
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
        }

        private async void BtnReproducirAudio_Clicked(object sender, EventArgs e)
        {
            SetupCurrentMediaPositionData(CrossMediaManager.Current.Position);
            var currentMediaItem = await CrossMediaManager.Current.Play(audiolink);
            SetupCurrentMediaDetails(currentMediaItem);
        }

        private void SetupCurrentMediaDetails(IMediaItem currentMediaItem)
        {
            // Set up Media item details in UI
            var displayDetails = string.Empty;
            if (!string.IsNullOrEmpty(currentMediaItem.DisplayTitle))
                displayDetails = currentMediaItem.DisplayTitle;

            if (!string.IsNullOrEmpty(currentMediaItem.Artist))
                displayDetails = $"{displayDetails} - {currentMediaItem.Artist}";

            //LabelMediaDetails.Text = displayDetails.ToUpper();
        }

        private async void BtnPausarAudio_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.PlayPause();
        }

        private async void BtnReiniciarAudio_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
        }

        private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPlayerState(e.State);
            });
        }

        private async void ForwardButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.StepForward();
        }

        private async void RewindButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.StepBackward();
        }


        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            SetupCurrentMediaDetails(e.MediaItem);
        }

        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPositionData(e.Position);
            });
        }

        private void SetupCurrentMediaPositionData(TimeSpan currentPlaybackPosition)
        {
            var formattingPattern = @"hh\:mm\:ss";
            if (CrossMediaManager.Current.Duration.Hours <= 0)
                formattingPattern = @"mm\:ss";

            var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
            LabelPositionStatus.Text = $"{currentPlaybackPosition.ToString(formattingPattern)}/{fullLengthString}";

            SliderSongPlayDisplay.Value = currentPlaybackPosition.Ticks;
        }

        private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        {
            //LabelPlayerStatus.Text = $"{currentPlayerState.ToString().ToUpper()}";

            if (currentPlayerState == MediaManager.Player.MediaPlayerState.Loading)
            {
                SliderSongPlayDisplay.Value = 0;
            }
            else if (currentPlayerState == MediaManager.Player.MediaPlayerState.Playing
                    && CrossMediaManager.Current.Duration.Ticks > 0)
            {
                SliderSongPlayDisplay.Maximum = CrossMediaManager.Current.Duration.Ticks;
            }
        }

        private async void BtnAudioClick_Clicked(object sender, EventArgs e)
        {
            StackLayoutAudios.IsVisible = false;

            var b = (Button)sender;

            var ob = b.CommandParameter as EAudios;

            if (ob != null)

            {

                // retrieve the value from the ‘ob’ and continue your work.

            }

            LblTitleAudio.Text = ob.title;
            audiolink = ob.link;
            StackLayoutAudioPage.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");

        }

       

        public async void CargarInformacionVocabularyGlobalPage(string category)
        {
            UserDialogs.Instance.ShowLoading("Did you drink water today?");
            var datos = await metodos.GetVocabularyGlobal(category);
            lsv_VocabularyGlobal.ItemsSource = datos;
            LblTitleVocabularyGlobal.Text = datos[0].Vocabulary_category;
            UserDialogs.Instance.HideLoading();
        }

       
       

        private void btnVocabularyPhrasalVerbs_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Can you give me a hand?");
            StackLayoutVocabularyCategory.IsVisible = false;
            TextoCategoria = "Phrasal Verbs";
            CargarInformacionVocabularyGlobalPage(TextoCategoria);
            StacklayoutVocabularyGlobal.IsVisible = true;
            ContenPage.BackgroundColor = Color.FromHex("#2196F3");
            UserDialogs.Instance.HideLoading();

        }

        private void BtnAtrasVocabularyGlobal_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Let's see phrasal verbs");
            StackLayoutVocabularyCategory.IsVisible = true;
            StacklayoutVocabularyGlobal.IsVisible = false;
            UserDialogs.Instance.HideLoading();
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
      
        private void BtnAtrasQuantifiers_Clicked(object sender, EventArgs e)
        {
            StackLayoutQuantifiers.IsVisible = false;
            
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
