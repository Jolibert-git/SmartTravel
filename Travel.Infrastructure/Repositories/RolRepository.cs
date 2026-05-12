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
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(DbContext context) : base(context) { }

        public async Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(r => r.NameRol == name, cancellationToken);

        public IQueryable<Rol> GetByUserId(long userId)
             => _dbSet.AsNoTracking()
                      .Where(r => r.RolUsers.Any(ru => ru.IdSystemUser == userId));


        public async Task AddRolUserAsync(RolUser entity, CancellationToken cancellationToken = default)
            => await _context.Set<RolUser>().AddAsync(entity, cancellationToken);
    }
}
