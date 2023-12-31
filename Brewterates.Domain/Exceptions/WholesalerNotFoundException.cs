using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class WholesalerNotFoundException : CustomException
    {
        private const string _message = "The wholesaler must exist";
        public WholesalerNotFoundException() : base(_message) { }
    }
}
