using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        /// <summary>Busca un proveedor por RNC.</summary>
        Task<Supplier?> GetByRncAsync(string rnc, CancellationToken cancellationToken = default);

        /// <summary>Verifica si el RNC ya existe.</summary>
        Task<bool> RncExistsAsync(string rnc, CancellationToken cancellationToken = default);

        /// <summary>IQueryable del proveedor con teléfonos y servicios.</summary>
        IQueryable<Supplier> GetWithDetails();
    }
}
