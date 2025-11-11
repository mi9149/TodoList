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

    public TodoItemsDataModel GetTodoItems()
    {
        var todoItems = _context.TodoItems.FirstOrDefault();

        if (todoItems != null) return todoItems;

        todoItems = new TodoItemsDataModel
        {
            Title = "Todo1"
        };

        SaveTodoItems(todoItems);
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


    public void SaveTodoItems(TodoItemsDataModel todoItems)
    {
        if(_context.TodoItems.Any(f => f.Id == todoItems.Id))
            _context.TodoItems.Update(todoItems);
        else
        {
            _context.TodoItems.RemoveRange(_context.TodoItems);
            _context.TodoItems.Add(todoItems);
        }
        
        //Commit
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

    public void ApplyMigration()
    {
        _context.Database.EnsureCreated();
    }
}