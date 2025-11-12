using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using TodoList.DataStorage.DataModels;

namespace TodoList.DataStorage;

public class ApplicationDbContext : DbContext
{

    public DbSet<TodoItemsDataModel> TodoItems { get; set; }
    public DbSet<CategoryDataModel> Categories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TodoList");
        Directory.CreateDirectory(storagePath);
        

        optionsBuilder.UseSqlite($"Data Source={Path.Combine(storagePath, "TodoList.db")};");
    }

    /// <summary>
    /// 테이블 구조를 설정
    /// 매핑 규칙을 수동으로 설정
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //ID 속성을 Primary Key로 지정
        modelBuilder.Entity<TodoItemsDataModel>()
            .HasKey(f => f.Id);
        
        //외래키 지정
        modelBuilder.Entity<TodoItemsDataModel>()
            .HasOne(t => t.Category) 
            .WithMany(c => c.TodoItems)
            .HasForeignKey(t => t.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);


        //ID 속성을 Primary Key로 지정
        modelBuilder.Entity<CategoryDataModel>()
            .HasKey(f => f.Id);

    }
}