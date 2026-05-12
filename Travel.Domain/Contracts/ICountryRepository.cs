using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        /// <summary>Busca un país por código ISO.</summary>
        Task<Country?> GetByIsoCodeAsync(string isoCode, CancellationToken cancellationToken = default);

        /// <summary>Verifica si el código ISO ya existe.</summary>
        Task<bool> IsoCodeExistsAsync(string isoCode, CancellationToken cancellationToken = default);
    }
}
