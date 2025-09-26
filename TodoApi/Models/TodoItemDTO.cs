namespace TodoApi.Models
{
    public record TodoItemDto(long Id, string? Name, bool IsComplete);
}
