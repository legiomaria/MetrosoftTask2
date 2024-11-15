using TodoList.Api.Models.Dtos;

namespace TodoList.Api.Services;

public interface ITodoListService
{
    Task<AddTodoDto> AddTodoAsync(AddTodoDto request);
}