using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Users.Application.Interfaces;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserListQueryHandler(IUserDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<UserListVm> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var UsersQuery = await _dbContext.Users
                .OrderBy(user => user.CreatedOn)
                .Where(user => user.RevokedOn == DateTime.MinValue)
                .ProjectTo<UserLoockupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UserListVm() { Users = UsersQuery };
        }
    }
}
