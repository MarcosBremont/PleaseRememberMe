using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private bool _userTapped;
        ModalTournament modalTournament = new ModalTournament();


        Metodos metodos = new Metodos();
        public PaginaPrincipal()
        {
            InitializeComponent();
            GridVolverAtras.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblPuntos.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;

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


        }

        private void ModalTournament_OnLLamarOtraPantalla(object sender, EventArgs e)
        {
        }

        private void ModalTournament_Disappearing(object sender, EventArgs e)
        {
            if (App.Torneo == "S")
            {
                BtnLetsGo_Clicked(new object(), new EventArgs());
                BtnGiveMeSomeExamples.IsVisible = false;
                BtnAnotherOne.IsVisible = false;
                LblTorneo.IsVisible = true;
                LblTorneoEnCurso.IsVisible = true;
                BtnTerminarTorneo.IsVisible = true;


            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


        }


        public async void GetRandomVerb()
        {
            UserDialogs.Instance.ShowLoading("looking for verbs");
            var response = await metodos.GetListadoVerbos();
            var random = new Random().Next(1, response.Count);
            var datos = response[random];
            txtVerbInPast.Text = datos.VerboFormaBase.ToUpper();
            VerbInSimplePast = datos.verboSimplePast.ToUpper();
            VerbInPastParticiple = datos.verboPasParticiple.ToUpper();
            Traduccion = datos.traduccion.ToUpper();
            lblExamplePast.Text = datos.examplesInBaseForm;
            lblExamplePastSimple.Text = datos.examplesInSimplePast;
            lblExamplePastParticiple.Text = datos.examplesInPastParticiple;
            UserDialogs.Instance.HideLoading();

        }

        private void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
            BtnTerminarTorneo.IsVisible = false;

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
            GetRandomVerb();
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
            LblTrad.IsVisible = true;
            LblTraduction.IsVisible = true;
            LblTraduction.Text = Traduccion;
            LblVerbInPastSimpleCheck.IsVisible = true;
            LblVerbInPastParticipleCheck.IsVisible = true;

            if (string.IsNullOrEmpty(txtVerbInPastSimple.Text))
            {
                LblVerbInPastSimpleCheck.Text = "Incorrect";
            }
            else
            {
                if (txtVerbInPastSimple.Text.ToUpper() == VerbInSimplePast)
                {
                    LblVerbInPastSimpleCheck.Text = "Correct";

                }
                else
                {
                    if (App.Torneo == "S")
                    {
                        GetRandomVerb();
                        LimpiarText();
                    }
                    LblVerbInPastSimpleCheck.Text = "Incorrect";
                }


            }

            if (string.IsNullOrEmpty(txtVerbInPastParticiple.Text))
            {
                LblVerbInPastParticipleCheck.Text = "Incorrect";
            }
            else
            {
                if (txtVerbInPastParticiple.Text.ToUpper() == VerbInPastParticiple)
                {
                    LblVerbInPastParticipleCheck.Text = "Correct";
                }
                else
                {
                    if (App.Torneo == "S")
                    {
                        GetRandomVerb();
                        LimpiarText();

                    }
                    LblVerbInPastParticipleCheck.Text = "Incorrect";
                }


            }

            if (LblVerbInPastSimpleCheck.Text == "Correct" && LblVerbInPastParticipleCheck.Text == "Correct" && App.Torneo == "S")
            {
                LblPuntos.IsVisible = true;
                App.SumaTotalDePuntos = App.SumaTotalDePuntos + 1;
                BtnAnotherOne_Clicked(new object(), new EventArgs());
                LblPuntos.Text = App.SumaTotalDePuntos.ToString();
            }

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
            GridVerbos.IsVisible = false;
            GridVolverAtras.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnCheck.IsVisible = false;
            BtnAnotherOne.IsVisible = false;
            BtnGiveMeSomeExamples.IsVisible = false;
            lblVersion.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = false;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            LblPuntos.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;

        }

        private async void BtnTablaDePosiciones_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Wait a minute, I'm drinking coffee ");
            StacklayoutPrincipal.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = true;
            GridVolverAtrasPosiciones.IsVisible = true;
            var datos = await metodos.GetListadoDePosiciones();
            lsv_TablaPuntuacion.ItemsSource = datos;
            UserDialogs.Instance.HideLoading();

        }

        private void BtnAtrasPosiciones_Clicked(object sender, EventArgs e)
        {
            GridVolverAtrasPosiciones.IsVisible = false;
            StackLayoutTablaPosiciones.IsVisible = false;
            StacklayoutPrincipal.IsVisible = true;
            GridVolverAtras.IsVisible = false;
            GridVerbos.IsVisible = false;
            BtnLetsGo.IsVisible = true;
            BtnTablaDePosiciones.IsVisible = true;
            StackTournament.IsVisible = true;
            LblPuntos.IsVisible = false;
            LblTorneo.IsVisible = false;
            LblTorneoEnCurso.IsVisible = false;
            BtnTerminarTorneo.IsVisible = false;

        }

        private async void BtnTournament_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(modalTournament);
        }

        private async void BtnTerminarTorneo_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Information", "¿Do you want to finish the tournament?", "Of course", "Noo"))
            {
                UserDialogs.Instance.ShowLoading("I'm eating a cookie, give me a few seconds");
                BtnAtras_Clicked(new object(), new EventArgs());
                BtnTerminarTorneo.IsVisible = false;
                var apiResult = await metodos.EnterToTheTournament(App.nombrePersona, App.SumaTotalDePuntos, App.direccion);
                UserDialogs.Instance.HideLoading();
            }

        }
    }
}
