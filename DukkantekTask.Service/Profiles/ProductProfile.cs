using AutoMapper;
using DukkantekTask.Domain.Entities;
using DukkantekTask.Service.Models.Requests;

namespace DukkantekTask.Service.Profiles
{
    /// <summary>
    /// Automapper profile for Product
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ChangeProductStatusRequest, Product>();
        }
    }
}
