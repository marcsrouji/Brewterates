using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class EmptyOrderException : CustomException
    {
        private const string _message = "The order cannot be empty.";
        public EmptyOrderException() : base(_message) { }
    }
}
