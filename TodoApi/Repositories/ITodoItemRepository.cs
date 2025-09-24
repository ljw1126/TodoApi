using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<int> UpdateAsync();
        Task<int> DeleteAsync(TodoItem todoItem);
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(long id);
    }
}
