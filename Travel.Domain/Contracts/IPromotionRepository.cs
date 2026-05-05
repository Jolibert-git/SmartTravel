using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPromotionRepository : IGenericRepository<Promotion>
    {
        /// <summary>IQueryable de promociones activas y vigentes hoy.</summary>
        IQueryable<Promotion> GetActive();

        /// <summary>IQueryable de promociones aplicables a un servicio.</summary>
        IQueryable<Promotion> GetByService(long serviceId);

        /// <summary>IQueryable de promociones aplicables a un paquete.</summary>
        IQueryable<Promotion> GetByPackage(long packageId);

        /// <summary>IQueryable con detalle de servicios/paquetes incluido.</summary>
        IQueryable<Promotion> GetWithDetails();
    }
}
