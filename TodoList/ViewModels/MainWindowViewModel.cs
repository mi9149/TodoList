using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;
using TodoList.Views;

namespace TodoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{ 
    public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>();

    [ObservableProperty] 
    private DialogViewModel<Category> _currentDialog = new AddCategoryDialogViewModel();

    [RelayCommand]
    public async Task AddCategory()
    {
        CurrentDialog.Show();
        var result = await CurrentDialog.WaitAsnyc();

        if (result is not null)
        {
            Categories.Add(new CategoryViewModel(result));
        }
    }

}