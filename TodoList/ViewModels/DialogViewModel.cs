using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class DialogViewModel<TResult>: ViewModelBase
{
    //UI와 Binding
    [ObservableProperty]
    private bool _isDialogOpen;

    protected TaskCompletionSource<TResult?> closeTask = new(TaskCreationOptions.RunContinuationsAsynchronously);
    
    /// <summary>
    /// Dialog 창 닫힐때 까지 기다림
    /// </summary>
    public async Task<TResult?> WaitAsnyc()
    {
       return await closeTask.Task;
    } 
    
    /// <summary>
    /// Dialog 창 열림
    /// </summary>
    public void Show()
    {
        if (closeTask.Task.IsCompleted)
        {
            closeTask = new (TaskCreationOptions.RunContinuationsAsynchronously);
        }
        IsDialogOpen = true;
    }
    
    /// <summary>
    /// Dialog 창 닫힘
    /// </summary>
    public void Close(TResult? result)
    {
        IsDialogOpen = false;
        
        closeTask.TrySetResult(result);
    }
    
    
}