﻿
using Microsoft.AspNetCore.Identity;
using my_books.Data.Models;

namespace my_books.Data;

public class AppDbInitialiser
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if(!context.Books.Any())
            {
                context.Books.AddRange(new Book(){
                    Title = "1st Book Title",
                    Description = "1st Book Desc",
                    IsRead = true,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Biography",
                    CoverUrl = "https....",
                    DateAdded = DateTime.Now
                },
                new Book(){
                    Title = "2nd Book Title",
                    Description = "2nd Book Desc",
                    IsRead = false,
                    Genre = "Biography",
                    CoverUrl = "https....",
                    DateAdded = DateTime.Now
                }
                );
                context.SaveChanges();
            }
        }
    }

    public static async Task SeedRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));

                if (!await roleManager.RoleExistsAsync(UserRoles.Author))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Author));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
        }
}
