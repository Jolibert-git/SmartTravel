using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPhoneSupplierRepository : IGenericRepository<PhoneSupplier>
    {
        /// <summary>IQueryable de teléfonos de un proveedor.</summary>
        IQueryable<PhoneSupplier> GetBySupplier(long supplierId);

        /// <summary>Verifica si un número ya está registrado para ese proveedor.</summary>
        Task<bool> PhoneExistsAsync(long supplierId, string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>Elimina todos los teléfonos de un proveedor (para reemplazarlos).</summary>
        Task DeleteBySupplierAsync(long supplierId, CancellationToken cancellationToken = default);
    }
}
