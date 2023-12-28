using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using Brewterates.Infrastructre.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.Repositories
{
    public class BreweryRepository : RepositoryBase<Brewery>, IBreweryRepository
    {
        public BreweryRepository(brewteratesDbContext context) : base (context) { }
    }
}
