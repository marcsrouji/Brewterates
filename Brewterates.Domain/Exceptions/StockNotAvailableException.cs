using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Domain.Exceptions
{
    public class StockNotAvailableException : CustomException
    {
        private const string _message = "Quantity ordered of {itemName} is not available in stock. Limit : {itemQuantity}";
        public StockNotAvailableException(string itemName, int itemQuantity) : base(_message.Replace("{itemName}", itemName.ToString()).Replace("{itemQuantity}", itemQuantity.ToString())) { }
    }
}
