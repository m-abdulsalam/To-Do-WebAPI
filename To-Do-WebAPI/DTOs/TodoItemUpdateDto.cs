using System.ComponentModel.DataAnnotations;
using To_Do_WebAPI.Models.Enums;

namespace To_Do_WebAPI.DTOs
{
    public class TodoItemUpdateDto
    {
        [Required(ErrorMessage ="The title is required")]
        [StringLength(99, ErrorMessage = "This field cannot exceed 99 characters. ")]
        public string Title { get; set; } = null!;

       
        public ToDoStatus Status { get; set; } = ToDoStatus.Pending;
        public string CategoryName { get; set; } = "General";
        [StringLength(99, ErrorMessage = "This field cannot exceed 99 Character")]
        public string? Description { get; set; }

    }
}
