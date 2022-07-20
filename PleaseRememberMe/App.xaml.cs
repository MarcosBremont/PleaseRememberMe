using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.FirebasePushNotification;
using System.Diagnostics;
using MediaManager;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PleaseRememberMe.Utilitarios;
using PleaseRememberMe.Pantallas;

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
        public static string VideoDescription { get; set; }
        public static string TituloVideo { get; set; }
        public static string Categoria { get; set; }
        public static string Vocabulary { get; set; }
        public static string AudioTitle { get; set; }
        public static string AudioLink { get; set; }
        public static string Wordsss { get; set; }


        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental" });

            InitializeComponent();
            CrossMediaManager.Current.Init();

            MainPage = new Pantallas.PrincipalPage();
        }

   

        protected override void OnStart()
        {
            string ruta_archivo_configuracion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Configuracion.nombre_archivo_configuracion);
            List<String> palabras = new List<string>();
            Random random = new Random();

            string[] lines = File.ReadAllLines(ruta_archivo_configuracion);
            string TextoBien = "";

            Task.Run(() =>
            {
                // some long running task

                while (true)
                {
                    if (File.Exists(ruta_archivo_configuracion))
                    {
                        TextoBien = File.ReadAllText(ruta_archivo_configuracion);
                        TextoBien = TextoBien.Trim();
                        palabras = TextoBien.Split(',').ToList();

                        App.Wordsss = palabras[(random.Next(0, palabras.Count - 1))];
                        PrincipalPage principalPage = new PrincipalPage();
                        principalPage.PropertyChanged()
                    }
                    Thread.Sleep(1000);
                }
            });
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
