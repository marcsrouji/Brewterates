using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class BeerDto
    {
        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal AlcoholIntent { get; set; }
        public decimal Price { get; set; }
        public BreweryDto Brewery { get; set; }
    }
}
