using AutoMapper;

using LivingLab.Core.Entities;
using LivingLab.Web.Models.DTOs.Todo;

namespace LivingLab.Web.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDTO>().ReverseMap();
    }
}
