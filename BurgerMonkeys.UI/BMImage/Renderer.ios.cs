using System;
using System.Linq;
using BurgerMonkeys.UI.Controls;
using BurgerMonkeys.UI.iOS;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BMImage), typeof(BMImageRenderer))]
namespace BurgerMonkeys.UI.iOS
{
    public class BMImageRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;

            SetColorTint();
            RoundCorners();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                SetColorTint();
                RoundCorners();
            }

            if (e.PropertyName == BMImage.TintColorProperty.PropertyName)
                SetColorTint();


            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == BMImage.BorderColorProperty.PropertyName ||
                e.PropertyName == BMImage.BorderSizeProperty.PropertyName ||
                e.PropertyName == BMImage.IsCircleProperty.PropertyName)
                RoundCorners();
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

        void RoundCorners()
        {
            string borderName = "borderLayerName";
            var bmImage = (BMImage)Element;
            var isCircle = bmImage.IsCircle;
            var borderSize = bmImage.BorderSize;
            var backgroundColor = bmImage.BackgroundColor;
            var borderColor = bmImage.BorderColor;
            var min = Math.Min(Element.Width, Element.Height);
            var radius = (nfloat)(isCircle ? (min / 2.0) : 0);

            try
            {
                Control.Layer.CornerRadius = radius;
                Control.Layer.MasksToBounds = false;
                Control.BackgroundColor = backgroundColor.ToUIColor();
                Control.ClipsToBounds = true;

                var tempLayer = Control.Layer.Sublayers?
                                       .Where(p => p.Name == borderName)
                                       .FirstOrDefault();
                tempLayer?.RemoveFromSuperLayer();

                var externalBorder = new CALayer
                {
                    Name = borderName,
                    CornerRadius = radius,
                    Frame = new CGRect(-.5, -.5, min + 1, min + 1),
                    BorderColor = borderColor.ToCGColor(),
                    BorderWidth = borderSize
                };

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}
