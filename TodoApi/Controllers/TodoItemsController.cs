using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController(TodoItemService service) : ControllerBase
    {

        // Get: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var todoItems = await service.GetAllAsync();
            var result = todoItems.ConvertAll(x => ItemToDto(x));
            return Ok(result);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await service.GetByIdAsync(id);
            return todoItem == null
                ? NotFound()
                : Ok(ItemToDto(todoItem));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDTO)
        {
            var todoItem = await service.CreateAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDto(todoItem));
        }

        // PUT: api/TodoItems/5
        // IActionResult ??
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItemDTO)
        {
            try
            {
                await service.UpdateAsync(id, todoItemDTO);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ApplicationException)
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await service.DeleteAsync(id);
            return result == 0 ? NotFound() : NoContent();
        }


        // 람다에 리터럴 방식으로 초기화가 가능
        private static TodoItemDto ItemToDto(TodoItem todoItem) =>
            new(todoItem.Id, todoItem.Name, todoItem.IsComplete);

    }
}
