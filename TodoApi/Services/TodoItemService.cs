using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoItemService(ITodoItemRepository repository)
    {
        public async Task<TodoItem> CreateAsync(TodoItemDto todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            return await repository.CreateAsync(todoItem);
        }

        public async Task UpdateAsync(long id, TodoItemDto todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new ArgumentException();
            }

            var todoItem = await repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await repository.UpdateAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ApplicationException();
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var todoItem = await repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                return 0;
            }

            return await repository.DeleteAsync(todoItem);
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}
