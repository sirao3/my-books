using my_books.Data.Models;

namespace my_books;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Navigation properties
    public List<Book> Books { get; set; }
}
