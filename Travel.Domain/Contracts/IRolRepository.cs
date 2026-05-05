using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IRolRepository: IGenericRepository<Rol>
    {
        /// <summary>Busca un rol por su nombre exacto.</summary>
        Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>Retorna todos los roles de un usuario.</summary>
        /*Task<IEnumerable<Rol>>*/
        IQueryable<Rol> GetByUserId(long userId);

        Task AddRolUserAsync(RolUser entity, CancellationToken cancellationToken = default);
    }
}
