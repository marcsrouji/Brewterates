using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.DTOs
{
    public class WholesalerBeerDto
    {
        [Required]
        public long beerId { get; set; }
        [Required]
        public long wholeSalerId { get; set; }
    }
}
