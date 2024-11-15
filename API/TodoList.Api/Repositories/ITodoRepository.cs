using TodoList.Api.Enums;
using TodoList.Api.Models.Dtos;
using TodoList.Api.Models.Entities;

namespace TodoList.Api.Repositories;
public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllTodosAsync(StatusEnum? status);
    Task<IEnumerable<Todo>> GetTodosByStatusAsync(string status);
    Task<Todo> GetTodoByIdAsync(int id);
    Task<Todo> AddTodoAsync(Todo todo);
    Task<bool> UpdateTodoAsync(int id, StatusEnum status);
    Task<bool> DeleteTodoAsync(int[] ids);
    Task<bool> ClearCompletedTodosAsync();
}
