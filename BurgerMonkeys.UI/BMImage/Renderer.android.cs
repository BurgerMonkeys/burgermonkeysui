using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using BurgerMonkeys.UI.Controls;
using BurgerMonkeys.UI.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BMImage), typeof(BMImageRenderer))]
namespace BurgerMonkeys.UI.Droid
{
    public class BMImageRenderer : ImageRenderer
    {
        public BMImageRenderer(Context context) : base(context)
        {
        }

        public static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            SetColorTint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == BMImage.TintColorProperty.PropertyName || e.PropertyName == Image.SourceProperty.PropertyName)
                SetColorTint();
        }

        void SetColorTint()
        {
            if (Control == null || Element == null)
                return;

            if (((BMImage)Element).TintColor.Equals(Xamarin.Forms.Color.Transparent))
            {
                if (Control.ColorFilter != null)
                    Control.ClearColorFilter();

                return;
            }

            var colorFilter = new PorterDuffColorFilter(((BMImage)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            Control.SetColorFilter(colorFilter);
        }
    }
}
