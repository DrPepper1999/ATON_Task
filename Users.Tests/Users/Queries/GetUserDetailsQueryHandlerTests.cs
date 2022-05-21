using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Shouldly;
using Users.Application.Users.Queries.GetUserDetails;
using Users.Persistence;
using Users.Tests.Common;
using Users.Application.Common.Exceptions;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserDetailsQueryHandlerTests
    {
        private readonly UsersDbContext Context;
        private readonly IMapper Mapper;

        public GetUserDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetUserDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetUserDetailsQueryHandler(Context, Mapper);
            var login = UsersContextFactory.UserLoginForDetailsQuery;

            // Act
            var result = await handler.Handle(new GetUserDetailsQuery
            {
                Login = login
            },
            CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserDetailsVm>();
            result.Name.ShouldBe("name2");
            result.Gender.ShouldBe(1);
            result.BirthDay.ShouldBe(new DateTime(2000, 2, 13));
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task GetUserDetailsQueryHandler_FailOnWrongLoginNotFound()
        {
            // Arrange
            var handler = new GetUserDetailsQueryHandler(Context, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetUserDetailsQuery
                    {
                        Login = "nonExistentLogin",
                    },
                    CancellationToken.None));
        }
    }
}
