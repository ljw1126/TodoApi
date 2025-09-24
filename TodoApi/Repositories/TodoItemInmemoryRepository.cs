using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoItemInmemoryRepository : ITodoItemRepository
    {
        private readonly TodoContext _dbContext;

        public TodoItemInmemoryRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<int> UpdateAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TodoItem todoItem)
        {
            _dbContext.TodoItems.Remove(todoItem);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {
            return await _dbContext.TodoItems.FindAsync(id);
        }
    }
}
