using System;
using Microsoft.EntityFrameworkCore;
using Users.Domain;
using Users.Persistence;

namespace Users.Tests.Common
{
    public class UsersContextFactory
    {
        public static string UserAdminLogin { get; set; } = "admin";
        public static string UserLoginForFullDelete = "login1";
        public static string UserLoginForSoftDelete = "login2";
        public static string UserLoginForUpdate = "login3";
        public static string UserLoginForUpdateExeption = "login4";
        public static string UserLoginForUpdateYourself = "login5";
        public static string UserLoginForUpdateYourselfExeption = "login6";
        public static string UserLoginForUpdateRecovery = "login7";
        public static string UserLoginForDetailsQuery = "login2";
        public static UsersDbContext Create()
        {   
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new UsersDbContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserAdminLogin,
                    Password = "admin",
                    Name = "admin",
                    BirthDay = new DateTime(1982, 4, 3),
                    Gender = 2,
                    Admin = true,
                    CreatedBy = "admin",
                    CreatedOn = DateTime.Today
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForFullDelete,
                    Password = "password1",
                    Name = "name1",
                    BirthDay = new DateTime(1999, 12, 8),
                    Gender = 0,
                    Admin = false,
                    CreatedBy = "admin",
                    CreatedOn = DateTime.Today,
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForDetailsQuery,
                    Password = "password2",
                    Name = "name2",
                    BirthDay = new DateTime(2000, 2, 13),
                    Gender = 1,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today,
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForUpdate,
                    Password = "password3",
                    Name = "name3",
                    BirthDay = new DateTime(2000, 2, 13),
                    Gender = 1,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForUpdateExeption,
                    Password = "password4",
                    Name = "name4",
                    BirthDay = new DateTime(2001, 1, 15),
                    Gender = 1,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today,
                    RevokedBy = UserAdminLogin,
                    RevokedOn = DateTime.Now
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForUpdateYourself,
                    Password = "password5",
                    Name = "name5",
                    BirthDay = new DateTime(1990, 7, 27),
                    Gender = 2,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForUpdateYourselfExeption,
                    Password = "password6",
                    Name = "name6",
                    BirthDay = new DateTime(2004, 4, 1),
                    Gender = 3,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today,
                    RevokedBy = UserAdminLogin,
                    RevokedOn = DateTime.Now

                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = UserLoginForUpdateRecovery,
                    Password = "password7",
                    Name = "name7",
                    BirthDay = new DateTime(1990, 7, 27),
                    Gender = 2,
                    Admin = false,
                    CreatedBy = UserAdminLogin,
                    CreatedOn = DateTime.Today,
                    RevokedBy = UserAdminLogin,
                    RevokedOn = DateTime.Now
                }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(UsersDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
