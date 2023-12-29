using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class Wholesaler
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();

    public virtual ICollection<WholesalerBeerCatalog> WholesalerBeerCatalogs { get; set; } = new List<WholesalerBeerCatalog>();

    public virtual ICollection<WholesalerStock> WholesalerStocks { get; set; } = new List<WholesalerStock>();
}
