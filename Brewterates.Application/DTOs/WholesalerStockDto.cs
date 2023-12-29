using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class WholesalerStockDto
    {
        public long WholesalerId { get; set; }
        public long BeerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
