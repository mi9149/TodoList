using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace TodoList.DataStorage.DataModels;

public class CategoryDataModel : IEnumerable
{
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [MaxLength(100)] 
    public string Title { get; set; } = string.Empty;
        
    [MaxLength(10)]
    public string ColorHex { get; set; } = "#0000FF";

    public List<TodoItemsDataModel> TodoItems { get; set; } = new();

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}