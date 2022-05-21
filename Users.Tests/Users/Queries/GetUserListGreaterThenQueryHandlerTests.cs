using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Shouldly;
using Users.Application.Users.Queries.GetUserGreaterThen;
using Users.Persistence;
using Users.Tests.Common;
using Users.Application.Users.Queries.GetUserListGreaterThen;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserListGreaterThenQueryHandlerTests
    {
        private readonly UsersDbContext Context;
        private readonly IMapper Mapper;

        public GetUserListGreaterThenQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetUserListQueryHandler_Success()
        {
            // Arrage
            var handler = new GetUserListGreaterThenQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetUserListGreaterThenQuery
                {
                    Age = 22
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserListGreaterThenVm>();
            result.Users.Count.ShouldBe(3);
        }
    }
}
