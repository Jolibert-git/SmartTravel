using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ISystemsUserRepository : IGenericRepository<SystemsUser>
    {
        /// <summary>Busca un usuario por email (login).</summary>
        Task<SystemsUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>Verifica si el email ya está registrado.</summary>
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>IQueryable del usuario con roles — el llamador decide qué incluir.</summary>
        IQueryable<SystemsUser> GetWithRoles();

        /// <summary>IQueryable paginado ordenado por apellido.</summary>
        IQueryable<SystemsUser> GetPaged(int page, int pageSize);
    }
}
