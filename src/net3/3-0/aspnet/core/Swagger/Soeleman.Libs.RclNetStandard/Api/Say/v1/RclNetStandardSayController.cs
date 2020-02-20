using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Soeleman.Libs.Model;

namespace Soeleman.Libs.RclNetStandard.Api.Say.v1
{
    [ApiController]
    [Route("[controller]")]
    public class RclNetStandardSayController :
        ControllerBase
    {
        [HttpGet]
        public async ValueTask<SayModel> Get()
        {
            return await Task
                .FromResult(new SayModel
                {
                    Name = nameof(RclNetStandardSayController)
                })
                .ConfigureAwait(false);
        }
    }
}
