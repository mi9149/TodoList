using System.Runtime.InteropServices.JavaScript;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.DataStorage;
using TodoList.DataStorage.DataModels;
using TodoList.Models;

namespace TodoList.ViewModels;



/// <summary>
/// Category 추가를 위한 Dialog 창 ('+' 버튼 클릭시 뜨는 창)
/// </summary>
public partial class AddCategoryDialogViewModel : DialogViewModel<CategoryDataModel>
{
    private readonly DatabaseFactory _factory;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    private string _title = string.Empty;
    
    [ObservableProperty] private string _colorHex = "#FFFFFF";
    
    [ObservableProperty] private string _confirmText = "Confirm";
    [ObservableProperty] private string _cancelText = "Cancel";
    

    [ObservableProperty] private bool _confirmed;


    //private string? _newCategoryName;

    public AddCategoryDialogViewModel() : this(new DatabaseFactory(() => new DatabaseService(new ApplicationDbContext())))
    { }

    public AddCategoryDialogViewModel(DatabaseFactory factory)
    {
        _factory = factory;
        
    }

    private bool CanAddCategory() => !string.IsNullOrWhiteSpace(Title);
    
    [RelayCommand(CanExecute = nameof(CanAddCategory))]
    public void Confirm()
    {
        using var dbContext = _factory.GetDatabaseService();
        var newCategory = ToDataModel();
        dbContext.SaveCategories(newCategory);
        Confirmed = true;
        Close(newCategory);
    }
    
    
    [RelayCommand]
    public void Cancel()
    {
        Confirmed = false;
        Close(null);
    }
    
    private CategoryDataModel ToDataModel() => new()
    { 
        Title = Title,
        ColorHex = ColorHex
    };

    
    
    


}