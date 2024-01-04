namespace my_books;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public PaginatedList(List<T> items, int count, int PageIndex, int pageSize)
    {
        PageIndex= PageIndex;
        TotalPages = (int)Math.Ceiling(count/ (double)pageSize);

        this.AddRange(items);
    }

    public bool HasPreviousPage{
        get{
            return PageIndex>1;
        }
    }

     public bool HasNextPage{
        get{
            return PageIndex < TotalPages;
        }
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int PageIndex, int pageSize){
        var count = source.Count();
        var items = source.Skip((PageIndex -1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count , PageIndex, pageSize);
    }
}
