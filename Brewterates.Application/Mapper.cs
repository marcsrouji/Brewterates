using Brewterates.Application.DTOs;
using Brewterates.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Brewterates.Application
{
    [Mapper]
    public partial class Mapper
    {
        public partial List<BeerDto> BeerToBeerDtoList(List<Beer> beer);
        [MapperIgnoreTarget(nameof(BeerDto.Brewery))]
        public partial BeerDto BeerToBeerDto(Beer beer);

        [MapperIgnoreTarget(nameof(Beer.Brewery))]
        public partial Beer BeerDtoToBeer(BeerDto beerdto);
        public partial WholesalerDto WholesalerToWholeSalerDto(Wholesaler beerdto);
        public partial QuoteItem QuoteItemDtoToQuoteItem(QuoteItemDto quoteItemDto);
    }
}
