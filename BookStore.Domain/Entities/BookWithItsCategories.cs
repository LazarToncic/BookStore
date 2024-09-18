namespace BookStore.Domain.Entities;

public class BookWithItsCategories
{
    public string Name { get;  set; }
    public int ReleaseYear { get;  set; }
    public string Description { get;  set; }
    public List<string> CategoriesForBook { get; private set; }

    public BookWithItsCategories AddCategoriesForBook(List<string> categories)
    {
        CategoriesForBook = categories;
        return this;
    }
}