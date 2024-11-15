using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models.Entities;

namespace TodoList.Api.EF;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
}
