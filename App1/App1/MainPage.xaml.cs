using System;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLogPageButtonClicked(object sender, EventArgs e)
        {
            // 페이지 이동 또는 필요한 작업 수행
            Navigation.PushAsync(new LogPage()); // AuthPage는 실제 사용하려는 페이지로 변경
        }

        private void OnOnePageButtonClicked(object sender, EventArgs e)
        {
            // 페이지 이동 또는 필요한 작업 수행
            Navigation.PushAsync(new OnePage()); // AuthPage는 실제 사용하려는 페이지로 변경
        }
    }
}
