using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Application.Users.Queries.GetUserGreaterThen;

namespace Users.Application.Users.Queries.GetUserListGreaterThen
{
    public class GetUserListGreaterThenQueryHandler
        : IRequestHandler<GetUserListGreaterThenQuery, UserListGreaterThenVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserListGreaterThenQueryHandler(IUserDbContext dbContext, IMapper mapper) =>
    (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserListGreaterThenVm> Handle(GetUserListGreaterThenQuery request,
            CancellationToken cancellationToken)
        {
            var UsersQuery = await _dbContext.Users
               .ProjectTo<UserGreateThenLookupDto>(_mapper.ConfigurationProvider)
               .Where(user => user.BirthDay != null)
               .ToListAsync(cancellationToken);
            UsersQuery = UsersQuery.Where(user => GetAgeUser(user.BirthDay) > request.Age).ToList();
            return new UserListGreaterThenVm() { Users = UsersQuery };
        }

        private static int? GetAgeUser(DateTime? birthDate)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - birthDate.Value.Year;
            if (birthDate.Value.AddYears(age) > today)
            {
                age--;
            }
            return age;
        }
    }
}
