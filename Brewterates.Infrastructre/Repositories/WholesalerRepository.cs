using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.Repositories
{
    public class WholesalerRepository : RepositoryBase<Wholesaler>, IWholesalerRepository
    {
        public WholesalerRepository(brewteratesDbContext context) : base(context) { }
    }
}
