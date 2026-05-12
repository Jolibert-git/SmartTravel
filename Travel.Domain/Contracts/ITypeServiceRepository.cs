using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface ITypeServiceRepository : IGenericRepository<TypeService>
    {
        /// <summary>IQueryable con la cantidad de servicios por tipo.</summary>
        IQueryable<TypeService> GetWithServices();
    }
}
