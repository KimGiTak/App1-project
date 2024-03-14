using Xamarin.Forms;
using System.Collections.Generic;

namespace App1
{
    public partial class ThirdPage : ContentPage
    {
        private Dictionary<string, object> userInformation;

        public ThirdPage(Dictionary<string, object> userInformation)
        {
            InitializeComponent();

            this.userInformation = userInformation;

            var locationPicker = new Picker
            {
                Title = "운동 장소 선택",
                Items = { "거주지", "헬스장" }
            };

            var exerciseOptionsPicker = new Picker
            {
                Title = "운동 옵션 선택"
            };

            var intensitySlider = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 50
            };
            var intensityLabel = new Label
            {
                TextColor = Color.White
            };
            intensitySlider.ValueChanged += (sender, e) =>
            {
                intensityLabel.Text = $"운동 강도: {e.NewValue}";
            };

            intensitySlider.ValueChanged += IntensitySlider_ValueChanged;

            var smokingSlider = new Slider
            {
                Minimum = 0,
                Maximum = 7,
                Value = 0
            };
            var smokingLabel = new Label
            {
                TextColor = Color.White
            };
            smokingSlider.ValueChanged += (sender, e) =>
            {
                smokingLabel.Text = $"흡연 정도: {e.NewValue}";
            };

            var drinkingSlider = new Slider
            {
                Minimum = 0,
                Maximum = 7,
                Value = 0
            };
            var drinkingLabel = new Label
            {
                TextColor = Color.White
            };
            drinkingSlider.ValueChanged += (sender, e) =>
            {
                drinkingLabel.Text = $"음주 정도: {e.NewValue}";
            };

            locationPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (locationPicker.SelectedIndex == 0) // 거주지 선택
                {
                    exerciseOptionsPicker.Items.Clear();
                    exerciseOptionsPicker.Items.Add("맨몸운동");
                    exerciseOptionsPicker.Items.Add("운동기구 사용");
                }
                else // 헬스장 선택
                {
                    exerciseOptionsPicker.Items.Clear();
                    exerciseOptionsPicker.Items.Add("헬스장에서 진행하는 운동");
                    exerciseOptionsPicker.Items.Add("추가 선택사항");
                }
            };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "운동 수준 선택" },
                    new Picker { Title = "운동 수준 선택", Items = { "입문", "초급", "중급", "고급" } },
                    new Label { Text = "운동 강도 선택" },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { intensitySlider, intensityLabel }
                    },
                    new Label { Text = "주로 운동하는 장소 선택" },
                    locationPicker,
                    new Label { Text = "운동 옵션 선택" },
                    exerciseOptionsPicker,
                    new Label { Text = "흡연 정도 선택" },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { smokingSlider, smokingLabel }
                    },
                    new Label { Text = "음주 정도 선택" },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { drinkingSlider, drinkingLabel }
                    },
                    new Button { Text = "다음", Command = new Command(OnNextButtonClicked) }
                }
            };
        }

        private void IntensitySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // 여기에 슬라이더 값 변경에 대한 로직을 추가합니다.
        }

        private async void OnNextButtonClicked()
        {
            await Navigation.PushAsync(new FourthPage(userInformation));
        }
    }
}
