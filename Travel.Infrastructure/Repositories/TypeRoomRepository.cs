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
    public class TypeRoomRepository : GenericRepository<TypeRoom>, ITypeRoomRepository
    {
        public TypeRoomRepository(DbContext context) : base(context) { }

        public async Task<TypeRoom?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(tr => tr.TypeDescription == description, cancellationToken);

        public IQueryable<TypeRoom> GetWithRooms()
            => _dbSet.AsNoTracking()
                     .Include(tr => tr.Rooms)
                         .ThenInclude(r => r.Hotel);
    }
}
