using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;

namespace Brewterates.Infrastructre.Repositories
{
    public class QuoteItemRepository : RepositoryBase<QuoteItem>, IQuoteItemRepository
    {
        public QuoteItemRepository(brewteratesDbContext _context) : base(_context) { }
    }
}
