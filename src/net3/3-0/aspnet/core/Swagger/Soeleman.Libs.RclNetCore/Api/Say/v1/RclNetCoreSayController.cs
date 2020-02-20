using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Soeleman.Libs.Model;

namespace Soeleman.Libs.RclNetCore.Api.Say.v1
{
    [ApiController]
    [Route("[controller]")]
    public class RclNetCoreSayController :
        ControllerBase
    {
        [HttpGet]
        public async ValueTask<SayModel> Get()
        {
            return await Task
                .FromResult(new SayModel
                {
                    Name = nameof(RclNetCoreSayController)
                })
                .ConfigureAwait(false);
        }
    }
}