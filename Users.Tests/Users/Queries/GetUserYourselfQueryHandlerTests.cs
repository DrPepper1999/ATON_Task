using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using Xunit;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Queries.GetUserYourself;
using Users.Persistence;
using Users.Tests.Common;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserYourselfQueryHandlerTests
    {
        private readonly UsersDbContext Context;
        private readonly IMapper Mapper;

        public GetUserYourselfQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetUserYourselfQueryHandlerTests_Success()
        {
            // Arrange
            var handler = new GetUserYourselfQueryHandler(Context, Mapper);
            var login = UsersContextFactory.UserLoginForDetailsQuery;

            // Act
            var result = await handler.Handle(new GetUserYourselfQuery
            {
                Login = login
            },
            CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserYourselfDataVm>();
            result.Name.ShouldBe("name2");
            result.Gender.ShouldBe(1);
            result.BirthDay.ShouldBe(new DateTime(2000, 2, 13));
            result.Admin.ShouldBe(false);
            result.CreatedBy.ShouldBe(UsersContextFactory.UserAdminLogin);
            result.CreatedOn.ShouldBe(DateTime.Today);
            result.ModifiedBy.ShouldBe("");
            result.ModifiedOn.ShouldBe(null);
            result.RevokedBy.ShouldBe("");
            result.RevokedOn.ShouldBe(null);
        }

        [Fact]
        public async Task GetUserYourselfQueryHandlerTests_FailOnWrongLoginNotFound()
        {
            // Arrange
            var handler = new GetUserYourselfQueryHandler(Context, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetUserYourselfQuery
                    {
                        Login = "nonExistentLogin",
                    },
                    CancellationToken.None));
        }
    }
}
