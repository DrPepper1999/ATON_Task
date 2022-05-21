using System;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserListGreaterThen
{
    public class UserListGreaterThenVm : IMapWith<User>
    {
        public IList<UserGreateThenLookupDto> Users { get; set; }
    }
}
