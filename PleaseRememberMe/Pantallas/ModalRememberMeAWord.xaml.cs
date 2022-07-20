using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PleaseRememberMe.Models;
using System.IO;
using PleaseRememberMe.Utilitarios;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalRememberMeAWord : Rg.Plugins.Popup.Pages.PopupPage
    {
        string ruta_archivo_configuracion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Configuracion.nombre_archivo_configuracion);

        Metodos metodos = new Metodos();

        public event EventHandler OnLLamarOtraPantalla;

        public ModalRememberMeAWord()
        {
            InitializeComponent();
            OnLLamarOtraPantalla?.Invoke(this, EventArgs.Empty);
        }


        private async void BtnEnter_Clicked(object sender, EventArgs e)
        {

            string lines = File.ReadAllText(ruta_archivo_configuracion);

            File.WriteAllText(ruta_archivo_configuracion, txtWord.Text + " , " +  lines);
            if (File.Exists(ruta_archivo_configuracion))
            {
                Configuracion.Server = File.ReadAllText(ruta_archivo_configuracion);
            }
            else
            {
                //toastConfig.MostrarNotificacion("Error al guardar la nueva conexión", ToastPosition.Top, 3, "#FC412F");
            }

            string liness = File.ReadAllText(ruta_archivo_configuracion);

            UserDialogs.Instance.ShowLoading("Saving your word, wait...");
            UserDialogs.Instance.HideLoading();
            txtWord.Text = "";
            OnLLamarOtraPantalla(sender, e);
        }

        private async void BtnCancell_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            txtWord.Text = "";
        }
    }
}