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
    public class PhoneSupplierRepository : GenericRepository<PhoneSupplier>, IPhoneSupplierRepository
    {
        public PhoneSupplierRepository(DbContext context) : base(context) { }

        public IQueryable<PhoneSupplier> GetBySupplier(long supplierId)
            => _dbSet.AsNoTracking()
                     .Where(ps => ps.IdSupplier == supplierId);

        public async Task<bool> PhoneExistsAsync(long supplierId, string phoneNumber, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(ps =>
                               ps.IdSupplier == supplierId &&
                               ps.PhoneNumber == phoneNumber,
                               cancellationToken);

        public async Task DeleteBySupplierAsync(long supplierId, CancellationToken cancellationToken = default)
        {
            var items = await _dbSet.Where(ps => ps.IdSupplier == supplierId)
                                    .ToListAsync(cancellationToken);
            _dbSet.RemoveRange(items);
        }
    }
}
