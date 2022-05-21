using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Domain;

namespace Users.Application.Login.Queries.GetToken
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, TokenVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public GetTokenQueryHandler(IUserDbContext dbContext, IMapper mapper, ITokenService tokenService) =>
            (_dbContext, _mapper, _tokenService) = (dbContext, mapper, tokenService);

        public async Task<TokenVm> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Login == request.Login &&
                user.Password == request.Password, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }
            
            var token = _tokenService.BuildToken(request.JwtKey, request.JwtIssuer,
                request.JwtAudience, entity);

            return new TokenVm { Token = token };
        }
    }
}
