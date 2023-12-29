using Brewterates.Application.Abstractions;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.Repositories
{
    public class WholesalerBeerCatalogRepository : RepositoryBase<WholesalerBeerCatalog>, IWholesalerBeerCatalogRepository
    {
        public WholesalerBeerCatalogRepository(brewteratesDbContext context) : base(context) { }
    }
}
