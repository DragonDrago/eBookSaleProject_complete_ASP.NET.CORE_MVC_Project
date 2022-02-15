using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using eBookSaleProject.Data.Enum;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Identity;
using eBookSaleProject.Data.Static;

namespace eBookSaleProject.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder application)
        {
            using (var serviceScope = application.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                //Authors
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            FullName = "Author1",
                            ProfilePictureURL ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg" ,
                            Biography = " This is a biography of Author 1"
                        },
                         new Author()
                        {
                            FullName = "Author2",
                            ProfilePictureURL ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg" ,
                            Biography = " This is a biography of Author 2"
                        },
                          new Author()
                        {
                            FullName = "Author3",
                            ProfilePictureURL ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg" ,
                            Biography = " This is a biography of Author 3"
                        },
                           new Author()
                        {
                            FullName = "Author4",
                            ProfilePictureURL ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg" ,
                            Biography = " This is a biography of Author 4"
                        },
                         new Author()
                        {
                            FullName = "Author 5",
                            ProfilePictureURL ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg" ,
                            Biography = " This is a biography of Author 5"
                        }

                    });
                    context.SaveChanges();
                }
                //Books
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Name ="BookName1",
                            Description ="Book_1 Description",
                            Price = 12.40,
                            ImageUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            BookFileUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            EdititonDate = DateTime.Now.AddDays(-7),
                            BookCategory = BookCategory.Childrens,
                            PublisherId =1
                        },
                        new Book()
                        {
                            Name ="BookName2",
                            Description ="Book_2 Description",
                            Price = 14.70,
                            ImageUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            BookFileUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            EdititonDate = DateTime.Now.AddDays(-4),
                            BookCategory = BookCategory.Crime,
                            PublisherId = 2
                        },
                        new Book()
                        {
                            Name ="BookName3",
                            Description ="Book_3 Description",
                            Price = 44.40,
                            ImageUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            BookFileUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            EdititonDate = DateTime.Now.AddDays(-1),
                            BookCategory = BookCategory.Fantasy,
                            PublisherId =3
                        },
                        new Book()
                        {
                            Name ="BookName4",
                            Description ="Book_4 Description",
                            Price = 12.40,
                            ImageUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            BookFileUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            EdititonDate = DateTime.Now.AddDays(-9),
                            BookCategory = BookCategory.Fantasy,
                            PublisherId =3
                        },
                        new Book()
                        {
                            Name ="BookName5",
                            Description ="Book_5 Description",
                            Price = 66.40,
                            ImageUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            BookFileUrl ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            EdititonDate = DateTime.Now.AddDays(-5),
                            BookCategory = BookCategory.Guide,
                            PublisherId = 2 
                        }
                    });
                    context.SaveChanges();
                }
                //Publishers
                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            Name ="Publisher1",
                            Logo ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            Description ="This is the description of Publisher 1"
                        },
                        new Publisher()
                        {
                            Name ="Publisher2",
                            Logo ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            Description ="This is the description of Publisher 2"
                        },
                         new Publisher()
                        {
                            Name ="Publisher3",
                            Logo ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            Description ="This is the description of Publisher 3"
                        },
                          new Publisher()
                        {
                            Name ="Publisher4",
                            Logo ="https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                            Description ="This is the description of Publisher 4"
                        }
                    });
                    context.SaveChanges();
                }
                //Author_Books
                if (!context.Author_Books.Any())
                {
                    context.Author_Books.AddRange(new List<Author_Book>()
                    {
                      new Author_Book()
                      {
                          BookId = 1,
                          AuthorId = 1
                      },
                       new Author_Book()
                      {
                          BookId = 3,
                          AuthorId =4
                      },
                      new Author_Book()
                      {
                          BookId = 3,
                          AuthorId = 3
                      },
                      new Author_Book()
                      {
                          BookId = 1,
                          AuthorId = 4
                      },
                      new Author_Book()
                      {
                          BookId = 1,
                          AuthorId = 1
                      },
                      new Author_Book()
                      {
                          BookId = 2,
                          AuthorId = 3
                      },
                      new Author_Book()
                      {
                          BookId = 4,
                          AuthorId = 4
                      }

                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users -Admin
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //App -User
                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "user");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
