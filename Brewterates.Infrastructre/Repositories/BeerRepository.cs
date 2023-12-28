using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using Brewterates.Infrastructre.UnitOfWork;

namespace Brewterates.Infrastructre.Repositories
{
    public class BeerRepository : RepositoryBase<Beer>, IBeerRepository
    {
        public BeerRepository(brewteratesDbContext _context) : base(_context) { }
    }
}
