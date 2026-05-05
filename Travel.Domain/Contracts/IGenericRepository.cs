using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        // ----------------------------------------------------------------
        // READ — IQueryable (AsNoTracking activado)
        // El llamador agrega .Where(), .Include(), .OrderBy(), .Select()
        // antes de ejecutar con .ToListAsync() o .FirstOrDefaultAsync()
        // ----------------------------------------------------------------

        /// <summary>
        /// Retorna un IQueryable con AsNoTracking sobre toda la tabla.
        /// Punto de entrada para cualquier consulta personalizada.
        /// </summary>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Retorna un IQueryable con AsNoTracking + filtro base aplicado.
        /// </summary>
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate);

        // ----------------------------------------------------------------
        // Helpers de ejecución — evitan repetir .AnyAsync / .CountAsync
        // fuera del repositorio
        // ----------------------------------------------------------------

        /// <summary>
        /// Busca por llave primaria. Sin tracking.
        /// </summary>
        Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica si existe al menos un registro que cumple el predicado.
        /// </summary>
        Task<bool> ExistsAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cuenta registros. Si no se pasa predicado cuenta todos.
        /// </summary>
        Task<int> CountAsync(
            Expression<Func<T, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        // ----------------------------------------------------------------
        // WRITE
        // ----------------------------------------------------------------

        /// <summary>
        /// Agrega una nueva entidad al contexto (requiere SaveChanges).
        /// </summary>
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega una colección de entidades (bulk insert).
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marca una entidad como modificada.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Marca una colección de entidades como modificadas.
        /// </summary>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad — hard delete real en BD.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Elimina una colección de entidades — hard delete bulk.
        /// </summary>
        void DeleteRange(IEnumerable<T> entities);
    }
}
