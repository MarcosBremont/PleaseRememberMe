using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PleaseRememberMe.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPage : ContentPage
    {
        public AudioPage()
        {
            InitializeComponent();
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.StateChanged += Current_OnStateChanged;
        }

        private async void BtnAtrasAudioPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            await CrossMediaManager.Current.Stop();

        }

        private async void BtnReproducirAudio_Clicked(object sender, EventArgs e)
        {
            SetupCurrentMediaPositionData(CrossMediaManager.Current.Position);
            var currentMediaItem = await CrossMediaManager.Current.Play(App.AudioLink);
            SetupCurrentMediaDetails(currentMediaItem);
        }
        private void SetupCurrentMediaDetails(IMediaItem currentMediaItem)
        {
            // Set up Media item details in UI
            var displayDetails = string.Empty;
            if (!string.IsNullOrEmpty(currentMediaItem.DisplayTitle))
                displayDetails = currentMediaItem.DisplayTitle;

            if (!string.IsNullOrEmpty(currentMediaItem.Artist))
                displayDetails = $"{displayDetails} - {currentMediaItem.Artist}";

            //LabelMediaDetails.Text = displayDetails.ToUpper();
        }

        private async void BtnPausarAudio_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.PlayPause();
        }

        private async void BtnReiniciarAudio_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
        }

        private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPlayerState(e.State);
            });
        }

        private async void ForwardButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.StepForward();
        }

        private async void RewindButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.StepBackward();
        }


        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            SetupCurrentMediaDetails(e.MediaItem);
        }

        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPositionData(e.Position);
            });
        }

        private void SetupCurrentMediaPositionData(TimeSpan currentPlaybackPosition)
        {
            var formattingPattern = @"hh\:mm\:ss";
            if (CrossMediaManager.Current.Duration.Hours <= 0)
                formattingPattern = @"mm\:ss";

            var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
            LabelPositionStatus.Text = $"{currentPlaybackPosition.ToString(formattingPattern)}/{fullLengthString}";

            SliderSongPlayDisplay.Value = currentPlaybackPosition.Ticks;
        }

        private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        {
            //LabelPlayerStatus.Text = $"{currentPlayerState.ToString().ToUpper()}";

            if (currentPlayerState == MediaManager.Player.MediaPlayerState.Loading)
            {
                SliderSongPlayDisplay.Value = 0;
            }
            else if (currentPlayerState == MediaManager.Player.MediaPlayerState.Playing
                    && CrossMediaManager.Current.Duration.Ticks > 0)
            {
                SliderSongPlayDisplay.Maximum = CrossMediaManager.Current.Duration.Ticks;
            }
        }



       
    }
}