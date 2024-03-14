using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace App1
{
    public partial class FourthPage : ContentPage
    {
        public FourthPage(Dictionary<string, object> userInformation)
        {
            InitializeComponent();
        }

        private async void OnCompleteRegistrationClicked(object sender, EventArgs e)
        {
            // 회원가입 완료 다이얼로그 표시
            bool result = await DisplayAlert("회원가입 완료", "회원가입이 완료되었습니다. 선택한 목표와 BMI에 맞는 운동 루틴을 확인하세요.", "예", "아니오");

            // 예를 선택하면 HomePage로 이동
            if (result)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }
    }
}
