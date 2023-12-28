using Brewterates.Application.Abstractions;
using Brewterates.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Brewterates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : Controller
    {
        private readonly IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet("/BreweryId/{breweryId}")]
        public ActionResult Get(long breweryId)
        {
            return Ok(_beerService.GetBeerByBreweyId(breweryId));
        }

        [HttpPost]
        public ActionResult AddBeer(BeerDto breweryId)
        {
            return Ok(_beerService.CreateBeer(breweryId));
        }

    }
}
