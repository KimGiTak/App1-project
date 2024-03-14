using App1;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // 기존의 토큰 값을 확인하는 부분을 주석 처리하거나 삭제합니다.
            // if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            // {
            //     MainPage = new NavigationPage(new MainPage());
            // }
            // else
            // {
            //     //MainPage = new NavigationPage(new HomePage());
            // }

            // 토큰 값이 없으므로 기본 페이지로 MainPage를 설정합니다.
            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }
    }
}