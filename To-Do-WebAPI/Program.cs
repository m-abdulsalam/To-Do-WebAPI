using Microsoft.EntityFrameworkCore;
using To_Do_WebAPI.Context;
using To_Do_WebAPI.Implementations.Repositories;
using To_Do_WebAPI.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoContext")
    ));

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "To-Do API v1");
        options.RoutePrefix = "swagger";

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
