namespace BookStore.Domain.Entities;

public class AuthorsBooks
{
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    
    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public AuthorsBooks(Guid authorId, Guid bookId)
    {
        AuthorId = authorId;
        BookId = bookId;
    }
}