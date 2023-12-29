using Brewterates.Application.Abstractions;
using Brewterates.Application.DTOs;
using Brewterates.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brewterates.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WholesalerController : Controller
    {
        private readonly IWholesalerSevice _wholesalerSevice;
        public WholesalerController(IWholesalerSevice wholesalerSevice) 
        {
            _wholesalerSevice = wholesalerSevice;
        }

        [HttpPost]
        public IActionResult AddBeer(WholesalerBeerDto wholesalerBeerDto)
        {
            return Ok(_wholesalerSevice.AddWholesalerBeerToCatalog(wholesalerBeerDto));
        }

        [HttpPatch]
        public IActionResult UpdateStockQuantity(StockDto stockDto)
        {
            return Ok(_wholesalerSevice.UpdateWholesalerStockQuantity(stockDto));
        }
    }
}
