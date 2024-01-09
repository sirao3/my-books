using Microsoft.AspNetCore.Identity;

namespace my_books;

public class ApplicationUser : IdentityUser
{
    public string Custom { get; set; }

}
