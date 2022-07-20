using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PleaseRememberMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchPage : ContentPage
    {
        Metodos metodos = new Metodos();
        string DefinitiveAnswer = "";
        string DefinitiveAnswer2 = "";
        List<Entidad.EExercises> listMatch = new List<Entidad.EExercises>();

        public MatchPage()
        {
            InitializeComponent();
            CargarInformacionTitlePage();
        }

        public async void CargarInformacionTitlePage()
        {
            UserDialogs.Instance.ShowLoading("Did you drink water today?");
            var datos = await metodos.GetExercisesByCategory(App.Categoria);
            listMatch = datos;
            
            DefinitiveAnswer = datos[0].CorrectAnswer;
            DefinitiveAnswer2 = datos[0].CorrectAnswer2;
            UserDialogs.Instance.HideLoading();
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

        private async void BtnAtrasMatch_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


    }
}