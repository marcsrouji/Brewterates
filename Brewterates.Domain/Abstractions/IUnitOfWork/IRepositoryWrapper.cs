using Brewterates.Application.Abstractions;
using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Abstractions.IUnitOfWork
{
    public interface IRepositoryWrapper
    {
        public IBeerRepository BeerRepository { get; }
        public IBreweryRepository BreweryRepository { get; }
        public IWholesalerBeerCatalogRepository WholesalerBeerCatalogRepository { get; }
        public IWholesalerRepository WholesalerRepository { get; }
        public IWholesalerStockRepository WholesalerStockRepository { get; }
        public IQuoteRepository QuoteRepository { get; }
        public IQuoteItemRepository QuoteItemRepository { get; }
        public brewteratesDbContext Context { get; }
        void Save();
        void Commit();
        void Rollback();
        void BeginTransaction();
    }
}
