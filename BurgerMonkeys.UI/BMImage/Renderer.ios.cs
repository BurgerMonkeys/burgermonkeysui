using BurgerMonkeys.UI.Controls;
using BurgerMonkeys.UI.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BMImage), typeof(BMImageRenderer))]
namespace BurgerMonkeys.UI.iOS
{
    public class BMImageRenderer : ImageRenderer
    {
        public new static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            SetColorTint();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == BMImage.TintColorProperty.PropertyName || e.PropertyName == Image.SourceProperty.PropertyName)
                SetColorTint();
        }

        void SetColorTint()
        {
            if (Control?.Image == null || Element == null)
                return;

            if (((BMImage)Element).TintColor == Color.Transparent)
            {
                Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
                Control.TintColor = null;
            }
            else
            {
                Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.AlwaysTemplate);
                Control.TintColor = ((BMImage)Element).TintColor.ToUIColor();
            }
        }
    }
}
