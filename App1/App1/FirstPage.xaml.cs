using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1
{
    public partial class FirstPage : ContentPage
    {
        private double BMI;
        private double BMR;
        private int Age;
        private double Height;
        private double Weight;
        private string BMStatus;
        private string SelectedGoal;
        private double CarbIntake;
        private double ProteinIntake;
        private double FatIntake;

        public FirstPage(int age, double height, double weight, double bmi, double bmr, string bmiStatus, string selectedGoal, double carbIntake, double proteinIntake, double fatIntake)
        {
            InitializeComponent();

            // 전달받은 값으로 Entry 컨트롤에 설정합니다.
            AgeEntry.Text = age.ToString() + "세";
            HeightEntry.Text = (height * 100).ToString() + "cm"; // 키 값을 cm 단위로 표시합니다.
            WeightEntry.Text = weight.ToString() + "kg";
            BMILabel.Text = $"BMI: {bmi:F2}";
            BMRLabel.Text = $"BMR: {bmr:F2} kcal/day";
            BMIStatusLabel.Text = $"BMI 상태: {bmiStatus}";
            SelectedGoalLabel.Text = $"선택된 목표: {selectedGoal}";
            CarbLabel.Text = $"탄수화물(g): {carbIntake:F2}";
            ProteinLabel.Text = $"단백질(g): {proteinIntake:F2}";
            FatLabel.Text = $"지방(g): {fatIntake:F2}";

            // 전달받은 BMI와 BMR 값을 저장합니다.
            BMI = bmi;
            BMR = bmr;
            Age = age;
            Height = height;
            Weight = weight;
            BMStatus = bmiStatus;
            SelectedGoal = selectedGoal;
            CarbIntake = carbIntake;
            ProteinIntake = proteinIntake;
            FatIntake = fatIntake;
        }

        public FirstPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var email = UserNewEmail.Text;
            var password = UserNewPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("알림", "이메일과 비밀번호를 모두 입력하세요.", "확인");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyArevEEm7iw4hZEHN7WDWRQKNOCM02oPeA"));
                var authResult = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                if (authResult != null && authResult.User != null)
                {
                    var firebase = new FirebaseClient("https://login-fba75-default-rtdb.firebaseio.com/");

                    var userInformation = new Dictionary<string, object>
                    {
                        { "Name", NameEntry.Text },
                        { "UserNewEmail", email },
                        { "UserNewPassword", password },
                        { "Age", Age.ToString() },
                        { "Height", Height.ToString() },
                        { "Weight", Weight.ToString() },
                        { "BMI", BMI.ToString() },
                        { "BMR", BMR.ToString() },
                        { "BMIStatus", BMStatus },
                        { "SelectedGoal", SelectedGoal },
                        { "CarbIntake", CarbIntake.ToString() },
                        { "ProteinIntake", ProteinIntake.ToString() },
                        { "FatIntake", FatIntake.ToString() }
                    };

                    // 현재 로그인한 사용자의 ID를 가져와서 해당 사용자의 정보를 users 노드에 저장
                    var userId = authResult.User.LocalId;
                    await firebase.Child("users").Child(userId).Child("UserInformation").PutAsync(userInformation);

                    // OnePage에서 추가한 사용자 정보를 Firebase에 저장하는 부분 추가
                    var onePageFirebase = new Dictionary<string, object>
                    {
                        { "Age", Age },
                        { "Height", Height },
                        { "Weight", Weight },
                        { "BMI", BMI },
                        { "BMR", BMR },
                        { "BMIStatus", BMStatus },
                        { "SelectedGoal", SelectedGoal }
                    };

                    // 두 번째 페이지로 이동
                    await Navigation.PushAsync(new SecondPage(userInformation));
                }
            }
            catch (Exception ex)
            {
                // 사용자 생성 중 오류가 발생한 경우 처리
                await DisplayAlert("알림", $"오류 발생: {ex.Message}", "확인");
            }
        }
    }
}
