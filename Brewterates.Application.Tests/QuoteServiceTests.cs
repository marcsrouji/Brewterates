using Brewterates.Application.Abstractions;
using Brewterates.Application.Abstractions.Discount;
using Brewterates.Application.DTOs;
using Brewterates.Application.Services;
using Brewterates.Controllers;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using Xunit;
using NSubstitute;
using Brewterates.Application.Services.Discount;
using Brewterates.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Infrastructre.Repositories;
using System.Linq.Expressions;
using NSubstitute.ExceptionExtensions;
using Brewterates.Domain.Exceptions;

namespace Brewterates.Application.Tests
{
    public class QuoteServiceTests
    {
        private readonly IQuoteService _quoteService;
        private readonly IRepositoryWrapper _repositoryWrapper = Substitute.For<IRepositoryWrapper>();
        private readonly Mapper _mapper = Substitute.For<Mapper>();
        private readonly IDiscountFactory _discountFactory = Substitute.For<IDiscountFactory>(); 

        public QuoteServiceTests()
        {
            _quoteService = new QuoteService(_repositoryWrapper, _discountFactory, _mapper);
        }

        [Theory]
        [MemberData(nameof(GetEnumerator))]
        public void RequestQuote_Under10Drinks_Discount0(List<int> beerIds, Exception ex)
        {

            //Arrange

            var quoteItemDto1 = new QuoteItemDto()
            {
                BeerId = 30,
                Quantity = 2
            };

            QuoteDto quoteDto = new QuoteDto()
            {
                WholesalerId = 1,
                ClientName = "Marc Srouji",
                QuoteItems = new List<QuoteItemDto> { quoteItemDto1 }
            };


            var wholeSaler = new Wholesaler()
            {
                Id = 1,
                Name = "test"
            };

            var lstWholesaler = new List<Wholesaler>() { wholeSaler }.AsQueryable();

            var dbWholesaler = _repositoryWrapper.WholesalerRepository
                .GetByCondition(Arg.Any<Expression<Func<Wholesaler, bool>>>())
                .Returns(lstWholesaler);


            var wholeSalerStock = new WholesalerStock()
            {
                Id = 1,
                WholesalerId = 1,
                BeerId = 30,
                Quantity = 5,
                DateCreated = DateTime.Now
            };

            var lstWholesalerStock = new List<WholesalerStock>() { wholeSalerStock }.AsQueryable();

            var dbWholesalerStock = _repositoryWrapper.WholesalerStockRepository
                .GetByCondition(Arg.Any<Expression<Func<WholesalerStock, bool>>>())
                .Returns(lstWholesalerStock);

            var lstBeer = new List<Beer>();

            foreach (var beerid in beerIds)
            {
                var beer = new Beer()
                {
                    Id = 1,
                    Name = "test",
                    AlcoholIntent = 2,
                    Price = 3.2M,
                    BreweryId = 1
                };
                lstBeer.Add(beer);
            }

            var beers = lstBeer.AsQueryable();

            var lstDbBeer = _repositoryWrapper
                .BeerRepository
                .GetByCondition(Arg.Any<Expression<Func<Beer, bool>>>())
                .Returns(beers);

            var wholesalerBeerCatalog = new WholesalerBeerCatalog()
            {
                Id = 1,
                WholesalerId = 1,
                BeerId = 1,
                DateCreated = DateTime.Now
            };

            var lstWholesalerBeerCatalog = new List<WholesalerBeerCatalog>() { wholesalerBeerCatalog }.AsQueryable();

            var lstDbWholesalerBeerCatalog = _repositoryWrapper
                .WholesalerBeerCatalogRepository
                .GetByCondition(Arg.Any<Expression<Func<WholesalerBeerCatalog, bool>>>())
                .Returns(lstWholesalerBeerCatalog);


            var quoteItem = new QuoteItem()
            {
                BeerId = quoteItemDto1.BeerId,
                Quantity = quoteItemDto1.Quantity,
                QuoteId = 20
            };

            var test = _mapper.QuoteItemDtoToQuoteItem(quoteItemDto1);

            var quote = new Quote() 
            { 
                WholesalerId = quoteDto.WholesalerId,
                ClientName = quoteDto.ClientName,
                InitialTotalAmount = 5.5M,
                TotalAmount = 5.5M,
                Discount = 0,
                DateCreated = DateTime.Now,
                QuoteItems = new List<QuoteItem>() { quoteItem }
            };

            var lstQuote = new List<Quote>() { quote }.AsQueryable();
            _repositoryWrapper
                .QuoteRepository
                .GetByCondition(Arg.Any<Expression<Func<Quote, bool>>>())
                .Returns(lstQuote);

            var discountStrategy = new DiscountStrategy0();
            _discountFactory.GetDiscountStrategy(quoteDto.QuoteItems).Returns(discountStrategy);
            //discountStrategy.CalculateDiscount().Returns(0);


            //Act
            var result = _quoteService.RequestQuote(quoteDto);

            //Assert
            if (ex is null)
            {
                result.Should().NotBeNull();
                result.TotalAmount.Should().Be(result.InitialTotalAmount);
            } else
            {
                result.Should().Throws<DuplicateInOrderException>();
            }

        }



        public static IEnumerable<object[]> GetEnumerator()
        {
            yield return new object[] { new List<int> { 1 }, null };
            yield return new object[] { new List<int> { 1, 1 }, new DuplicateInOrderException() };
        }

    }
}
