using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoItemService _service;

        public TodoItemsController(TodoItemService service)
        {
            _service = service;
        }

        // Get: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _service.GetAllAsync();
            var result = todoItems.ConvertAll(x => ItemToDto(x));
            return Ok(result);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _service.GetByIdAsync(id);
            return todoItem == null
                ? NotFound()
                : Ok(ItemToDto(todoItem));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _service.CreateAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDto(todoItem));
        }

        // PUT: api/TodoItems/5
        // IActionResult ??
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                await _service.UpdateAsync(id, todoItemDTO);
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
            var result = await _service.DeleteAsync(id);
            return result == 0 ? NotFound() : NoContent();
        }


        // 람다에 리터럴 방식으로 초기화가 가능
        private static TodoItemDTO ItemToDto(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

    }
}
