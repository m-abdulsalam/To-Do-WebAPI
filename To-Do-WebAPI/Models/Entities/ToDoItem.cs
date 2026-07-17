using System.ComponentModel.DataAnnotations;
using To_Do_WebAPI.Models.Enums;
namespace To_Do_WebAPI.Models.Entities
{
    public class ToDoItem
    {
        public int id { get; set;}
        public string Title { get; set;} = string.Empty;
        public string ?Description { get; set;} 
        public ToDoStatus Status { get; set; } = ToDoStatus.Pending;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set;  } = DateTime.UtcNow;
        public int CategoryId { get; set;}
        public string CategoryName { get; set; } = "General";
    }
}
