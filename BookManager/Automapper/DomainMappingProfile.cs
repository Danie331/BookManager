using AutoMapper;

namespace BookManager.Automapper
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<DtoModels.Book, DomainModels.Book>().ReverseMap();
            CreateMap<DtoModels.SearchFilter, DomainModels.SearchFilter>();
        }
    }
}
