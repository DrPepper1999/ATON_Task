using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.UpdateYourself;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class UpdateYourselfCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateYourselfCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateYourselfCommandHandler(Context);
            var currentUserLogin = UsersContextFactory.UserLoginForUpdateYourself;
            var updateLogin = "newLoginYourself";
            var updatePasword = "newPasswordYourself";
            var updateName = "newNameYourself";
            var updateGender = 1;
            var updateBirthDay = new DateTime(2014, 3, 12);


            // Act
            await handler.Handle(new UpdateYourselfCommand
            {
                CurrentUserLogin = currentUserLogin,
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
        public async Task UpdateYourselfCommandHandler_FailOnWrongNonExistentLogin()
        {
            // Arrange
            var handler = new UpdateYourselfCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateYourselfCommand
                    {
                        CurrentUserLogin = "nonExistentLogin",
                        Name = "newName"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateYourselfCommandHandler_FailOnWrongLoginNotFound()
        {
            // Arrange
            var handler = new UpdateYourselfCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdateYourselfExeption;
            var updateLogin = UsersContextFactory.UserAdminLogin;

            // Act
            // Assert
            await Assert.ThrowsAsync<NonUniqueFieldException>(async () =>
                await handler.Handle(
                    new UpdateYourselfCommand
                    {
                        CurrentUserLogin = userLogin,
                        Login = updateLogin,
                        Name = "newName"
                    },
                    CancellationToken.None));
        }


        [Fact]
        public async Task UpdateYourselfCommandHandler_FailOnWrongUserRevoked()
        {
            // Arrange
            var handler = new UpdateYourselfCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdateYourselfExeption;

            // Act
            // Assert
            await Assert.ThrowsAsync<UserRevokedException>(async () =>
                await handler.Handle(
                    new UpdateYourselfCommand
                    {
                        CurrentUserLogin = userLogin,
                        Name = "newName"
                    },
                    CancellationToken.None));
        }
    }
}
