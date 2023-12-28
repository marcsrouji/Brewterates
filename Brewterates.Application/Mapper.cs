using Brewterates.Application.DTOs;
using Brewterates.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Brewterates.Application
{
    [Mapper]
    public partial class Mapper
    {
        public partial List<BeerDto> BeerToBeerDtoList(List<Beer> beer);
        //[MapProperty(nameof(BeerDto.Brewery.Id), nameof(Beer.BreweryId))]
        //[MapProperty(nameof(BeerDto.Name), nameof(Beer.Name))]
        //[MapProperty(nameof(BeerDto.AlcoholIntent), nameof(Beer.AlcoholIntent))]
        //[MapProperty(nameof(BeerDto.Price), nameof(Beer.Price))]
        [MapperIgnoreTarget(nameof(Beer.Brewery))]
        public partial Beer BeerDtoToBeer(BeerDto beerdto);
    }
}
