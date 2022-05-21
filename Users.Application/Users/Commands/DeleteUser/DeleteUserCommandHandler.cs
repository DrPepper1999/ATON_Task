using System;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Users.Application.Common.Exceptions;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserDbContext _dbContext;

        public DeleteUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Login == request.Login, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            if (request.IsFullDeleted)
            {
                _dbContext.Users.Remove(entity);
            }
            else
            {
                entity.RevokedOn = DateTime.Now;
                entity.RevokedBy = request.RevokedBy;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
