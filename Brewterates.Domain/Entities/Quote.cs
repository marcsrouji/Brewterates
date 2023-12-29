using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class Quote
{
    public long Id { get; set; }

    public string? ClientName { get; set; }

    public long WholesalerId { get; set; }

    public decimal InitialTotalAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Discount { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<QuoteItem> QuoteItems { get; set; } = new List<QuoteItem>();

    public virtual Wholesaler Wholesaler { get; set; } = null!;
}
