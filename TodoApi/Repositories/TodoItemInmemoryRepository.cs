using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoItemInmemoryRepository(TodoContext dbContext) : ITodoItemRepository
    {
        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            dbContext.TodoItems.Add(todoItem);
            await dbContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<int> UpdateAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TodoItem todoItem)
        {
            dbContext.TodoItems.Remove(todoItem);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {
            return await dbContext.TodoItems.FindAsync(id);
        }
    }
}
