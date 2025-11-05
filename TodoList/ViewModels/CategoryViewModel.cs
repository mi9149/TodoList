using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.Models;

namespace TodoList.ViewModels;


public partial class CategoryViewModel:ViewModelBase
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _colorHex;
    public CategoryViewModel(Category category)
    {
        Name = category.Title;
        ColorHex = category.ColorHex;
    }
    
    
    
    public Category GetCategory()
    {
        return new Category()
        {
            Title = this.Name,
            ColorHex = this.ColorHex
        };
    }
}