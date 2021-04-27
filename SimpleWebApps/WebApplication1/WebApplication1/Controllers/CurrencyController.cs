using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealTimeEconomy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Currency
            {
                CurDateTime = DateTime.Now.AddDays(index),
                //Name = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
