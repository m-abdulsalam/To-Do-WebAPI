using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_Do_WebAPI.Context;
using To_Do_WebAPI.DTOs;
using To_Do_WebAPI.Interfaces.Repositories;
using To_Do_WebAPI.Models.Entities;
using To_Do_WebAPI.Models.Enums;

namespace To_Do_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase           
    {
        private readonly IToDoRepository _toDoRepository;
        public TodoItemsController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItemCreateDto newItem)
        {
            var todoItem = new ToDoItem 
            {
                Title = newItem.Title!,
                Description = newItem.Description,
                CategoryName = newItem.CategoryName,
                Status = ToDoStatus.Pending,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            await _toDoRepository.AddAsync(todoItem);
            await _toDoRepository.SaveChangesAsync();

            var responseDto = new ToDoItemResponseDTOs
            {
                Id = todoItem.id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Status = ToDoStatus.Pending,
                DateCreated = todoItem.DateCreated,
                DateModified = todoItem.DateModified,
                CategoryName = todoItem.CategoryName
            };
            return CreatedAtAction(nameof(GetItemById), new { id = todoItem.id }, responseDto); 
        }





        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById([FromRoute]int id)
        {
            var item = await _toDoRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound($"Item with ID{id} not found");
            }
            var responseDto = new ToDoItemResponseDTOs
            {
                Id = item.id,
                Title = item.Title,
                Description = item.Description,
                Status= item.Status,
                DateCreated = item.DateCreated,
                CategoryName = item.CategoryName,
                DateModified = item.DateModified
            };
            return Ok(responseDto);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllItems(
            [FromQuery] string? search,
            [FromQuery] ToDoStatus status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
              var item = await _toDoRepository.GetAllItemAsync(search,status, pageNumber, pageSize);
            var responseDtos = item.Select(item => new ToDoItemResponseDTOs
            {
                Id = item.id,
                Title = item.Title,
                Description = item.Description,
                Status = item.Status,
                DateCreated = item.DateCreated,
                DateModified = item.DateModified,
                CategoryName = item.CategoryName
            }).ToList();
            return Ok(responseDtos);
        }






        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatedItem([FromRoute]int id, [FromBody] TodoItemUpdateDto updatdDto)
        {
            var todoItem = await _toDoRepository.GetItemByIdAsync(id);
            
            if (todoItem == null)
            {
                return NotFound($"Item with this {id}ID does not exist");
            }

            todoItem.Title = updatdDto.Title;
            todoItem.Description = updatdDto.Description;
            todoItem.Status = updatdDto.Status;
            todoItem.CategoryName = updatdDto.CategoryName;
            todoItem.DateModified = DateTime.UtcNow;

            _toDoRepository.Update(todoItem);
            await _toDoRepository.SaveChangesAsync();   

            return NoContent();

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute]int id)
        {
            var item = await _toDoRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound($"Item with this id {id}does not exust.");
            }   await _toDoRepository.Delete(id);
                await _toDoRepository.SaveChangesAsync();  
                return NoContent();
        }
    }
}
