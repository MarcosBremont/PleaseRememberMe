using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using PleaseRememberMe.Models;
using Xamarin.Essentials;

namespace PleaseRememberMe.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        AndroidNotificationManager androidNotification = new AndroidNotificationManager();
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            androidNotification.CrearNotificacionLocal(message.GetNotification().Title, message.GetNotification().Body);

        }
        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            Preferences.Set("TokenFirebase", token);
            sedRegisterToken(token);

        }
        public void sedRegisterToken(string token)
        {
            try
            {
                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TokenFirebase.txt");


                // This text is added only once to the file.
                if (!File.Exists(_fileName))
                {
                    // Create a file to write to.
                    File.WriteAllText(_fileName, token);
                }

                // Open the file to read from.

            }
            catch (Exception ex)
            {
                
            }
         
        }

    }
}