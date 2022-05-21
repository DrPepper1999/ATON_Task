using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.DeleteUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task FullDeleteUserCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteUserCommand()
            {
                Login = UsersContextFactory.UserLoginForFullDelete,
                IsFullDeleted = true,
                RevokedBy = UsersContextFactory.UserAdminLogin
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Users.SingleOrDefault(user =>
            user.Login == UsersContextFactory.UserLoginForFullDelete));
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteUserCommand()
            {
                Login = UsersContextFactory.UserLoginForSoftDelete,
                IsFullDeleted = false,
                RevokedBy = UsersContextFactory.UserAdminLogin
            }, CancellationToken.None);

            // Assert
            var deletedUser = Context.Users.SingleOrDefault(user =>
            user.Login == UsersContextFactory.UserLoginForSoftDelete);
            Assert.NotNull(deletedUser);
            Assert.Equal(deletedUser.RevokedBy, UsersContextFactory.UserAdminLogin);
        }

        [Fact]
        public async Task DeleteUserCommandHandler_FailOnWrongLogin()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                 await handler.Handle(new DeleteUserCommand()
                 {
                     Login = "nonExistentLogin",
                     IsFullDeleted = false,
                     RevokedBy = UsersContextFactory.UserAdminLogin
                 }, CancellationToken.None)
            );
        }
    }
}
