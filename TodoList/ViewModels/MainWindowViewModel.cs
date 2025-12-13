
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.DataStorage;
using TodoList.Dialog;


namespace TodoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDialogProvider
{ 
    //DB
    private readonly DatabaseFactory _databaseFactory;
    private readonly DialogService _dialogService;

    
    //Categories
    public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>();
    
    [ObservableProperty]
    private CategoryViewModel? _selectedCategory;

    //Dialog
    [ObservableProperty] private DialogViewModel _dialog;

    //Pages(list, calendar, Kanbanborad)
    [ObservableProperty] private ViewModelBase _currentPage;

    private readonly ListViewModel _listViewPage;

    /// <summary>
    /// Design-time only constructor
    /// </summary>
#pragma warning disable CS8618, CS9264
    public MainWindowViewModel()
    {
        if (Design.IsDesignMode)
        {
            _databaseFactory = new DatabaseFactory(() => new DatabaseService(new ApplicationDbContext()));
            _dialogService = new DialogService();
            Categories =
            [
                new CategoryViewModel { ColorHex = "#D0FFB5", Title = "New Category" },
                new CategoryViewModel { ColorHex = "#FE6366", Title = "Edit Category" }
            ];

            _listViewPage = new ListViewModel();
            SelectedCategory = Categories[0];
            CurrentPage = _listViewPage;
        }


    }
#pragma warning restore CS8618, CS9264

    public MainWindowViewModel(DatabaseFactory databaseFactory, DialogService dialogService )
    {
        _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        
        using var dbContext = _databaseFactory.GetDatabaseService();
        dbContext.ApplyMigration();
        Categories.Clear();
        LoadCategories();
        SelectedCategory = Categories[0];
        
        _listViewPage = new ListViewModel(databaseFactory, SelectedCategory.GetCategory());
        CurrentPage = _listViewPage;
    }
    

    private void LoadCategories()
    {
        
        using var dbContext = _databaseFactory.GetDatabaseService();
        var list = dbContext.GetCategories();
        Categories.Clear();
        foreach (var c in list)
        {
            Categories.Add(new CategoryViewModel(c));
        }
      
    }
    
    [RelayCommand]
    private async Task AddCategory()
    {
        Dialog = new AddCategoryDialogViewModel(_databaseFactory);
        await _dialogService.ShowDialog(this, Dialog);
        
      //  await Dialog.WaitAsync();

        // if (result is not null)
        // {
        //     Categories.Add(new CategoryViewModel());
        // }

        LoadCategories();
    }

    partial void OnSelectedCategoryChanged(CategoryViewModel? value)
    {
        
        if (value is not null)
            CurrentPage = new ListViewModel(value.GetCategory());

    }

    
}