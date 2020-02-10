using Xamarin.Forms;

namespace BurgerMonkeys.UI.Controls
{
    public class BMImage : Image
    {
        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create(nameof(TintColor),
                typeof(Color),
                typeof(BMImage),
                Color.Black);

        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }

		public static readonly BindableProperty BorderSizeProperty =
		  BindableProperty.Create(nameof(BorderSize),
			  typeof(float),
			  typeof(BMImage),
			  0F);

		public float BorderSize
		{
			get { return (float)GetValue(BorderSizeProperty); }
			set { SetValue(BorderSizeProperty, value); }
		}

		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor),
			  typeof(Color),
			  typeof(BMImage),
			  Color.White);

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		public static readonly BindableProperty IsCircleProperty =
			BindableProperty.Create(nameof(IsCircle),
			  typeof(bool),
			  typeof(BMImage),
			  false);

		public bool IsCircle
		{
			get { return (bool)GetValue(IsCircleProperty); }
			set { SetValue(IsCircleProperty, value); }
		}
	}
}
