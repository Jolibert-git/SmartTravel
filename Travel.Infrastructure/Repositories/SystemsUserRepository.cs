using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;
using Travel.Domain.Contracts;

namespace Travel.Infrastructure.Repositories
{
    public class SystemsUserRepository : GenericRepository<SystemsUser>, ISystemsUserRepository
    {
        public SystemsUserRepository(DbContext context) : base(context) { }

        public async Task<SystemsUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(u => u.Email == email, cancellationToken);

        public IQueryable<SystemsUser> GetWithRoles()
            => _dbSet.AsNoTracking()
                     .Include(u => u.RolUsers)
                         .ThenInclude(ru => ru.Rol);

        public IQueryable<SystemsUser> GetPaged(int page, int pageSize)
            => _dbSet.AsNoTracking()
                     .OrderBy(u => u.LastName)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize);
    }
}
