using AutoMapper;

namespace BookManager.Automapper
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<DtoModels.Book, DomainModels.Book>().ReverseMap();
            CreateMap<DtoModels.NewBook, DomainModels.Book>();
            CreateMap<DtoModels.SearchFilter, DomainModels.SearchFilter>();
            CreateMap(typeof(DomainModels.PaginatedList<>), typeof(DtoModels.PaginatedList<>));
        }
    }
}
