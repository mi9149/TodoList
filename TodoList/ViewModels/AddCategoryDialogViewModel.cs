using System.Runtime.InteropServices.JavaScript;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;

namespace TodoList.ViewModels;



/// <summary>
/// Category 추가를 위한 Dialog 창 ('+' 버튼 클릭시 뜨는 창)
/// </summary>
public partial class AddCategoryDialogViewModel : DialogViewModel<Category>
{
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    private string _title = string.Empty;
    [ObservableProperty] private string _colorHex = "#FFFFFF";
    [ObservableProperty] private string _confirmText = "Confirm";
    [ObservableProperty] private string _cancelText = "Cancel";
    

    [ObservableProperty] private bool _confirmed;


    //private string? _newCategoryName;
    
    

    private bool CanAddCategory() => !string.IsNullOrWhiteSpace(Title);
    
    [RelayCommand(CanExecute = nameof(CanAddCategory))]
    public void Confirm()
    {
        var newCategory =  GetCategory();
        Confirmed = true;
        Close(newCategory);
    }
    
    
    [RelayCommand]
    public void Cancel()
    {
        Confirmed = false;
        Close(null);
    }

    public Category GetCategory()
    {
        return new Category()
        {
            Title = this.Title,
            ColorHex = this.ColorHex
        };
    }
    
    
    


}