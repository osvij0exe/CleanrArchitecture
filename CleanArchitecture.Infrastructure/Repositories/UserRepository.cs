using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;




namespace CleanArchitecture.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User,UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) 
            : base(context)
        {
            // aqui solo va la logica personalizada del la clase user
    }

        public async Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
        {
            return await DbContext
        }
    }
}
