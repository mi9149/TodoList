using TodoList.ViewModels;

namespace TodoList.Dialog;

public interface IDialogProvider
{
    DialogViewModel? Dialog { get; set; }
}