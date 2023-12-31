using Brewterates.Application.Abstractions;
using Brewterates.Application.Abstractions.Discount;
using Brewterates.Application.DTOs;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using Brewterates.Domain.Exceptions;
using Brewterates.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IDiscountFactory _discountFactory;
        private readonly Mapper _mapper;
        public QuoteService(IRepositoryWrapper repositoryWrapper, IDiscountFactory discountFactory, Mapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _discountFactory = discountFactory;
            _mapper = mapper;
        }

        public Quote RequestQuote(QuoteDto quoteDto)
        {

            ValidateQuote(quoteDto);
            IDiscountStrategy discountStrategy = _discountFactory.GetDiscountStrategy(quoteDto.QuoteItems);
            decimal discount = discountStrategy.CalculateDiscount();

            List<Beer> beerInQuote = _repositoryWrapper
                .BeerRepository
                .GetByCondition(c => quoteDto.QuoteItems.Select(s => s.BeerId).Contains(c.Id))
                .ToList();

            var totalAmount = quoteDto.QuoteItems.Select(q =>
                q.Quantity * beerInQuote.FirstOrDefault(a => a.Id == q.BeerId)?.Price ?? 0
            ).Sum();

            var quote = new Quote()
            {
                ClientName = quoteDto.ClientName,
                WholesalerId = _repositoryWrapper.WholesalerRepository.GetByCondition(b => b.Id == quoteDto.WholesalerId).FirstOrDefault().Id,
                InitialTotalAmount = totalAmount,
                TotalAmount = totalAmount - (totalAmount * discount / 100),
                Discount = discount,
                DateCreated = DateTime.Now
            };

            _repositoryWrapper.QuoteRepository.Create(quote);
            _repositoryWrapper.Save();

            var quoteItem = quoteDto.QuoteItems.Select(qi => _mapper.QuoteItemDtoToQuoteItem(qi)).ToList();

            quoteItem.ForEach(qi => 
            {
                qi.QuoteId = quote.Id;
                _repositoryWrapper.QuoteItemRepository.Create(qi);
                WholesalerStock? stock = _repositoryWrapper.WholesalerStockRepository.GetByCondition(s => qi.BeerId == s.BeerId && s.WholesalerId == quoteDto.WholesalerId).FirstOrDefault();
                stock.Quantity -= qi.Quantity;
                _repositoryWrapper.WholesalerStockRepository.Update(stock);
            });

            _repositoryWrapper.Save();

            Quote? insertedQuote = _repositoryWrapper
                .QuoteRepository
                .GetByCondition(q => q.Id == quote.Id)
                .Include(i => i.QuoteItems.Where(w => w.QuoteId == quote.Id))
                .FirstOrDefault();

            return insertedQuote;
        }

        private void ValidateQuote(QuoteDto quoteDto) 
        {

            if (!quoteDto.QuoteItems.Any())
            {
                throw new EmptyOrderException();
            }


            Wholesaler? wholesaler = _repositoryWrapper.WholesalerRepository.GetByCondition(b => b.Id == quoteDto.WholesalerId).FirstOrDefault();
            if (wholesaler is null)
            {
                throw new WholesalerNotFoundException();
            }

            var duplicateExists = quoteDto
                .QuoteItems
                .GroupBy(g => g.BeerId)
                .Where(w => w.Count() > 1)
                .Any();

            if (duplicateExists)
            {
                throw new DuplicateInOrderException();
            }

            var lstQuoteItemStock = _repositoryWrapper
                .WholesalerStockRepository
                .GetByCondition(c => quoteDto.QuoteItems.Select(s => s.BeerId).Contains(c.BeerId))
                .ToList();

            if (lstQuoteItemStock.Count() == 0 || lstQuoteItemStock.Count() != quoteDto.QuoteItems.Count())
            {
                throw new DuplicateInOrderException();
            }

            foreach(var quoteItem in quoteDto.QuoteItems)
            {
                var itemQuantity = lstQuoteItemStock.Where(w => w.BeerId == quoteItem.BeerId).FirstOrDefault().Quantity;
                if (quoteItem.Quantity > itemQuantity)
                {
                    var itemName = _repositoryWrapper.BeerRepository.GetByCondition(b => b.Id == quoteItem.BeerId).FirstOrDefault().Name;
                    throw new StockNotAvailableException(itemName, itemQuantity);
                }

                WholesalerBeerCatalog? wholesalerBeerCatalog = _repositoryWrapper
                        .WholesalerBeerCatalogRepository
                        .GetByCondition(wb => wb.WholesalerId == quoteDto.WholesalerId && wb.BeerId == quoteItem.BeerId)
                        .FirstOrDefault();

                if (wholesalerBeerCatalog is null)
                {
                    throw new BeerNotInWholesalerCatalogException();
                }
            }


        }
    }
}
