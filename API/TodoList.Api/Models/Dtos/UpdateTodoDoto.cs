using TodoList.Api.Enums;

namespace TodoList.Api.Models.Dtos;

public class UpdateTodoDoto
{
    public int Id { get; set; }
    public StatusEnum Status { get; set; }
}
