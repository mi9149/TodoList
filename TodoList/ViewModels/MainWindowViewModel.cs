
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using TodoList.DataStorage;
using TodoList.DataStorage.DataModels;


namespace TodoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{ 
    //DB
    private readonly DatabaseFactory _databaseFactory;

    
    //Categories
    public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>();
    
    [ObservableProperty]
    private CategoryViewModel? _selectedCategory;
    

    [ObservableProperty] 
    private DialogViewModel<CategoryDataModel> _currentDialog = new AddCategoryDialogViewModel();

    //Pages(list, calendar, Kanbanborad)
    [ObservableProperty] private ViewModelBase _currentPage;

    private readonly ListViewModel _listViewPage;

    public MainWindowViewModel()
    {
        _databaseFactory = new DatabaseFactory(() => new DatabaseService(new ApplicationDbContext()));
        
        using var dbContext = _databaseFactory.GetDatabaseService();
        dbContext.ApplyMigration();
        Categories.Clear();
        LoadCategories();
        SelectedCategory = Categories[0];
        
        _listViewPage = new ListViewModel(SelectedCategory.GetCategory());
        CurrentPage = _listViewPage;
    }

    private void LoadCategories()
    {
        
        using var dbContext = _databaseFactory.GetDatabaseService();
        var list = dbContext.GetCategories();
        Categories.Clear();
        foreach (var c in list)
        {
            Categories.Add(new CategoryViewModel
            {
                Id = c.Id,
                Title = c.Title,
                ColorHex = c.ColorHex
            });
        }
      
    }
    
    [RelayCommand]
    public async Task AddCategory()
    {
        CurrentDialog.Show();
        var result = await CurrentDialog.WaitAsync();

        if (result is not null)
        {
            Categories.Add(new CategoryViewModel());
        }

        LoadCategories();
    }

    partial void OnSelectedCategoryChanged(CategoryViewModel? value)
    {
        
        if (value is not null)
            CurrentPage = new ListViewModel(value.GetCategory());

    }
}