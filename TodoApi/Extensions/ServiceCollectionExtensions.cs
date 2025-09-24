using TodoApi.Repositories;
using TodoApi.Services;

namespace TodoApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemRepository, TodoItemInmemoryRepository>();
            services.AddScoped<TodoItemService>();
            return services;
        }
    }
}
