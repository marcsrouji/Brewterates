using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class BeerNotInWholesalerStockException : CustomException
    {
        private const string _message = "This Beer is not in you stock.";
        public BeerNotInWholesalerStockException() : base(_message) { }
    }
}
