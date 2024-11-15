﻿namespace TodoList.Api.Models.Entities;
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    //public bool IsCompleted { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
