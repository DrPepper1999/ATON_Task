using System;
using Users.Domain;

namespace Users.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(UsersDbContext context)
        { 
           if (context.Database.EnsureCreated())
           {
                context.Users.Add(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Login = "admin",
                        Password = "admin",
                        Name = "admin",
                        Gender = 0,
                        BirthDay = null,
                        Admin = true,
                        CreatedBy = "system",
                        CreatedOn = DateTime.Now
                    });
                context.SaveChanges();
           }
        }
    }
}
