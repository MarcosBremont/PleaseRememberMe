using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.FirebasePushNotification;
using System.Diagnostics;
using MediaManager;

namespace PleaseRememberMe
{
    public partial class App : Application
    {
        public static int SumaTotalDePuntos { get; set; }
        public static string Torneo { get; set; }
        public static string nombrePersona { get; set; }
        public static string direccion { get; set; }
        public static string YaPasoPorAqui { get; set; }
        public static string YaPasastePorGetRandomVerb { get; set; }
        public static string LinkVideo { get; set; }
        public static string TituloVideo { get; set; }
        public static string Categoria { get; set; }


        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental" });

            InitializeComponent();
            CrossMediaManager.Current.Init();

            MainPage = new Pantallas.PrincipalPage();
        }

   

        protected override void OnStart()
        {

            // Formatting numbers, dates, etc.
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            // UI strings that we have localized.
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            // Formatting numbers, dates, etc.
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            // UI strings that we have localized.
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
        }
    }
}
