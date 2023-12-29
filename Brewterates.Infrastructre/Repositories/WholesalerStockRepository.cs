using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.Repositories
{
    public class WholesalerStockRepository : RepositoryBase<WholesalerStock>, IWholesalerStockRepository
    {
        public WholesalerStockRepository(brewteratesDbContext context) : base(context) { }
    }
}
