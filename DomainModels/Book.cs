namespace DomainModels
{
    public class Book
    {
        public Guid? Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public required string Isbn { get; set; }
    }
}