using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Soeleman.Wasm.Shared;

namespace Soeleman.Wasm.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController :
        ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger logger;

        public WeatherForecastController(
            ILoggerFactory logger)
        {
            this.logger = logger.CreateLogger<WeatherForecastController>();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            this.logger.LogInformation(
                @"Controller: {0}, Action: {1}", 
                nameof(WeatherForecastController), 
                nameof(this.Get));

            var rng = new Random();

            return Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}