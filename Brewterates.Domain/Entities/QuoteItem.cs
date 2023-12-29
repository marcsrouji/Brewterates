using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class QuoteItem
{
    public long Id { get; set; }

    public long BeerId { get; set; }

    public int Quantity { get; set; }

    public long QuoteId { get; set; }

    public virtual Quote Quote { get; set; } = null!;
}
