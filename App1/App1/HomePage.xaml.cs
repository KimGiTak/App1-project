using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnMyButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void OnMapButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}
