using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.Repositories
{
    public class QuoteRepository : RepositoryBase<Quote>, IQuoteRepository
    {
        public QuoteRepository(brewteratesDbContext context) : base(context) { }
    }
}
