using System.Drawing;
using Avalonia.Controls.Chrome;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.DataStorage;
using TodoList.Models;

namespace TodoList.ViewModels;


public partial class CategoryViewModel:ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _colorHex;
}