using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// ��ü ������ ��ȸ
app.MapGet("/todoitems", async (TodoDb db) => 
    await db.Todos.ToListAsync());

// IsComplete�� true �� ��츸 ��ȸ
app.MapGet("/todoItems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync()
);

// id �ܰ� ��ȸ
app.MapGet("/todoItems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
         ? Results.Ok(todo)
         : Results.NotFound()
);

// $�� ��ƽ�� ���, Location ���� �����̷�Ʈ
app.MapPost("/todoitems", async (Todo todo, TodoDb db) => {
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
       if(await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();
