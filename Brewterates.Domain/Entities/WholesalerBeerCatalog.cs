﻿using System;
using System.Collections.Generic;

namespace Brewterates.Domain.Entities;

public partial class WholesalerBeerCatalog
{
    public long Id { get; set; }

    public long WholesalerId { get; set; }

    public long BeerId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Beer Beer { get; set; } = null!;

    public virtual Wholesaler Wholesaler { get; set; } = null!;
}
