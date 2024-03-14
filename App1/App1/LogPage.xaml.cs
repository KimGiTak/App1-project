using System;
using Xamarin.Forms;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace App1
{
    public partial class LogPage : ContentPage
    {
        public LogPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("알림", "이메일과 비밀번호를 모두 입력하세요.", "확인");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyArevEEm7iw4hZEHN7WDWRQKNOCM02oPeA"));
                var authResult = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await authResult.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);

                if (authResult != null && !string.IsNullOrEmpty(authResult.User.LocalId))
                {
                    Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                    // 토큰 값을 앱 화면에 표시
                    await DisplayAlert("알림", $"토큰 값: {serializedContent}", "확인");
                    var savedFirebaseAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                    var refreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                    await DisplayAlert("알림", $"로컬ID: {refreshedContent.User.LocalId}", "확인");
                    // 로그인 성공
                    await Navigation.PushAsync(new HomePage());
                    Navigation.RemovePage(this); // 현재 페이지를 스택에서 제거


                }

                else
                {
                    // 로그인 실패
                    await DisplayAlert("알림", "이메일 또는 비밀번호가 일치하지 않습니다.", "확인");
                }
            }
            catch (Exception ex)
            {
                // Firebase 인증 실패
                await DisplayAlert("알림", $"로그인 중 오류가 발생했습니다. ({ex.Message})", "확인");
            }
        }
    }
}