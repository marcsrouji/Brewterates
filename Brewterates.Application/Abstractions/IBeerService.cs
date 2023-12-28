using Brewterates.Application.DTOs;
using Brewterates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewterates.Application.Abstractions
{
    public interface IBeerService
    {
        List<BeerDto> GetBeerByBreweyId(long breweryId);
        Beer CreateBeer(BeerDto beerDto);

    }
}
