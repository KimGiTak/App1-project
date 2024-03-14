using System;
using System.Collections.Generic;
using App1;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class LoginPage : ContentPage
    {
        public string WebAPIKey = "AIzaSyArevEEm7iw4hZEHN7WDWRQKNOCM02oPeA";
        
        // Constructor
        public LoginPage()
        {
            InitializeComponent();
            GetProfileInformationAndRefreshToken();
        }

        // Method to retrieve user profile information and refresh token
        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            try
            {
                // Retrieve saved Firebase authentication token
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                
                // Refresh the token
                var refreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refreshedContent));

                // Access Firebase Realtime Database
                var firebase = new FirebaseClient("https://login-fba75-default-rtdb.firebaseio.com/");
                
                // Retrieve user information from Realtime Database
                var userInformation = await firebase.Child("users").Child(savedfirebaseauth.User.LocalId).OnceSingleAsync<UserInformation>();

                // Display user information
                NameEntry.Text = userInformation.Name;
                email.Text = $"이메일: {userInformation.UserNewEmail}";
                AgeLabel.Text = $"나이: {userInformation.Age}세";
                HeightLabel.Text = $"키: {userInformation.Height}cm";
                WeightLabel.Text = $"몸무게: {userInformation.Weight}kg";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Alert", "토큰이 만료되었습니다", "OK");
            }
        }

        // Method to handle logout button click
        void Logout_Clicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        // Class representing user information
        public class UserInformation
        {
            public string Name { get; set; }
            public string UserNewEmail { get; set; }
            public string UserNewPassword { get; set; }
            public int Age { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
        }
    }
}
