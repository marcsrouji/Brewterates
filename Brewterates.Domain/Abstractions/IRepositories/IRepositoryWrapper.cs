using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Abstractions.IRepositories
{
    public interface IRepositoryWrapper
    {
        public IBeerRepository BeerRepository { get; }
        public IBreweryRepository BreweryRepository { get; }
        public brewteratesDbContext Context { get; }
        void Save();
        void Commit();
        void Rollback();
        void BeginTransaction();
    }
}
