using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Users.Application.Users.Commands.CreateUser;
using Users.Tests.Common;
using Users.Application.Common.Exceptions;

namespace Users.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateUserCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateUserCommandHandler(Context);
            var userLogin = "userlogin";
            var userPassword = "userPassword";
            var userName = "userName";
            var userGender = 0;
            var userBirthDay = new DateTime(2001, 1, 1);
            var userAdmin = false;

            //Act
            await handler.Handle(
                new CreateUserCommand
                {
                    Login = userLogin,
                    Password = userPassword,
                    Name = userName,
                    Gender = userGender,
                    BirthDay = userBirthDay,
                    Admin = userAdmin,
                    CreatedBy = UsersContextFactory.UserAdminLogin,
                    CreatedOn = DateTime.Now,
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Users.SingleOrDefaultAsync(user =>
            user.Login == userLogin && user.Password == userPassword &&
            user.Name == userName && user.CreatedBy == UsersContextFactory.UserAdminLogin));
        }

        [Fact]
        public async Task CreateUserCommandHandler_FailOnWrongLogin()
        {
            //Arrange
            var handler = new CreateUserCommandHandler(Context);
            var userLogin = UsersContextFactory.UserAdminLogin;
            var userPassword = "userSomePassword";
            var userName = "userSomeName";
            var userGender = 0;
            var userBirthDay = new DateTime(2001, 1, 1);
            var userAdmin = false;

            //Act
            //Assert
            await Assert.ThrowsAsync<NonUniqueFieldException>(async () =>
                await handler.Handle(
                new CreateUserCommand
                {
                    Login = userLogin,
                    Password = userPassword,
                    Name = userName,
                    Gender = userGender,
                    BirthDay = userBirthDay,
                    Admin = userAdmin,
                    CreatedBy = UsersContextFactory.UserAdminLogin,
                    CreatedOn = DateTime.Now,
                },
                CancellationToken.None));
        }
    }
}
