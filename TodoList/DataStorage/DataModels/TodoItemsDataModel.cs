using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using TodoList.Models;

namespace TodoList.DataStorage.DataModels;

public class TodoItemsDataModel : IEnumerable
{
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } =string.Empty;
    public bool Completed { get; set; } = false;
    public DateTime? DueDate { get; set; } = null;
    [MaxLength(100)]
    public string CategoryID {get; set;}  //FK
    public CategoryDataModel? Category { get; set; } //CategoryId를 이용해 CategoryDataModel을 자동으로 로드할 때 사용.


    public string? Memo { get; set; } = null;
   
    /* public State State { get; set; } = State.ToDo;*/
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}