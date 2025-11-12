using System.Drawing;
using Avalonia.Controls.Chrome;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.DataStorage;
using TodoList.DataStorage.DataModels;
using TodoList.Models;

namespace TodoList.ViewModels;


public partial class CategoryViewModel:ViewModelBase
{
    public string Id {get; set;}
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _colorHex;

    public CategoryViewModel()
    {
    }

    public CategoryViewModel(CategoryDataModel categoryDataModel)
    {
        Id = categoryDataModel.Id;
        _title = categoryDataModel.Title;
        _colorHex = categoryDataModel.ColorHex;
    }

    public CategoryDataModel GetCategory()
    {
        return new CategoryDataModel()
        {
            Id = this.Id,
            Title = this.Title,
            ColorHex = this.ColorHex
        };
    }
    
    
}