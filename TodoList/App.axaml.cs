using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TodoList.DataStorage;
using TodoList.Dialog;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // 프로그램이 시작될 때 어떤 객체를 “어떻게 생성하고, 언제까지 유지할지” 
        var collection = new ServiceCollection();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<ApplicationDbContext>();
        collection.AddTransient<DatabaseService>();
        collection.AddSingleton<Func<DatabaseService>>(serviceProvider =>serviceProvider.GetRequiredService<DatabaseService>);
        collection.AddSingleton<DatabaseFactory>();

        collection.AddSingleton<DialogService>();

        var service = collection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext =  service.GetRequiredService<MainWindowViewModel>()
            };
        }
        
        base.OnFrameworkInitializationCompleted();
       
    }
    

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

}