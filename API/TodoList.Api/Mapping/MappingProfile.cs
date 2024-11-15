using AutoMapper;
using TodoList.Api.Models.Dtos;
using TodoList.Api.Models.Entities;

namespace TodoList.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, AddTodoDto>().ReverseMap();
        CreateMap<Todo, UpdateTodoDoto>().ReverseMap();
    }
}
