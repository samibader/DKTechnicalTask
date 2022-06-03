using AutoMapper;
using DukkantekTask.Domain.Entities;
using DukkantekTask.Service.Models.Requests;

namespace DukkantekTask.Service.Profiles
{
    /// <summary>
    /// Automapper profile for Category
    /// </summary>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
        }
    }
}
