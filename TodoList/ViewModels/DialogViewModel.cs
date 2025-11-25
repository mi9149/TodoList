using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class DialogViewModel: ViewModelBase
{
    //UI와 Binding
    [ObservableProperty]
    private bool _isDialogOpen;
    
    protected TaskCompletionSource CloseTask = new(TaskCreationOptions.RunContinuationsAsynchronously);
   // protected TaskCompletionSource<TResult?> closeTask = new TaskCompletionSource<TResult?>();
    
    /// <summary>
    /// Dialog 창 닫힐때 까지 기다림
    /// </summary>
    public async Task WaitAsync()
    {
        await CloseTask.Task;
        
    } 
    
    /// <summary>
    /// Dialog 창 열림
    /// </summary>
    public void Show()
    {
        if (CloseTask.Task.IsCompleted)
        {
            CloseTask = new (TaskCreationOptions.RunContinuationsAsynchronously);
        }
        IsDialogOpen = true;
    }
    
    /// <summary>
    /// Dialog 창 닫힘
    /// </summary>
    public void Close()
    {
        IsDialogOpen = false;
        
        CloseTask.TrySetResult();
    }
    
    
}