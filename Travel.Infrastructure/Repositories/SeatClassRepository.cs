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
    public class SeatClassRepository : GenericRepository<SeatClass>, ISeatClassRepository
    {
        public SeatClassRepository(DbContext context) : base(context) { }

        public async Task<SeatClass?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(sc => sc.ClassName == name, cancellationToken);
    }
}
