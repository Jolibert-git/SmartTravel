using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Contracts;

namespace Travel.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Implementación genérica del repositorio.
        /// Todas las lecturas exponen IQueryable con AsNoTracking().
        /// El llamador decide qué filtros, includes y proyecciones aplicar
        /// antes de ejecutar con .ToListAsync() o .FirstOrDefaultAsync().
        /// CancellationToken en todos los métodos async.
        /// Hard delete en todos los removes.
        /// </summary>
        /// 

        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ----------------------------------------------------------------
        // IQueryable — AsNoTracking activado, el llamador construye el query
        // ----------------------------------------------------------------

        public IQueryable<T> GetQueryable()
            => _dbSet.AsNoTracking();

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
            => _dbSet.AsNoTracking().Where(predicate);

        // ----------------------------------------------------------------
        // Helpers de ejecución
        // ----------------------------------------------------------------

        public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(
                               e => EF.Property<long>(e, "Id") == id,
                               cancellationToken);

        public async Task<bool> ExistsAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(predicate, cancellationToken);

        public async Task<int> CountAsync(
            Expression<Func<T, bool>>? predicate = null,
            CancellationToken cancellationToken = default)
            => predicate is null
                ? await _dbSet.AsNoTracking().CountAsync(cancellationToken)
                : await _dbSet.AsNoTracking().CountAsync(predicate, cancellationToken);

        // ----------------------------------------------------------------
        // WRITE
        // ----------------------------------------------------------------

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbSet.AddAsync(entity, cancellationToken);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _dbSet.AddRangeAsync(entities, cancellationToken);

        public void Update(T entity)
            => _dbSet.Update(entity);

        public void UpdateRange(IEnumerable<T> entities)
            => _dbSet.UpdateRange(entities);

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities)
            => _dbSet.RemoveRange(entities);

    }
}
