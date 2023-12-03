# .NET 8 Custom Image Button

Your comment indicates that you had some problems with the gesture recognizer with a custom control. I wanted to offer an alternative to using a `TapGestureRecognizer` as shown in the answer that I linked. My personal spin would be to use an actual button as the bottom layer which gives you the benefit of having both `Clicked` and `Pressed` events. Then just overlay an `Image` and a `Label` on top of the button. 

The `Label` and `Image` controls are probably not interested in intercepting button gestures, but I set `InputTransparent="True"` out of an abundance of caution. The internal controls will bind to both standard properties of `Frame` (e.g. the button will track `Frame.CornerRadius`) but we can also introduce custom bindable properties in the code behind such as `ImageMargin` and `ImageSource` that a frame doesn't ordinarily expose.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<Frame 
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:controls="clr-namespace:net_8_image_buttons.Controls"
    x:Class="net_8_image_buttons.Controls.ButtonEx"
    Padding="0"
    CornerRadius="10">
    <Grid
        ColumnDefinitions="Auto,*">
        <!--Pull properties down from the wrapper for individual controls-->
        <Button         
            x:Name="GestureRecognizerEx"
            Grid.ColumnSpan="2"
            HorizontalOptions="Fill"
            VerticalOptions="Fill"
            BackgroundColor="Transparent"
            CornerRadius="{Binding Path=CornerRadius, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"
            BorderColor="{Binding Path=BorderColor, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"
            BorderWidth="{Binding Path=BorderWidth, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"/>
        <Image 
            WidthRequest="50"
            Source="{Binding Path=ImageSource, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"
            Margin="{Binding Path=ImageMargin, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"
            InputTransparent="True"/>   
        <Label         
            Grid.Column="0"
            Grid.ColumnSpan="2"
            Text="{Binding Path=Text, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"         
            TextColor="{Binding Path=TextColor, Source={RelativeSource AncestorType={x:Type controls:ButtonEx}}}"        
            VerticalOptions="Center" 
            HorizontalOptions="Center"
            InputTransparent="True"/>
    </Grid>
</Frame>
```

 ___
    
 **Custom Bindable Properties**
    
You can make the `ButtonEx` behave more (or less) like  a `Button` depending on the custom properties you choose to expose.
    
```csharp
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
```


