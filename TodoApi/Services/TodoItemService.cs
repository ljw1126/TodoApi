using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> CreateAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            return await _repository.CreateAsync(todoItem);
        }

        public async Task UpdateAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new ArgumentException();
            }

            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _repository.UpdateAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ApplicationException();
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                return 0;
            }

            return await _repository.DeleteAsync(todoItem);
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
