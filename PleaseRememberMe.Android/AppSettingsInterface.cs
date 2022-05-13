using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Android.Content;
using Xamarin.Forms;
using Application = Android.App.Application;
using PleaseRememberMe.Droid;

[assembly: Dependency(typeof(AppSettingsInterface))]
namespace PleaseRememberMe.Droid
{
    public class AppSettingsInterface : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApnSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            var uri = Android.Net.Uri.FromParts("package", Android.App.Application.Context.PackageName, null);
            intent.SetData(uri);
            Application.Context.StartActivity(intent);
        }
    }
}