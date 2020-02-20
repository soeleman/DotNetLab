using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public sealed class GenericApiController<TK, T> :
        ControllerBase
    {
        private readonly IStorage<T, TK> storage;
        private readonly IConfiguration configuration;

        public GenericApiController(
            IConfiguration configuration,
            IStorage<T, TK> storage)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TK>>> GetAll()
        {
            var data = await this.storage
                .Read()
                .ConfigureAwait(false);

            return new ActionResult<IEnumerable<TK>>(data.Any()
                ? this.Ok(data)
                : (ActionResult)this.NotFound());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TK>> Get(
            T id)
        {
            var (state, currentData) = await this.storage
                .ReadByKey(id)
                .ConfigureAwait(false);

            return new ActionResult<TK>(state
                ? this.Ok(currentData)
                : (ActionResult)this.NotFound());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] TK value)
        {
            if (value == null)
            {
                return this.BadRequest();
            }

            var (state, _) = await this.storage
                .Create(value)
                .ConfigureAwait(false);

            return state ?
                this.Created(Request.GetEncodedUrl(), string.Empty)
                : (IActionResult)this.NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(
            T id,
            [FromBody] TK value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var (state, _) = await this.storage
                .Update(id, value, configuration.ExcludeProperty(this.GetType(), typeof(TK)))
                .ConfigureAwait(false);

            return state
                ? this.Accepted()
                : (IActionResult)this.NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            T id)
        {
            var (state, _) = await this.storage
                .Delete(id)
                .ConfigureAwait(false);

            return state ? this.Accepted() : (ActionResult)this.NoContent();
        }
    }
}
