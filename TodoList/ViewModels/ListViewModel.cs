using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.DataStorage;
using TodoList.DataStorage.DataModels;
using TodoList.Models;
using TodoList.Views;

namespace TodoList.ViewModels;

public partial class ListViewModel:ViewModelBase
{
    
    private readonly DatabaseFactory _databaseFactory;

    private static string CategoryId;
    

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddTodoItemCommand))]
    private string? _newTodoItemTitle;
    
    public ObservableCollection<ListItemViewModel>TodoItems { get; } = [];

    public ListViewModel(): this(new DatabaseFactory(()=>new DatabaseService(new ApplicationDbContext())), new CategoryDataModel()) 
    {
    }

    public ListViewModel(CategoryDataModel category) : this(
        new DatabaseFactory(() => new DatabaseService(new ApplicationDbContext())), category)
    {
    }
    
    public ListViewModel(DatabaseFactory databaseFactory, CategoryDataModel category )
    {
        CategoryId = category.Id;
        _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        LoadTodoItems(CategoryId);
    }
    
    private bool CanAddTodoItem() => !string.IsNullOrWhiteSpace(NewTodoItemTitle);

    private void LoadTodoItems(string categoryId)
    {
        using var dbContext = _databaseFactory.GetDatabaseService();
        var list = dbContext.GetTodoItems(categoryId);
        TodoItems.Clear();
        foreach (var c in list)
        {
            TodoItems.Add(new ListItemViewModel(c));
        }

    }

    [RelayCommand(CanExecute = nameof(CanAddTodoItem))]
    public Task AddTodoItem()
    {
        using var dbContext = _databaseFactory.GetDatabaseService();
        var newTodoItem = new ListItemViewModel()
            { Title = NewTodoItemTitle, IsChecked = false, CategoryId =CategoryId };
        
        TodoItems.Add(newTodoItem);
        dbContext.SaveTodoItems(newTodoItem.GetTodoItem());
        return Task.CompletedTask;
    }
    

    [RelayCommand]
    private void RemoveTodoItem(ListItemViewModel item)
    {
        using var dbContext = _databaseFactory.GetDatabaseService();
        TodoItems.Remove(item);
        dbContext.RemoveTodoItem(item.GetTodoItem());
    }
    
}