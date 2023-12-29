using Brewterates.Application.DTOs;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Abstractions.Discount
{
    public interface IDiscountFactory
    {
        IDiscountStrategy GetDiscountStrategy(List<QuoteItemDto> quoteItems);
    }
}
