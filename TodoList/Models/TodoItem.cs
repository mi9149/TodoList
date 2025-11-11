using System;
using System.ComponentModel;

namespace TodoList.Models;

public class TodoItem
{
   
    public string Content { get; set; } =string.Empty;
    public bool Completed { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public Category Category { get; set; } = new Category();
    public string? Memo{ get; set; }
    public State State { get; set; } = State.ToDo;

}

public enum State
{
    ToDo,
    InProgress,
    Done    
}