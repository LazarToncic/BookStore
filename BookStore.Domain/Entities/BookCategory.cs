namespace BookStore.Domain.Entities;

public class BookCategory
{
    public Guid Id { get; private set; }
    public string BookCategoryName { get; private set; }
    public IList<BookCategoryBook> BookCategoryBooks { get; } = new List<BookCategoryBook>();

    public BookCategory(string bookCategoryName)
    {
        Id = Guid.NewGuid();
        BookCategoryName = bookCategoryName;
    }

    private BookCategory()
    {
        
    }
}