using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class QuoteDto
    {
        public long WholesalerId { get; set; }
        public string ClientName { get; set; }
        public List<QuoteItemDto> QuoteItems { get; set; }
    }
}
