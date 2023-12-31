using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services;

public class BooksService
{
    private AppDbContext _context;
    public BooksService(AppDbContext context)
    {
        _context = context;
    }
    public List<Book> GetAllBooks(){
        var allBooks= _context.Books.ToList();
        return allBooks;
    }

    public Book GetBookById(int bookId){
        var bookbyId = _context.Books.FirstOrDefault(n => n.Id == bookId);
        return bookbyId;
    }

    public void AddBook(BookVM book){
        var _book = new Book(){
            Title = book.Title,
            Description = book.Description,
            IsRead = book.IsRead,
            DateRead = book.IsRead ? book.DateRead.Value : null,
            Rate = book.IsRead ? book.Rate.Value :null,
            Genre = book.Genre,
            Author = book.Author,
            CoverUrl = book.CoverUrl,
            DateAdded = DateTime.Now
        };
        _context.Books.Add(_book);
        _context.SaveChanges();
    }

    public Book UpdateBookById(int bookId, BookVM book){
        var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
        if(_book != null){
            _book.Title = book.Title;
            _book.Description = book.Description;
            _book.IsRead = book.IsRead;
            _book.DateRead = book.IsRead ? book.DateRead.Value : null;
            _book.Rate = book.IsRead ? book.Rate.Value :null;
            _book.Genre = book.Genre;
            _book.Author = book.Author;
            _book.CoverUrl = book.CoverUrl;

            _context.SaveChanges();
        }
        return _book;
    }

    public void DeleteBookById(int bookId){
        var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
        if(_book !=null){
            _context.Books.Remove(_book);
            _context.SaveChanges();
        }
    }
}
