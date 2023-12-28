using Brewterates.Application.Abstractions;
using Brewterates.Application.DTOs;
using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using Brewterates.Domain.Exceptions;


namespace Brewterates.Application.Services
{
    public class BeerService : IBeerService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Mapper _mapper;
        public BeerService(IRepositoryWrapper repositoryWrapper,
            Mapper mapper) 
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public List<BeerDto> GetBeerByBreweyId(long breweryId)
        {
            Brewery? brewery = _repositoryWrapper.BreweryRepository.GetByCondition(b => b.Id== breweryId).FirstOrDefault();
            if (brewery is null || brewery.Id == 0)
            {
                throw new Exception("The brewery must exist.");
            }


            List<Beer> lstBeer = _repositoryWrapper
                .BeerRepository
                .GetByCondition(b => b.BreweryId == breweryId)
                .ToList();

            
            return _mapper.BeerToBeerDtoList(lstBeer);
        }

        public Beer CreateBeer(BeerDto beerDto)
        {
            Brewery brewery = _repositoryWrapper.BreweryRepository.GetByCondition(b => b.Id == beerDto.Brewery.Id).FirstOrDefault();
            if (brewery is null)
            {
                throw new CustomException("The brewery must exist.");
            }

            Beer beer = _mapper.BeerDtoToBeer(beerDto);

            _repositoryWrapper.BeerRepository.Create(beer);
            _repositoryWrapper.Save();

            Beer createdBeer = _repositoryWrapper.BeerRepository.GetByCondition(b => b.Id == beer.Id).FirstOrDefault();
            if (createdBeer is null)
            {
                throw new CustomException();
            }

            return createdBeer;
        }
    }
}
