using System.Collections.ObjectModel;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class KanbanColumnViewModel : ViewModelBase
{
    public ObservableCollection<KanbanCardViewModel> Cards { get; } = new();
    
    private State Type { get; }

    
}  