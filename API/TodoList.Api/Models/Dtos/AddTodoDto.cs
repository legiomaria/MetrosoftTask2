using System.Text.Json.Serialization;
using TodoList.Api.Enums;
using TodoList.Api.Models.Entities;

namespace TodoList.Api.Models.Dtos;

public class AddTodoDto
{    
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
}
