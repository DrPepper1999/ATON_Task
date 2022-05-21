using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;

namespace Users.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserDbContext _dbContext;
        public CreateUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext; 
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.Where(user => user.Login == request.Login)
                .AnyAsync(cancellationToken))
            {
                throw new NonUniqueFieldException(nameof(User), request.Login);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Password = request.Password,
                Name = request.Name,
                Gender = request.Gender,
                BirthDay = request.BirthDay,
                Admin = request.Admin,
                CreatedOn = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModifiedBy = "",
                RevokedBy = ""
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return user.Id;
        }
    }
}
