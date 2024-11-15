using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoList.Api.Enums;
using TodoList.Api.Models.Dtos;
using TodoList.Api.Models.Entities;
using TodoList.Api.Repositories;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController(ITodoRepository repository, ITodoListService service) : ControllerBase
{
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(IEnumerable<Todo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllTodos(StatusEnum? status = null)
    {       
        var allTodo = await repository.GetAllTodosAsync(status);
        return Ok(allTodo);
    }

    [HttpPost("AddTodo")]
    [ProducesResponseType(typeof(AddTodoDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult>AddTodo([FromBody]AddTodoDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var serviceResponse = await service.AddTodoAsync(request);
        return Ok(serviceResponse);
    }

    [HttpGet("GetTodoById")]
    [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.OK)]
    public async Task<IActionResult>GetTodoById(int id)
    {
        var response = await repository.GetTodoByIdAsync(id);
        return Ok(response);
    }

    [HttpGet("MarkAsComplete")]
    [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> MarkAsComplete(int id, StatusEnum status)
    {
        var response = await repository.UpdateTodoAsync(id, status);
        return Ok(response);
    }

    [HttpDelete("ClearCompleted")]
    public async Task<IActionResult> ClearCompletedAsync()
    {
        bool isSuccess = await repository.ClearCompletedTodosAsync();
        if (isSuccess)
            return Ok("Success");
        return BadRequest("Failed");
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var ids = new[] { id };
        bool isSuccess = await repository.DeleteTodoAsync(ids);
        if (isSuccess)
            return Ok("Success");
        return BadRequest("Failed");
    }
}
