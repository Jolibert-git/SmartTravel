using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface ISupplierService
    {
        Task<SupplierResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SupplierResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SupplierResponseDto> CreateAsync(CreateSupplierRequest request, CancellationToken cancellationToken = default);
        Task<SupplierResponseDto> UpdateAsync(long id, UpdateSupplierRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
