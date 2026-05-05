using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IServiceAvailabilityRepository : IGenericRepository<ServiceAvailability>
    {
        /// <summary>IQueryable del historial de disponibilidad de un servicio.</summary>
        IQueryable<ServiceAvailability> GetByService(long serviceId);

        /// <summary>Verifica si un servicio está disponible en un rango de fechas.</summary>
        Task<bool> IsServiceAvailableAsync(long serviceId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

        /// <summary>Retorna el registro de disponibilidad actual de un servicio.</summary>
        Task<ServiceAvailability?> GetCurrentByServiceAsync(long serviceId, CancellationToken cancellationToken = default);
    }


}
