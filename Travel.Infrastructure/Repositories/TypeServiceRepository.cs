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
    public class TypeServiceRepository : GenericRepository<TypeService>, ITypeServiceRepository
    {
        public TypeServiceRepository(DbContext context) : base(context) { }

        public IQueryable<TypeService> GetWithServices()
            => _dbSet.AsNoTracking()
                     .Include(ts => ts.OfferedServices);
    }
}
