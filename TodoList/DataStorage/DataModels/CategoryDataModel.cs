using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TodoList.DataStorage.DataModels;

public class CategoryDataModel : IEnumerable
{
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [MaxLength(100)] 
    public string Title { get; set; } = string.Empty;
        
    [MaxLength(10)]
    public string ColorHex { get; set; } = "#0000FF";

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}