using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IDetailPackageRepository : IGenericRepository<DetailPackage>
    {
        /// <summary>IQueryable de detalles de un paquete.</summary>
        IQueryable<DetailPackage> GetByPackage(long packageId);

        /// <summary>Elimina todos los detalles de un paquete para reemplazarlos.</summary>
        Task DeleteByPackageAsync(long packageId, CancellationToken cancellationToken = default);
    }
}
