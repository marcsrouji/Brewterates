using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class Beer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal AlcoholIntent { get; set; }

    public decimal Price { get; set; }

    public long BreweryId { get; set; }

    public virtual Brewery Brewery { get; set; } = null!;

    public virtual ICollection<WholesalerBeerCatalog> WholesalerBeerCatalogs { get; set; } = new List<WholesalerBeerCatalog>();

    public virtual ICollection<WholesalerStock> WholesalerStocks { get; set; } = new List<WholesalerStock>();
}
