using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class BeerNotFoundException : CustomException
    {
        private const string _message = "The beer must exist.";
        public BeerNotFoundException() : base(_message) { }
    }
}
