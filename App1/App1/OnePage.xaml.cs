using System;
using Xamarin.Forms;

namespace App1
{
    public partial class OnePage : ContentPage
    {
        public OnePage()
        {
            InitializeComponent();
        }

        private async void CalculateButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AgeEntry.Text) &&
                !string.IsNullOrWhiteSpace(HeightEntry.Text) &&
                !string.IsNullOrWhiteSpace(WeightEntry.Text) &&
                !string.IsNullOrWhiteSpace(GenderPicker.SelectedItem.ToString()))
            {
                // 입력 값 가져오기
                int age = int.Parse(AgeEntry.Text);
                double height = double.Parse(HeightEntry.Text) / 100;
                double weight = double.Parse(WeightEntry.Text);
                string gender = GenderPicker.SelectedItem.ToString();

                // BMI 계산
                double bmi = CalculateBMI(weight, height);

                // BMI 상태 가져오기
                string bmiStatus = GetBMIStatus(bmi);

                // 기초대사량 계산
                double bmr = CalculateBMR(age, weight, height, gender);

                // 결과 출력
                BMILabel.Text = $"BMI: {bmi:F2} ({bmiStatus})";
                BMRLabel.Text = $"BMR: {bmr:F2} kcal/day";

                // 목표 선택에 따른 권장 메시지 가져오기
                string selectedGoal = GoalPicker.SelectedItem.ToString();
                string recommendationMessage = GetRecommendationMessage(selectedGoal);
                BMIStatusLabel.Text = recommendationMessage;

                double carbIntake, proteinIntake, fatIntake;
                CalculateCarbProteinFat(bmr, out carbIntake, out proteinIntake, out fatIntake);
                CarbLabel.Text = $"탄수화물: {carbIntake:F2}g";
                ProteinLabel.Text = $"단백질: {proteinIntake:F2}g";
                FatLabel.Text = $"지방: {fatIntake:F2}g";
            }
            else
            {
                await DisplayAlert("알림", "나이, 키, 몸무게, 성별을 모두 입력하세요.", "확인");
            }
        }

        private double CalculateBMI(double weight, double height)
        {
            // BMI 계산
            return weight / (height * height);
        }

        private double CalculateBMR(int age, double weight, double height, string gender)
        {
            // 기초대사량 계산식
            double bmr;
            if (gender.Equals("남성"))
            {
                bmr = (13.397 * weight) + (4.799 * height) - (5.677 * age) + 88.362;
            }
            else // 여성
            {
                bmr = (9.247 * weight) +(3.098 * height) - (4.330 * age) + 447.593;
            }
            return bmr;
        }

        private string GetBMIStatus(double bmi)
        {
            if (bmi < 18.5)
            {
                return "저체중";
            }
            else if (bmi < 25)
            {
                return "정상";
            }
            else if (bmi < 30)
            {
                return "과체중";
            }
            else
            {
                return "비만";
            }
        }

        private string GetRecommendationMessage(string selectedGoal)
        {
            switch (selectedGoal)
            {
                case "근성장":
                    return "몸의 부피를 늘리기 위해서는 많은 영양분 섭취와 고중량의 운동이 필요합니다. 목표에 맞는 루틴을 추천해드릴게요!";
                case "근성장 및 다이어트":
                    return "근성장과 다이어트에는 규칙적인 운동과 올바른 식습관이 필요합니다. 목표에 맞는 루틴을 추천해드릴게요!";
                case "체지방 감소(다이어트)":
                    return "근육량 감소 없이 체지방 감량이 가능한 운동 루틴을 추천할게요!";
                case "해당없음":
                    // BMI에 따른 운동법을 추천하는 문구 추가
                    double weight = double.Parse(WeightEntry.Text);
                    double height = double.Parse(HeightEntry.Text) / 100;
                    double bmi = CalculateBMI(weight, height);
                    return $"BMI에 따른 운동법을 추천합니다. (현재 BMI: {bmi:F2})";
                default:
                    return "";
            }
        }

        private void CalculateCarbProteinFat(double bmr, out double carbIntake, out double proteinIntake, out double fatIntake)
        {
            //탄단지는 4:4:2 비율로 넣음
            double carbRatio = 0.40;
            double proteinRatio = 0.40;
            double fatRatio = 0.20;

            // 탄,단은 1g당 4kcal, 지방은 1g당 9kcal
            carbIntake = bmr * carbRatio / 4;
            proteinIntake = bmr * proteinRatio / 4;
            fatIntake = bmr * fatRatio / 9;
            CarbLabel.Text = $"탄수화물: {carbIntake:F2}g";
            ProteinLabel.Text = $"단백질: {proteinIntake:F2}g";
            FatLabel.Text = $"지방: {fatIntake:F2}g";
        }

        private (double carbIntake, double proteinIntake, double fatIntake) CalculateCarbProteinFat(double bmr)
        {
            //탄단지는 4:4:2 비율로 넣음
            double carbRatio = 0.40;
            double proteinRatio = 0.40;
            double fatRatio = 0.20;

            // 탄,단은 1g당 4kcal, 지방은 1g당 9kcal
            double carbIntake = bmr * carbRatio / 4;
            double proteinIntake = bmr * proteinRatio / 4;
            double fatIntake = bmr * fatRatio / 9;

            return (carbIntake, proteinIntake, fatIntake);
        }

        private async void GoToFirstPageButton_Clicked(object sender, EventArgs e)
        {
            // 입력된 값들 가져오기
            int age = int.Parse(AgeEntry.Text);
            double height = double.Parse(HeightEntry.Text) / 100;
            double weight = double.Parse(WeightEntry.Text);
            double bmi = CalculateBMI(weight, height);
            string gender = GenderPicker.SelectedItem.ToString();
            double bmr = CalculateBMR(age, weight, height, gender);
            string bmiStatus = GetBMIStatus(bmi);
            string selectedGoal = GoalPicker.SelectedItem.ToString();

            // 탄단지 섭취량 계산
            (double carbIntake, double proteinIntake, double fatIntake) = CalculateCarbProteinFat(bmr);

            // FirstPage로 이동하면서 BMI, BMR 및 탄단지 결과값을 전달
            await Navigation.PushAsync(new FirstPage(age, height, weight, bmi, bmr, bmiStatus, selectedGoal, carbIntake, proteinIntake, fatIntake));

            // UI에 결과 표시
            CarbLabel.Text = $"탄수화물: {carbIntake:F2}g";
            ProteinLabel.Text = $"단백질: {proteinIntake:F2}g";
            FatLabel.Text = $"지방: {fatIntake:F2}g";
        }
    }
}
