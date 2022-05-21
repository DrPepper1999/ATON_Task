using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.UpdateUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class UpdateUserCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateUserCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateUserCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdate;
            var updateLogin = "newLoginUpdate";
            var updatePasword = "newPassword";
            var updateName = "newName";
            var updateGender = 2;
            var updateBirthDay = new DateTime(2007, 12, 12);


            // Act
            await handler.Handle(new UpdateUserCommand
            {
                UserLogin = userLogin,
                Login = updateLogin,
                Password = updatePasword,
                Name = updateName,
                Gender = updateGender,
                BirthDay = updateBirthDay,
                ModifiedBy = UsersContextFactory.UserAdminLogin
                
            }, CancellationToken.None);

            // Assert
            var updateUser = Context.Users.SingleOrDefault(user =>
            user.Login == updateLogin &&
            user.Password == updatePasword && user.Name == updateName &&
            user.Gender == updateGender && user.BirthDay == updateBirthDay);
            Assert.NotNull(updateUser);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_FailOnWrongNonExistentLogin()
        {
            // Arrange
            var handler = new UpdateUserCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateUserCommand
                    {
                        UserLogin = "nonExistentLogin",
                        Name = "newName"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateUserCommandHandler_FailOnWrongLoginNotFound()
        {
            // Arrange
            var handler = new UpdateUserCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdateExeption;
            var updateLogin = UsersContextFactory.UserAdminLogin;

            // Act
            // Assert
            await Assert.ThrowsAsync<NonUniqueFieldException>(async () =>
                await handler.Handle(
                    new UpdateUserCommand
                    {
                        UserLogin = userLogin,
                        Login = updateLogin,
                        Name = "newName"
                    },
                    CancellationToken.None));
        }


        [Fact]
        public async Task UpdateUserCommandHandler_FailOnWrongUserRevoked()
        {
            // Arrange
            var handler = new UpdateUserCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdateExeption;

            // Act
            // Assert
            await Assert.ThrowsAsync<UserRevokedException>(async () =>
                await handler.Handle(
                    new UpdateUserCommand
                    {
                        UserLogin = userLogin,
                        Name = "newName"
                    },
                    CancellationToken.None));
        }
    }
}
