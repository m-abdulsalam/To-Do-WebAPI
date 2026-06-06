using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_Do_WebAPI.Context;
using To_Do_WebAPI.Models;
using To_Do_WebAPI.Models.Entities;

namespace To_Do_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase           
    {
        private readonly ToDoContext _context;
        public TodoItemsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItem newItem)
        {
            if (newItem == null)
            {
                return BadRequest("Item data can not be empty");
            }
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemById), new { id = newItem.id }, newItem);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById([FromRoute]int id)
        {

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"Item with ID{id} not found");
            }
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var allItem = await _context.Items.ToListAsync();
            return Ok(allItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatedItem([FromRoute]int id, [FromBody] TodoItem updatdItem)
        {
            if (id != updatdItem.id)
            {
                return BadRequest("Item Id mistmatched");
            }
            _context.Entry(updatdItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!_context.Items.Any(e => e.id == id)) 
                {
                    return NotFound($"Item with ID {id} does not exist");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute]int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"Item with this id {id}does not exust.");
            }   _context.Items.Remove(item);
                await _context.SaveChangesAsync();  
                return NoContent();
        }
    }
}
