using AutoMapper;
using DomainModels;
using Persistence.Entities;

namespace Persistence.Automapper
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Book, BookEntity>().ReverseMap();
        }
    }
}