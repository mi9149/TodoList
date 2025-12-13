using System.Drawing;
using Avalonia.Controls.Chrome;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.DataStorage;
using TodoList.DataStorage.DataModels;
using TodoList.Models;

namespace TodoList.ViewModels;


public partial class CategoryViewModel:ViewModelBase
{
    private string Id {get; }
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _colorHex;

    /// <summary>
    /// Design-time only constructor
    /// </summary>
#pragma warning disable CS8618, CS9264
    public CategoryViewModel()
    {
    }
#pragma warning restore CS8618, CS9264

    public CategoryViewModel(CategoryDataModel categoryDataModel)
    {
        Id = categoryDataModel.Id; 
        Title = categoryDataModel.Title;
        ColorHex = categoryDataModel.ColorHex;
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