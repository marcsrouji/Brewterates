using Brewterates.Application.DTOs;
using Brewterates.Domain.Entities;
using Brewterates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Abstractions.Discount
{
    public interface IDiscountStrategy
    {
        public decimal CalculateDiscount();
    }
}
