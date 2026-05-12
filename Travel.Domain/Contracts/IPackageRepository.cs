using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        /// <summary>IQueryable de paquetes con oferta vigente hoy.</summary>
        IQueryable<Package> GetActive();

        /// <summary>IQueryable del paquete con todos sus servicios.</summary>
        IQueryable<Package> GetWithDetails();

        /// <summary>IQueryable de paquetes que vencen en los próximos N días.</summary>
        IQueryable<Package> GetExpiring(int days);
    }
}
