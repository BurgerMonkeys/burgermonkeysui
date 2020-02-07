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
        }

        void UpdateTint()
        {
            if (redSlider == null || greenSlider == null || blueSlider == null)
                return;

            bmImage.TintColor = Color.FromRgb((int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value);
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateTint();
        }
    }
}
