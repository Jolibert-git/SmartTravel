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
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(DbContext context) : base(context) { }

        public async Task<DocumentType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(dt => dt.DocumentName == name, cancellationToken);

        public IQueryable<DocumentType> GetWithPassengers()
            => _dbSet.AsNoTracking()
                     .Include(dt => dt.Passengers);
    }
}
