using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace TodoList.Controls;

public class ColorRadioButton :RadioButton
{
    public static readonly StyledProperty<IBrush?> ColorBrushProperty =
        AvaloniaProperty.Register<ColorRadioButton, IBrush?>(nameof(ColorBrush));

    public IBrush? ColorBrush
    {
        get => GetValue(ColorBrushProperty);
        set => SetValue(ColorBrushProperty, value);
    }
}