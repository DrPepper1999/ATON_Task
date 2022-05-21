using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Shouldly;
using Users.Application.Users.Queries.GetUserList;
using Users.Persistence;
using Users.Tests.Common;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserListQueryHandlerTests
    {
        private readonly UsersDbContext Context;
        private readonly IMapper Mapper;

        public GetUserListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetUserListQueryHandler_Success()
        {
            // Arrage
            var handler = new GetUserListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetUserListQuery
                {
                    
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserListVm>();
            result.Users.Count.ShouldBe(5);
        }
    }
}
