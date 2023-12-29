using Brewterates.Application.Abstractions;
using Brewterates.Application.DTOs;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using Brewterates.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Services
{
    public class WholesalerSevice : IWholesalerSevice
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Mapper _mapper;

        public WholesalerSevice(IRepositoryWrapper repositoryWrapper, Mapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public WholesalerBeerDataDto AddWholesalerBeerToCatalog(WholesalerBeerDto wholesalerBeerDto)
        {

            WholesalerBeerCatalog? dbWholesalerBeerCatalog = _repositoryWrapper.WholesalerBeerCatalogRepository
                .GetByCondition(wb => wb.WholesalerId == wholesalerBeerDto.wholeSalerId && wb.BeerId == wholesalerBeerDto.beerId)
                .FirstOrDefault();
            if (dbWholesalerBeerCatalog is not null)
            {
                throw new CustomException("This beer already exists in your catalog.");
            }

            Beer? beer = _repositoryWrapper
                .BeerRepository
                .GetByCondition(b => b.Id == wholesalerBeerDto.beerId)
                .FirstOrDefault();

            if (beer is null)
            {
                throw new CustomException("The beer must exist.");
            }

            Wholesaler? wholesaler = _repositoryWrapper
                .WholesalerRepository
                .GetByCondition(w => w.Id == wholesalerBeerDto.wholeSalerId)
                .FirstOrDefault();

            if (wholesaler is null)
            {
                throw new CustomException("The wholesaler must exist.");
            }

            var wholesalerBeerCatalog = new WholesalerBeerCatalog()
            {
                BeerId = beer.Id,
                WholesalerId = wholesaler.Id,
                DateCreated = DateTime.Now
            };

            var wholeSalerBeerStock = new WholesalerStock()
            {
                WholesalerId = wholesaler.Id,
                BeerId = beer.Id,
                Quantity = 0,
                DateCreated = DateTime.Now
            };

            _repositoryWrapper.WholesalerBeerCatalogRepository.Create(wholesalerBeerCatalog);
            _repositoryWrapper.WholesalerStockRepository.Create(wholeSalerBeerStock);
            _repositoryWrapper.Save();

            return new WholesalerBeerDataDto
            {
                beer = _mapper.BeerToBeerDto(beer),
                wholeSaler = _mapper.WholesalerToWholeSalerDto(wholesaler)
            };
        }

        public WholesalerStock UpdateWholesalerStockQuantity(StockDto stockDto)
        {
            WholesalerStock? WholesalerStock = _repositoryWrapper
                .WholesalerStockRepository
                .GetByCondition(ws => ws.WholesalerId == stockDto.wholesalerId && ws.BeerId == stockDto.beerId)
                .FirstOrDefault();
            
            if (WholesalerStock == null)
            {
                throw new CustomException("This Beer is not in you stock.");
            }

            if (stockDto.quantity == WholesalerStock.Quantity)
            {
                return WholesalerStock;
            }

            WholesalerStock.Quantity = stockDto.quantity;

            _repositoryWrapper.WholesalerStockRepository.Update(WholesalerStock);
            _repositoryWrapper.Save();

            return WholesalerStock;
        }
    }
}
