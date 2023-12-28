using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class Wholesaler
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<WholesalerBeerList> WholesalerBeerLists { get; set; } = new List<WholesalerBeerList>();
}
