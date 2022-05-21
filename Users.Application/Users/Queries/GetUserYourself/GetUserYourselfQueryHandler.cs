using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserYourself
{
    public class GetUserYourselfQueryHandler : IRequestHandler<GetUserYourselfQuery, UserYourselfDataVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserYourselfQueryHandler(IUserDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<UserYourselfDataVm> Handle(GetUserYourselfQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Login == request.Login, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            if (entity.RevokedOn != DateTime.MinValue)
            {
                throw new UserRevokedException(nameof(User), request.Login);
            }

            return _mapper.Map<UserYourselfDataVm>(entity);
        }
    }
}
