
using To_Do_WebAPI.Models.Entities;
using To_Do_WebAPI.Models.Enums;


namespace To_Do_WebAPI.Interfaces.Repositories
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllItemAsync(string? search, ToDoStatus? status, int pageNumber, int pageSize);
        Task<ToDoItem?> GetItemByIdAsync(int id);
        Task AddAsync (ToDoItem item);
        void Update(ToDoItem item);     
        Task Delete(int id);Task<bool> SaveChangesAsync();
    }
}