using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserDetailsQueryHandler(IUserDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Login == request.Login, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            return _mapper.Map<UserDetailsVm>(entity);
        }
    }
}
