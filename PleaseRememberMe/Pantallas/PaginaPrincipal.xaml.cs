using PleaseRememberMe.Models;
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
        string ExampleInBaseForm = "";
        string ExamplePastSimple = "";
        string ExampleInPastParticiple = "";
        string Traduccion = "";
        Metodos metodos = new Metodos();
        public PaginaPrincipal()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }


        public async void GetRandomVerb()
        {
            var response = await metodos.GetListadoVerbos();
            var random = new Random().Next(1, response.Count);
            var datos = response[random];
            txtVerbInPast.Text = datos.VerboFormaBase;
            VerbInSimplePast = datos.verboSimplePast;
            VerbInPastParticiple = datos.verboPasParticiple;
            ExampleInBaseForm = datos.examplesInBaseForm;
            ExamplePastSimple = datos.verboSimplePast;
            ExampleInPastParticiple = datos.examplesInPastParticiple;
            Traduccion = datos.traduccion;
            
        }

        private void BtnLetsGo_Clicked(object sender, EventArgs e)
        {
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
        }

        private void BtnCheck_Clicked(object sender, EventArgs e)
        {
            LblTrad.IsVisible = true;
            LblTraduction.IsVisible = true;
            LblTraduction.Text = Traduccion;
            LblVerbInPastSimpleCheck.IsVisible = true;
            LblVerbInPastParticipleCheck.IsVisible = true;

            if (txtVerbInPastSimple.Text == VerbInSimplePast)
            {
                LblVerbInPastSimpleCheck.Text = "Correct";
            }
            else
            {
                LblVerbInPastSimpleCheck.Text = "Incorrect";
            }

            if (txtVerbInPastParticiple.Text == VerbInPastParticiple)
            {
                LblVerbInPastParticipleCheck.Text = "Correct";
            }
            else
            {
                LblVerbInPastParticipleCheck.Text = "Incorrect";
            }

        }

        private void BtnGiveMeSomeExamples_Clicked(object sender, EventArgs e)
        {

        }
    }
}
