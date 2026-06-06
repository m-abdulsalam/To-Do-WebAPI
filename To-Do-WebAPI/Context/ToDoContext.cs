using Microsoft.EntityFrameworkCore;
using To_Do_WebAPI.Models.Entities;

namespace To_Do_WebAPI.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            :base (options) { }
        public DbSet<TodoItem> Items { get; set; }         
    }
}
