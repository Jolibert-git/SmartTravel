using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
        /// <summary>Busca un tipo de documento por nombre.</summary>
        Task<DocumentType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>IQueryable con los pasajeros que usan cada tipo de documento.</summary>
        IQueryable<DocumentType> GetWithPassengers();
    }
}
