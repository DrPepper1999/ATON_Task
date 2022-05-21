using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserDbContext _dbContext;

        public UpdateUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.Where(user => user.Login == request.Login)
                .AnyAsync(cancellationToken))
            {
                throw new NonUniqueFieldException(nameof(User), request.Login);
            }

            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Login == request.UserLogin, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.UserLogin);
            }

            if (entity.RevokedOn != DateTime.MinValue)
            {
                throw new UserRevokedException(nameof(User), request.UserLogin);
            }

            entity.Login = request.Login ?? entity.Login;
            entity.Password = request.Password ?? entity.Password;
            entity.Name = request.Name ?? entity.Name;
            entity.Gender = request.Gender ?? entity.Gender;
            entity.BirthDay = request.BirthDay;
            entity.ModifiedBy = request.ModifiedBy;
            entity.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
