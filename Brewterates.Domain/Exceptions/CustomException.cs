using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class CustomException : Exception
    {
        private const string  _message = "An error has occured.";
        public CustomException(string? message = _message) :base(message) { }
    }
}
