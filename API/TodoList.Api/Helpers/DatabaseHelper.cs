using Microsoft.EntityFrameworkCore;
using TodoList.Api.EF;
using TodoList.Api.Enums;

namespace TodoList.Api.Helpers;
public class DatabaseHelper
{
    public static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();
        var todos = context.Todos.ToList();
        if (!todos.Any())
        {
            var now = DateTime.Now;
            //Seed Database
            todos =
            [
                new Models.Entities.Todo{ Title = "Prepare Code Skeleton",  Status  = StatusEnum.Active.ToString(), CreatedDate = now},
                new Models.Entities.Todo{ Title = "Take a Nap",  Status  = StatusEnum.Completed.ToString(), CreatedDate = now},
                new Models.Entities.Todo{ Title = "Debug Critical Issues",  Status  = StatusEnum.Active.ToString(), CreatedDate = now},
                new Models.Entities.Todo{ Title = "Play Video Games",  Status  = StatusEnum.Completed.ToString(), CreatedDate = now},
                new Models.Entities.Todo{ Title = "Set up Public Repository",  Status  = StatusEnum.Active.ToString(), CreatedDate = now},
            ];

            context.Todos.AddRange(todos);
            context.SaveChanges();
        }
        return;
    }
}