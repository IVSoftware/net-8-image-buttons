using Microsoft.Maui.Controls;

namespace net_8_image_buttons.Controls;

public partial class ButtonEx : Frame
{
	public ButtonEx()
	{
		InitializeComponent();
        GestureRecognizerEx.Clicked += (sender, e) => Clicked?.Invoke(this, e);
        GestureRecognizerEx.Pressed += (sender, e) => Pressed?.Invoke(this, e);
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ButtonEx), default(string));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ButtonEx), default(ImageSource)); 
    
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ButtonEx), default(Color));

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ButtonEx), default(double));

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageMarginProperty =
        BindableProperty.Create(nameof(ImageMargin), typeof(Thickness), typeof(ButtonEx), default(Thickness));

    public Thickness ImageMargin
    {
        get => (Thickness)GetValue(ImageMarginProperty);
        set => SetValue(ImageMarginProperty, value);
    }
    public event EventHandler? Clicked;
    public event EventHandler? Pressed;
}