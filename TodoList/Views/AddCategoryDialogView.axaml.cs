using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList.Controls;
using TodoList.ViewModels;

namespace TodoList.Views;

public partial class AddCategoryDialogView : UserControl
{
    public AddCategoryDialogView()
    {
        InitializeComponent();
    }

    private void OnColorSelected(object? sender, RoutedEventArgs e)
    {
        if (sender is ColorRadioButton rb && DataContext is AddCategoryDialogViewModel vm)
        {
            vm.ColorHex  = rb?.ColorBrush.ToString();
        }
        
    }
}
