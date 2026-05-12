using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface ICatalogService
    {
        // TypeService
        Task<IEnumerable<TypeServiceDto>> GetAllTypeServicesAsync(CancellationToken cancellationToken = default);
        Task<TypeServiceDto> CreateTypeServiceAsync(CreateTypeServiceRequest request, CancellationToken cancellationToken = default);
        Task DeleteTypeServiceAsync(long id, CancellationToken cancellationToken = default);

        // TypeRoom
        Task<IEnumerable<TypeRoomDto>> GetAllTypeRoomsAsync(CancellationToken cancellationToken = default);
        Task<TypeRoomDto> CreateTypeRoomAsync(CreateTypeRoomRequest request, CancellationToken cancellationToken = default);
        Task DeleteTypeRoomAsync(long id, CancellationToken cancellationToken = default);

        // Country
        Task<IEnumerable<CountryDto>> GetAllCountriesAsync(CancellationToken cancellationToken = default);
        Task<CountryDto> GetCountryByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<CountryDto> CreateCountryAsync(CreateCountryRequest request, CancellationToken cancellationToken = default);

        // SeatClass
        Task<IEnumerable<SeatClassDto>> GetAllSeatClassesAsync(CancellationToken cancellationToken = default);

        // DocumentType
        Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypesAsync(CancellationToken cancellationToken = default);

        // DiscountType
        Task<IEnumerable<DiscountTypeDto>> GetAllDiscountTypesAsync(CancellationToken cancellationToken = default);

        // PaymentMethod
        Task<IEnumerable<PaymentMethodDto>> GetActivePaymentMethodsAsync(CancellationToken cancellationToken = default);
    }
}
