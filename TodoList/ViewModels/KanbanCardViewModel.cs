using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class KanbanCardViewModel:ViewModelBase
{
    [ObservableProperty] private string _content; //Required
    [ObservableProperty] private DateTime? _dueDate; //if null -> invisible

    public KanbanCardViewModel(TodoItem item)
    {
        Content =  item.Content;
        DueDate = item.DueDate;
    }
     
    public bool IsOverDue(DateTime d)
    {
        return d.Date < DateTime.Today;
    }
}