using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using static App1.LoginPage;
using Firebase.Database.Query;

namespace App1
{
    public partial class ProfilePage : ContentPage
    {
        public string WebAPIKey = "AIzaSyArevEEm7iw4hZEHN7WDWRQKNOCM02oPeA";
        public ProfilePage()
        {
            InitializeComponent();
            GetProfileInformationAndRefreshToken();
        }

        private async void OnChangeEmailClicked(object sender, EventArgs e)
        {
            // 이메일 주소 변경 로직 추가
            // Firebase 또는 웹 서비스를 통해 이메일 주소 변경 기능을 호출합니다.
            // 호출 후 사용자에게 성공 또는 실패 메시지를 표시합니다.
            await DisplayAlert("알림", "이메일 주소 변경 기능을 구현하세요.", "확인");
        }
        private async void GetProfileInformationAndRefreshToken()
        {

            try
            {
                var firebase = new FirebaseClient("https://login-fba75-default-rtdb.firebaseio.com/");
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
                var savedFirebaseAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);



                // 사용자 정보를 Realtime Database에서 가져옵니다.
                var userInformation = await firebase.Child("users").Child(savedFirebaseAuth.User.LocalId).Child("UserInformation").OnceSingleAsync<UserInformation>();

                // 가져온 사용자 정보를 UI 요소에 표시합니다.
                NameEntry.Text = userInformation.Name;
                email.Text = $"이메일: {userInformation.UserNewEmail}";
                AgeLabel.Text = $"나이: {userInformation.Age}세";
                HeightLabel.Text = $"키: {userInformation.Height}cm";
                WeightLabel.Text = $"몸무게: {userInformation.Weight}kg";

                // BMI와 BMR 값을 계산합니다.
                double bmi = CalculateBMI(userInformation.Height, userInformation.Weight);
                double bmr = CalculateBMR(userInformation.Age, userInformation.Height, userInformation.Weight);

                // 계산된 BMI와 BMR 값을 UI 요소에 표시합니다.
                BMILabel.Text = $"BMI: {bmi:F2}";
                BMRLabel.Text = $"BMR: {bmr:F2} kcal/day";
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        private double CalculateBMI(double height, double weight)
        {
            double heightInMeters = height / 100;
            return weight / (heightInMeters * heightInMeters);
        }

        private double CalculateBMR(int age, double height, double weight)
        {
            // BMR을 계산하는 논리를 여기에 구현합니다.
            // 예시: Mifflin-St Jeor 방정식을 사용한 BMR 계산
            // 여기서는 간단한 예시로만 제시하였으며, 실제로는 보다 정확한 방법을 사용해야 합니다.
            double bmr = 10 * weight + 6.25 * height - 5 * age + 5;
            return bmr;
        }



        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            // 비밀번호 재설정 로직 추가
            // Firebase 또는 웹 서비스를 통해 비밀번호 재설정 기능을 호출합니다.
            // 호출 후 사용자에게 성공 또는 실패 메시지를 표시합니다.
            await DisplayAlert("알림", "비밀번호 재설정 기능을 구현하세요.", "확인");
        }

        private async void OnSmsVerificationClicked(object sender, EventArgs e)
        {
            // SMS 인증 로직 추가
            // Firebase 또는 웹 서비스를 통해 SMS 인증 기능을 호출합니다.
            // 호출 후 사용자에게 성공 또는 실패 메시지를 표시합니다.
            await DisplayAlert("알림", "SMS 인증 기능을 구현하세요.", "확인");
        }
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