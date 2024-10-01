namespace BookStore.Domain.Entities;

public class Author 
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public int YearOfBirth { get; set; }
    
    public IList<AuthorsBooks> AuthorsBooks { get; } = new List<AuthorsBooks>();

    public Author(string fullName, int yearOfBirth)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        YearOfBirth = yearOfBirth;
    }
}