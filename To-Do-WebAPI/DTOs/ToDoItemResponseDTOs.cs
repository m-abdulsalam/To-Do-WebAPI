using To_Do_WebAPI.Models.Enums;
namespace To_Do_WebAPI.DTOs
{
    public class ToDoItemResponseDTOs
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CategoryName { get; set; } = "General";
        public ToDoStatus Status { get; set; } 
        public DateTime DateCreated { get; set; }   
        public DateTime DateModified { get; set; }
    }
}
