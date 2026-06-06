namespace To_Do_WebAPI.Models.Entities
{
    public class TodoItem
    {
        public int id { get; set;}
        public string Title { get; set;}
        public string ?Description { get; set;} 
        public bool IsCpmpleted { get; set;}
    }
}
