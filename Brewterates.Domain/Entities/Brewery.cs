using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class Brewery
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Beer> Beers { get; set; } = new List<Beer>();
}
