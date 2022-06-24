using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Android.Gms.Ads;
using AndroidX.AppCompat.App;
using Android.Speech.Tts;
using Plugin.FirebasePushNotification;
using Firebase;
using MediaManager.Forms.Platforms.Android;
using MediaManager;
using Xamarin.Forms;

namespace PleaseRememberMe.Droid
{
    [Activity(Label = "Remember Me", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        TextToSpeech textToSpeech;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Rg.Plugins.Popup.Popup.Init(this);
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo; ///1st LINE DUDE TO WORK !
            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //If debug you should reset the token each time.
            MobileAds.Initialize(ApplicationContext);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}