using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DataStorage.DataModels;

namespace TodoList.DataStorage;

public class DatabaseService(ApplicationDbContext context) : IDisposable
{

    private readonly ApplicationDbContext _context = context;

    public void Dispose()
    {
        _context.Dispose();
    }

    // public List<TodoItemsDataModel> GetTodoItems()
    // {
    //     var todoItems = _context.TodoItems.FirstOrDefault();
    //
    //     if (todoItems != null)
    //     { }
    //     else
    //     {
    //         todoItems = new TodoItemsDataModel
    //         {
    //             Title = "Untitled1",
    //         };
    //         
    //         SaveTodoItems(todoItems);
    //     }
    //
    //     return _context.TodoItems.ToList<TodoItemsDataModel>();
    // }
    
    public List<TodoItemsDataModel> GetTodoItems(string categoryId)
    {
        var todoItems = _context.TodoItems
            .Where(t => t.CategoryId ==categoryId)
            .ToList();

        if (todoItems.Count == 0)
        {
            var newItem = new TodoItemsDataModel(title: "Untitled1", categoryId: categoryId);
            _context.TodoItems.Add(newItem);
            _context.SaveChanges();
            
            todoItems = _context.TodoItems
                .Where(t => t.CategoryId == categoryId)
                .ToList();
        }
        return todoItems;
    }

    public List<CategoryDataModel> GetCategories()
    {
        var categories = _context.Categories.FirstOrDefault();
        if (categories != null)
        { }
        else
        {

            categories = new CategoryDataModel
            {
                Title = "Untitled1"
            };
            SaveCategories(categories);
        }

        return _context.Categories.ToList<CategoryDataModel>();
    }


    public void SaveTodoItems(TodoItemsDataModel todoItem)
    {
        if(_context.TodoItems.Any(f => f.Id == todoItem.Id))
            _context.TodoItems.Update(todoItem);
        else
        {
            //_context.TodoItems.RemoveRange(_context.TodoItems);
            _context.TodoItems.Add(todoItem);
        }
        
        //Commitm
        _context.SaveChanges();
    }
    
    public void SaveCategories(CategoryDataModel categories)
    {
        if(_context.Categories.Any(f => f.Id == categories.Id))
            _context.Categories.Update(categories);
        else
        {
           // _context.Categories.RemoveRange(_context.Categories);
            _context.Categories.Add(categories);
        }
        
        //Commit
        _context.SaveChanges();
    }

    public void RemoveTodoItem(TodoItemsDataModel todoItems)
    {
        if(_context.TodoItems.Any(f => f.Id == todoItems.Id))
            _context.TodoItems.Remove(todoItems);
        
        _context.SaveChanges();
    }

    public void ApplyMigration()
    {
        _context.Database.EnsureCreated();
    }
}