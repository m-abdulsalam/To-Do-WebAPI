using To_Do_WebAPI.Context;
using To_Do_WebAPI.Models.Enums;

using To_Do_WebAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using To_Do_WebAPI.Models.Entities;
namespace To_Do_WebAPI.Implementations.Repositories
{
    public class ToDoRepository : IToDoRepository   
    {
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ToDoItem item)
        {
            await _context.Items.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var items = await _context.Items.FindAsync(id);
            if (items != null)
            {
                _context.Items.Remove(items);
            }
        }                                   

        public async Task<IEnumerable<ToDoItem>> GetAllItemAsync(string? search, ToDoStatus? status, int pageNumber , int pageSize) 
        {
            var query = _context.Items.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.Title.Contains(search) || (i.Description != null && i.Description.Contains(search)));
            }

            if (status.HasValue)
            {
                query = query.Where(i => i.Status == status.Value);
            }
        
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<ToDoItem?> GetItemByIdAsync(int id)
        {
          return await _context.Items.FindAsync(id);  
        }
        public void Update(ToDoItem item)
        {
            _context.Items.Update(item);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
