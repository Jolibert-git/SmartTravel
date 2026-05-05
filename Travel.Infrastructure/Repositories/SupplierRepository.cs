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
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context) { }

        public async Task<Supplier?> GetByRncAsync(string rnc, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(s => s.Rnc == rnc, cancellationToken);

        public async Task<bool> RncExistsAsync(string rnc, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(s => s.Rnc == rnc, cancellationToken);

        public IQueryable<Supplier> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(s => s.PhoneSuppliers)
                     .Include(s => s.OfferedServices);
    }
}
