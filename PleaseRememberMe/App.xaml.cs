﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PleaseRememberMe
{
    public partial class App : Application
    {
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
