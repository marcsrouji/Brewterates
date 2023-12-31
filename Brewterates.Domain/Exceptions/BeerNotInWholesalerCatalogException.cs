using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class BeerNotInWholesalerCatalogException : CustomException
    {
        private const string _message = "This beer is not in the wholesaler catalog.";
        public BeerNotInWholesalerCatalogException() : base(_message) { }
    }
}
