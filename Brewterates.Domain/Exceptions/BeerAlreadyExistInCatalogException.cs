using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class BeerAlreadyExistInCatalogException : CustomException
    {
        private const string _message = "This beer already exists in your catalog.";
        public BeerAlreadyExistInCatalogException() : base(_message) { }
    }
}
