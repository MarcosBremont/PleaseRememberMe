using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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


        public App()
        {
            InitializeComponent();

            MainPage = new Pantallas.PaginaPrincipal();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
