using Brewterates.Application.Abstractions;
using Brewterates.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Brewterates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : Controller
    {
        private readonly IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }
        [HttpPost]
        public IActionResult RequestQuote(QuoteDto quoteDto) 
        {
            return Ok(_quoteService.RequestQuote(quoteDto));
        }
    }
}
