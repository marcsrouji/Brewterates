using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class WholesalerBeerDataDto
    {
        public BeerDto beer { get; set; }
        public WholesalerDto wholeSaler { get; set; }
    }
}
