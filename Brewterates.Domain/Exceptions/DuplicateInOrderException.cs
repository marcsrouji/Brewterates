using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class DuplicateInOrderException : CustomException
    {
        private const string _message = "There can't be any duplicate in the order.";
        public DuplicateInOrderException() : base(_message) { }
    }
}
