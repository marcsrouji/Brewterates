using Brewterates.Application.Abstractions.Discount;
using Brewterates.Application.DTOs;
using Brewterates.Domain.Abstractions.IUnitOfWork;
using Brewterates.Domain.Entities;
using Brewterates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Services.Discount
{
    public class DiscountStrategy10 : IDiscountStrategy
    {
        private const decimal _discount = 10;

        public decimal CalculateDiscount()
        {
            return _discount;
        }
    }
}
