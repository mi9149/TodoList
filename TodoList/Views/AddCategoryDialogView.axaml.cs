using Avalonia.Controls;
using Avalonia.Interactivity;
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
        if (sender is RadioButton rb && DataContext is AddCategoryDialogViewModel vm)
        {
            vm.ColorHex  = rb?.Foreground.ToString();
        }
        
    }
}
