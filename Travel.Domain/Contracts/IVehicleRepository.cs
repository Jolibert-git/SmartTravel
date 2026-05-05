using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        /// <summary>IQueryable de vehículos en un destino.</summary>
        IQueryable<Vehicle> GetByDestination(long destinationId);

        /// <summary>IQueryable con servicio y destino incluidos.</summary>
        IQueryable<Vehicle> GetWithDetails();

        /// <summary>IQueryable filtrado por tipo de transmisión.</summary>
        IQueryable<Vehicle> GetByTransmission(string transmission);
    }
}
