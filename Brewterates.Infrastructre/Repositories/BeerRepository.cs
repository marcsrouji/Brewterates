using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;

namespace Brewterates.Infrastructre.Repositories
{
    public class BeerRepository : RepositoryBase<Beer>, IBeerRepository
    {
        public BeerRepository(brewteratesDbContext _context) : base(_context) { }
    }
}
