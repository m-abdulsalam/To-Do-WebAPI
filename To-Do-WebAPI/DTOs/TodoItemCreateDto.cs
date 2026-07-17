using System.ComponentModel.DataAnnotations;

namespace To_Do_WebAPI.DTOs
{
    public class TodoItemCreateDto
    {
        [Required(ErrorMessage ="This field is Required")]
        [StringLength(99,ErrorMessage = "This title cannot exceed 99 characters")]
        public string ?Title { get; set; } = null;
        
        [Required(ErrorMessage ="This fied is compulsory")]
        public string? Description { get; set; }
        public string CategoryName { get; set; } = "General";
    }
}
