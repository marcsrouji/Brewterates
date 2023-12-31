using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class BreweryNotFoundException : CustomException
    {
        private const string _message = "The brewery must exist.";
        public BreweryNotFoundException() : base(_message) { }
    }
}
