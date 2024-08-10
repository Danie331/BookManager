using Microsoft.AspNetCore.Mvc;

namespace BookManager.DtoModels
{
    public record class SearchFilter
    {
        [FromQuery(Name = "author")]
        public string Author { get; set; }
        [FromQuery(Name = "title")]
        public string Title { get; set; }
    }
}
