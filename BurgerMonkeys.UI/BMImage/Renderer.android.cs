using System;
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

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            SetColorTint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Image.SourceProperty.PropertyName ||
                e.PropertyName == BMImage.TintColorProperty.PropertyName ||
                e.PropertyName == BMImage.BorderColorProperty.PropertyName ||
                e.PropertyName == BMImage.BorderSizeProperty.PropertyName ||
                e.PropertyName == BMImage.IsCircleProperty.PropertyName)
            {
                Invalidate();
            }
        }

        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            SetColorTint();
            var isCircle = ((BMImage)Element).IsCircle;
            var borderSize = ((BMImage)Element).BorderSize;
            var borderColor = ((BMImage)Element).BorderColor.ToAndroid();

            try
            {
                var strokeWidth = 0f;
                if (borderSize > 0)
                {
                    var logicalDensity = Android.App.Application.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (float)Math.Ceiling(borderSize * logicalDensity + .5f);
                }

                var radius = 0f;
                var path = new Path();
                var result = true;
                if (isCircle)
                {
                    radius = Math.Min(Width, Height) / 2f;
                    radius -= strokeWidth / 2f;
                    path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);
                    canvas.Save();
                    canvas.ClipPath(path);

                    result = base.DrawChild(canvas, child, drawingTime);
                    path.Dispose();
                    canvas.Restore();

                    path = new Path();
                    path.AddCircle(Width / 2f, Height / 2f, radius, Path.Direction.Ccw);
                }
                else
                {
                    result = base.DrawChild(canvas, child, drawingTime);
                    path = new Path();
                    path.AddRect(0f, 0f, Width, Height, Path.Direction.Ccw);
                }

                if (strokeWidth > 0.0f)
                {
                    var paint = new Paint
                    {
                        AntiAlias = true,
                        StrokeWidth = strokeWidth
                    };
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = borderColor;
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }
            return base.DrawChild(canvas, child, drawingTime);
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
