using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Models
{
    public class DiscountAmount
    {
        public decimal discount {  get; set; }
        public decimal discountedAmount { get; set; }
        public decimal initialAmount { get; set; }
    }
}
