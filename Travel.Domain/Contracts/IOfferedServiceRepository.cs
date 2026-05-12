using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IOfferedServiceRepository : IGenericRepository<OfferedService>
    {
        /// <summary>IQueryable filtrado por tipo de servicio.</summary>
        IQueryable<OfferedService> GetByType(long typeServiceId);

        /// <summary>IQueryable filtrado por proveedor.</summary>
        IQueryable<OfferedService> GetBySupplier(long supplierId);

        /// <summary>IQueryable de servicios disponibles en una fecha.</summary>
        IQueryable<OfferedService> GetAvailable(DateTime date);

        /// <summary>IQueryable con tipo y proveedor incluidos.</summary>
        IQueryable<OfferedService> GetWithDetails();

        /// <summary>IQueryable filtrado por rango de precio.</summary>
        IQueryable<OfferedService> GetByPriceRange(decimal min, decimal max);
    }
}
