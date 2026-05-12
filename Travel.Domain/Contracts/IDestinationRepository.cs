using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IDestinationRepository : IGenericRepository<Destination>
    {
        /// <summary>IQueryable de destinos filtrados por país.</summary>
        IQueryable<Destination> GetByCountry(long countryId);

        /// <summary>IQueryable del destino con país, hoteles y aeropuertos.</summary>
        IQueryable<Destination> GetWithDetails();

        /// <summary>IQueryable filtrado por ciudad (búsqueda parcial).</summary>
        IQueryable<Destination> SearchByCity(string city);
    }
}
