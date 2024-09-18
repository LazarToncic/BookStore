namespace BookStore.Domain.Entities;

public class BookCategoryBook
{
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    
    public BookCategory BookCategory { get; set; }
    public Guid BookCategoryId { get; set; }

    public BookCategoryBook(Guid bookId, Guid bookCategoryId)
    {
        BookId = bookId;
        BookCategoryId = bookCategoryId;
    }
}