using Brewterates.Application.Abstractions;
using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Infrastructre.UnitOfWork
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly brewteratesDbContext _context;
        private readonly IDbContextTransaction _dbTransaction;
        private Dictionary<Type, object> _repositories;

        public RepositoryWrapper(brewteratesDbContext context)
            => (_context, _repositories) = (context, new Dictionary<Type, object>());
        
        
        public brewteratesDbContext Context { get => _context; }
        public IBeerRepository BeerRepository { get => GetRepository<IBeerRepository>(); }
        public IBreweryRepository BreweryRepository { get => GetRepository<IBreweryRepository>(); }
        public IWholesalerBeerCatalogRepository WholesalerBeerCatalogRepository { get => GetRepository<IWholesalerBeerCatalogRepository>(); }
        public IWholesalerRepository WholesalerRepository { get => GetRepository<IWholesalerRepository>(); }
        public IWholesalerStockRepository WholesalerStockRepository { get => GetRepository<IWholesalerStockRepository>(); }
        public IQuoteRepository QuoteRepository { get => GetRepository<IQuoteRepository>(); }
        public IQuoteItemRepository QuoteItemRepository { get => GetRepository<IQuoteItemRepository>(); }

        private TRepositoryInterface GetRepository<TRepositoryInterface>() where TRepositoryInterface : class
        {
            var type = typeof(TRepositoryInterface);

            if (!_repositories.ContainsKey(type))
            {
                var concreteType = FindConcreteType(type);

                if (concreteType != null)
                {
                    var repositoryInstance = Activator.CreateInstance(concreteType, _context);
                    _repositories.Add(type, repositoryInstance);
                }
                else
                {
                    throw new InvalidOperationException($"No concrete implementation found for the interface {type.Name}.");
                }
            }

            return (TRepositoryInterface)_repositories[type];
        }

        private Type FindConcreteType(Type interfaceType)
        {
            var concreteTypeName = interfaceType.Name.Substring(1); // Assuming the 'I' prefix convention
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes().FirstOrDefault(t => t.Name == concreteTypeName);
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
