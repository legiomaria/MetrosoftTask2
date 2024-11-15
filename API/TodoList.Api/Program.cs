using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TodoList.Api.EF;
using TodoList.Api.Helpers;
using TodoList.Api.Mapping;
using TodoList.Api.Repositories;
using TodoList.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnection = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());

app.UseAuthorization();

app.MapControllers();
DatabaseHelper.UpdateDatabase(app);
app.Run();
