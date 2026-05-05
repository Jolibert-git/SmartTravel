using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ISeatClassRepository : IGenericRepository<SeatClass>
    {
        /// <summary>Busca una clase de asiento por nombre.</summary>
        Task<SeatClass?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
