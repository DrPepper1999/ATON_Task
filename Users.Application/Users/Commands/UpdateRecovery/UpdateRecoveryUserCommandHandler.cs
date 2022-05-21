using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.UpdateRecovery
{
    public class UpdateRecoveryUserCommandHandler : IRequestHandler<UpdateRecoveryUserCommand>
    {
        private readonly IUserDbContext _dbContext;
        public UpdateRecoveryUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(UpdateRecoveryUserCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login,
                cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            entity.RevokedBy = "";
            entity.RevokedOn = DateTime.MinValue;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
