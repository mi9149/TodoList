using System;
using System.ComponentModel.DataAnnotations;
using TodoList.Models;

namespace TodoList.DataStorage.DataModels;

public class TodoItemsDataModel
{
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } =string.Empty;
    public bool Completed { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public CategoryDataModel Category { get; set; } = new CategoryDataModel();
    public string? Memo{ get; set; }
    public State State { get; set; } = State.ToDo;
}