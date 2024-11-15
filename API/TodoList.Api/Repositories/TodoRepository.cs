using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using TodoList.Api.EF;
using TodoList.Api.Enums;
using TodoList.Api.Models.Dtos;
using TodoList.Api.Models.Entities;

namespace TodoList.Api.Repositories;

public class TodoRepository(ApplicationDbContext context) : ITodoRepository
{
    public async Task<Todo> AddTodoAsync(Todo todo)
    {
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return todo;
    }

    public async Task<bool> ClearCompletedTodosAsync()
    {
        var completedTodos = context.Todos.Where(t => t.Status == StatusEnum.Completed.ToString());
        context.Todos.RemoveRange(completedTodos);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTodoAsync(int[] ids)
    {
        bool isSuccess = false;
        if (ids.Length > 0)
        {
            var todos = await context.Todos.Where(todo => ids.Contains(todo.Id)).ToListAsync();

            if (todos.Any())
            {
                context.Todos.RemoveRange(todos);
              isSuccess =  await context.SaveChangesAsync() > 0;
            }
        }
        return isSuccess;
    }


    public async Task<IEnumerable<Todo>> GetAllTodosAsync(StatusEnum? status)
    {
        return await context.Todos
            .Where(todo => !status.HasValue || status == StatusEnum.All || todo.Status == status.ToString())
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }


    public async Task<Todo> GetTodoByIdAsync(int id)
        => await context.Todos.FindAsync(id);

    public async Task<IEnumerable<Todo>> GetTodosByStatusAsync(string status)
        => await context.Todos
            .Where(t => t.Status == status)
            .ToListAsync();
    public async Task<bool> UpdateTodoAsync(int id, StatusEnum status)
    {
        var model = await context.Todos.FindAsync(id);
        model.UpdatedDate = DateTime.Now;
        model.Status = status.ToString();
        context.Todos.Update(model);
        int updateStatus = await context.SaveChangesAsync();
        return updateStatus > 0;
    }
}
