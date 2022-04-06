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

            if (string.IsNullOrEmpty(txtVerbInPastSimple.Text) || string.IsNullOrEmpty(txtVerbInPastParticiple.Text))
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
                    LblVerbInPastSimpleCheck.Text = "Incorrect";
                }

            
            }

            if (string.IsNullOrEmpty(txtVerbInPastParticiple.Text))
            {
                LblVerbInPastSimpleCheck.Text = "Incorrect";
            }
            else
            {
                if (txtVerbInPastParticiple.Text.ToUpper() == VerbInPastParticiple)
                {
                    LblVerbInPastParticipleCheck.Text = "Correct";
                }
                else
                {
                    LblVerbInPastParticipleCheck.Text = "Incorrect";
                }


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
    }
}
