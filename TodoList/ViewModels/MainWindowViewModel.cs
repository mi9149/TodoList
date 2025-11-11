
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
    private readonly DatabaseFactory _databaseFactory;
    private readonly DatabaseService _dbContext;
    public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>();

    [ObservableProperty] 
    private DialogViewModel<CategoryDataModel> _currentDialog = new AddCategoryDialogViewModel();

    public MainWindowViewModel()
    {
        _databaseFactory = new DatabaseFactory(() => new DatabaseService(new ApplicationDbContext()));
        _dbContext = _databaseFactory.GetDatabaseService();
        _dbContext.ApplyMigration();
        LoadCategories();
    }

    private void LoadCategories()
    {
        
        var list = _dbContext.GetCategories();
        Categories.Clear();
        foreach (var c in list)
        {
            Categories.Add(new CategoryViewModel
            {
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
}