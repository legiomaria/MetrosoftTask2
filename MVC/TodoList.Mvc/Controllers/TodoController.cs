using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TodoList.Api.Enums;
using TodoList.Api.Models.Entities;
using TodoList.Mvc.Constants;

namespace TodoList.Mvc.Controllers;

public class TodoController(IHttpClientFactory httpClientFactory) : Controller
{
    readonly HttpClient _httpClient = httpClientFactory.CreateClient(NameConstant.ApiName);

    //GET: /Todo
    public async Task<IActionResult> Index(StatusEnum? status = null)
    {
        var todos = await GetAllTodosAsync(status); 
        return View(todos);
    }
    // Fetch all todos from the API
    private async Task<IEnumerable<Todo>> GetAllTodosAsync(StatusEnum? status)
    {
        string url = status == null ? "GetAll" : $"GetAll?status={status}";
        return await _httpClient.GetFromJsonAsync<IEnumerable<Todo>>(url);
    }

    // Add a new todo (called via AJAX in todo.js)
    [HttpPost]
    public async Task<IActionResult> AddTodo([FromBody] Todo todo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _httpClient.PostAsJsonAsync("AddTodo", todo);
        response.EnsureSuccessStatusCode();
        var createdTodo = await response.Content.ReadFromJsonAsync<Todo>();

        return Json(createdTodo);
    }

    // Update an existing todo's completion status
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoStatus(int id, [FromBody] Todo todo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _httpClient.PutAsJsonAsync($"{id}", todo);
        response.EnsureSuccessStatusCode();

        return Ok();
    }

    // Delete a todo by ID
    [HttpDelete()]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var url = $"Delete?id={id}";
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        return Ok();
    }

    [HttpDelete()]
    public async Task<IActionResult> ClearCompleted()
    {
        var url = $"clearCompleted";
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        return Ok();
    }

    [HttpPut()]
    public async Task<IActionResult> MarkAsComplete(int id, StatusEnum status)
    {
        try
        {
            var url = $"MarkAsComplete?id={id}&status={status}";
            var response = await _httpClient.GetAsync(url);
            return Ok();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
