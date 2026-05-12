using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ITypeRoomRepository : IGenericRepository<TypeRoom>
    {
        /// <summary>Busca un tipo de habitación por descripción.</summary>
        Task<TypeRoom?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default);

        /// <summary>IQueryable con las habitaciones que pertenecen a cada tipo.</summary>
        IQueryable<TypeRoom> GetWithRooms();
    }
}
