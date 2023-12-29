using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class StockDto
    {
        public long wholesalerId {  get; set; }
        public long beerId { get; set; }
        public int quantity { get; set; }
    }
}
