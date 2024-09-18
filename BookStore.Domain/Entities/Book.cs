namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int ReleaseYear { get; private set; }
    public string Description { get; private set; }
    public IList<BookCategoryBook> BookCategoryBooks { get; } = new List<BookCategoryBook>();
    
    //public List<string> CategoriesForBook { get; set; }

    public Book(string name, int releaseYear, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        ReleaseYear = releaseYear;
        Description = description;
    }

    private Book()
    {
        
    }
}