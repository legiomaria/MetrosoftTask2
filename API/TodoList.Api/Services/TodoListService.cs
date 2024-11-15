using AutoMapper;
using TodoList.Api.Enums;
using TodoList.Api.Models.Dtos;
using TodoList.Api.Models.Entities;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services;

public class TodoListService(IMapper mapper, ITodoRepository repository) : ITodoListService
{
    public async Task<AddTodoDto> AddTodoAsync(AddTodoDto request)
    {
        var model = mapper.Map<Todo>(request);
        model.CreatedDate = DateTime.Now;
        model.Status = request.Status;
        model = await repository.AddTodoAsync(model);
        var response = mapper.Map<AddTodoDto>(model);
        return response;
    }
}
