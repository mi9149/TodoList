using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.DataStorage.DataModels;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class ListItemViewModel:ViewModelBase
{
    
    
    [ObservableProperty] private string _title;
    [ObservableProperty] private bool _isChecked;
    public string CategoryId {get;set;}
    public string Id { get; set; }
    

    public ListItemViewModel()
    {
        Id =  Guid.NewGuid().ToString("N");
    }

    public ListItemViewModel(TodoItemsDataModel todoItem)
    {
        Id = todoItem.Id;
        Title = todoItem.Title;
        IsChecked = todoItem.Completed;
        CategoryId = todoItem.CategoryID;
    }

    public TodoItemsDataModel GetTodoItem()
    {
        return new TodoItemsDataModel()
        {
            Id = Id,
            Title = this.Title,
            Completed = this.IsChecked,
            CategoryID = this.CategoryId
        };
    }
        
    
}