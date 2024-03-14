using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1
{
    public partial class SecondPage : ContentPage
    {
        private Dictionary<string, object> userInformation;
        private int totalCheckBoxes = 10;
        private int checkedCheckBoxes = 0;

        public SecondPage(Dictionary<string, object> userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation; // userInformation 변수 초기화

            // mainStackLayout에서 CheckBox 항목들을 찾아 CheckedChanged 이벤트 핸들러 등록
            foreach (View view in mainStackLayout.Children)
            {
                if (view is StackLayout stackLayout)
                {
                    foreach (View innerView in stackLayout.Children)
                    {
                        if (innerView is CheckBox checkBox)
                        {
                            checkBox.CheckedChanged += CheckBox_CheckedChanged;
                        }
                    }
                }
            }

            // 초기 체크박스 수 업데이트
            UpdateCheckBoxCount();
        }

        // 체크박스 상태가 변경될 때 호출되는 이벤트 핸들러
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;

            if (e.Value) // 체크박스가 체크되었을 때
            {
                checkedCheckBoxes++;
            }
            else // 체크박스가 체크 해제되었을 때
            {
                checkedCheckBoxes--;
            }

            // 변경된 체크박스 수 업데이트
            UpdateCheckBoxCount();
        }

        // 체크박스 수를 업데이트하고 화면에 표시하는 메서드
        private void UpdateCheckBoxCount()
        {
            // 전체 체크박스 수 표시
            totalCheckBoxLabel.Text = $"전체 체크박스: {totalCheckBoxes}";

            // 체크된 체크박스 수 표시
            checkedCheckBoxLabel.Text = $"체크된 체크박스: {checkedCheckBoxes}";
        }

        private async void OnRegistrationCompleteButtonClicked(object sender, EventArgs e)
        {
            // ThirdPage로 이동할 때 userInformation 변수를 전달
            await Navigation.PushAsync(new ThirdPage(userInformation));
        }
    }
}
