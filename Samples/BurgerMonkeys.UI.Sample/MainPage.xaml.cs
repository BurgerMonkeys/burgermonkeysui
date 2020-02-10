using System.ComponentModel;
using Xamarin.Forms;

namespace BurgerMonkeys.UI.Sample
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            swtIsCircle.IsToggled = false;
            sizeBorderSlider.Value = 5;
        }

        void UpdateTint()
        {
            if (redSlider == null || greenSlider == null || blueSlider == null)
                return;

            bmImage.TintColor = Color.FromRgb((int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value);
        }

        void UpdateBorderColor()
        {
            if (redBorderSlider == null || greenBorderSlider == null || blueBorderSlider == null)
                return;
            
            bmImage.BorderColor = Color.FromRgb((int)redBorderSlider.Value, (int)greenBorderSlider.Value, (int)blueBorderSlider.Value);
            bmImage.BorderSize = (int)sizeBorderSlider.Value;
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateTint();
        }

        void OnSliderBorderColorValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateBorderColor();
        }

        void OnSliderBorderSizeValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateBorderColor();
        }

        void IsCircleToggled(object sender, ToggledEventArgs e)
        {
            bmImage.IsCircle = swtIsCircle.IsToggled;
            //frmBorderColor.IsVisible = swtIsCircle.IsToggled;
            //lblBorder.IsVisible = swtIsCircle.IsToggled;
        }
    }
}
