using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var repositoryType = configuration.GetValue<string>("RepositoryType");
            string databaseName = configuration.GetConnectionString("DefaultConnection");

            switch (repositoryType)
            {
                case "MSSQL":
                    services.AddDbContext<TodoContext>(options => options.UseSqlServer(databaseName));
                    services.AddScoped<ITodoItemRepository, TodoItemInmemoryRepository>();
                    break;
                case "InMemory":
                    services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase(databaseName));
                    services.AddScoped<ITodoItemRepository, TodoItemInmemoryRepository>();
                    break;
                default:
                    throw new ArgumentException();
            }

            return services;
        }
    }
}
