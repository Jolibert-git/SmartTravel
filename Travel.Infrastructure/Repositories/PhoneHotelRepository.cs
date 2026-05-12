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
    public class PhoneHotelRepository : GenericRepository<PhoneHotel>, IPhoneHotelRepository
    {
        public PhoneHotelRepository(DbContext context) : base(context) { }

        public IQueryable<PhoneHotel> GetByHotel(long hotelId)
            => _dbSet.AsNoTracking()
                     .Where(ph => ph.IdHotel == hotelId);

        public async Task<bool> PhoneExistsAsync(long hotelId, string phoneNumber, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(ph =>
                               ph.IdHotel == hotelId &&
                               ph.PhoneNumber == phoneNumber,
                               cancellationToken);

        public async Task DeleteByHotelAsync(long hotelId, CancellationToken cancellationToken = default)
        {
            var items = await _dbSet.Where(ph => ph.IdHotel == hotelId)
                                    .ToListAsync(cancellationToken);
            _dbSet.RemoveRange(items);
        }
    }
}
