using Brewterates.Application.Abstractions.Discount;
using Brewterates.Application.DTOs;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Services.Discount
{
    public class DiscountFactory : IDiscountFactory
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IReadOnlyDictionary<IDiscountStrategy, (int floor, int ceiling)> _discountStrategies;
        public DiscountFactory(IRepositoryWrapper repositoryWrapper) 
        {
            _repositoryWrapper = repositoryWrapper;
            _discountStrategies = new Dictionary<IDiscountStrategy, (int, int)>()
            {   
                { new DiscountStrategy10(), (10,20)}, 
                { new DiscountStrategy20(), (20, int.MaxValue)}
            };
        }

        public IDiscountStrategy GetDiscountStrategy(List<QuoteItemDto> quoteItems)
        {
            var quantity = quoteItems.Select(qi => qi.Quantity).Sum();

            var lstDiscountStrategy = _discountStrategies.Where(d => quantity >= d.Value.floor && quantity < d.Value.ceiling).ToList();
            if (!lstDiscountStrategy.Any())
            {
                return new DiscountStrategy0();
            }

            IDiscountStrategy discountStrategy = lstDiscountStrategy.MinBy(m => m.Value.floor).Key;
            return discountStrategy;
        }
    }
}
