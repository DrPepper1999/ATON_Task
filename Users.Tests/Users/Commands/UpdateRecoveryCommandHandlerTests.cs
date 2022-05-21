using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.UpdateRecovery;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class UpdateRecoveryCommandHandlerTests : TestCommandBase
    {

        [Fact]
        public async Task UpdateRecoveryCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateRecoveryUserCommandHandler(Context);
            var userLogin = UsersContextFactory.UserLoginForUpdateRecovery;

            // Act
            await handler.Handle(new UpdateRecoveryUserCommand
            {
                Login = userLogin

            }, CancellationToken.None);

            // Assert
            var updateUser = Context.Users.SingleOrDefault(user =>
            user.Login == userLogin);
            Assert.NotNull(updateUser);
            Assert.Equal(updateUser.RevokedBy, "");
            Assert.Equal(updateUser.RevokedOn, DateTime.MinValue);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_FailOnWrongNonExistentLogin()
        {
            // Arrange
            var handler = new UpdateRecoveryUserCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateRecoveryUserCommand
                    {
                        Login = "nonExistentLogin",
                    },
                    CancellationToken.None));
        }
    }
}
