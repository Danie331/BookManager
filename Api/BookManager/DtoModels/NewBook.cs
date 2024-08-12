using System.ComponentModel.DataAnnotations;

namespace BookManager.DtoModels
{
    public record class NewBook
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Author { get; set; }

        public DateTime? PublishedDate { get; set; }

        [Required]
        public required string Isbn { get; set; }
    }
}
