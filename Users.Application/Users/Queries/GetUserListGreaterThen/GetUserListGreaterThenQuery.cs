using System;
using MediatR;
using Users.Application.Users.Queries.GetUserListGreaterThen;

namespace Users.Application.Users.Queries.GetUserGreaterThen
{
    public class GetUserListGreaterThenQuery : IRequest<UserListGreaterThenVm>
    {
        public int Age { get; set; }
    }
}
