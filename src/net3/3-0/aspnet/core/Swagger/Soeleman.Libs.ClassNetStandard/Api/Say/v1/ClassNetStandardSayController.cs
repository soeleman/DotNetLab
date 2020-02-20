using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Soeleman.Libs.Model;

namespace Soeleman.Libs.ClassNetStandard.Api.Say.v1
{
    [ApiController]
    [Route("[controller]")]
    public class ClassNetStandardSayController :
        ControllerBase
    {
        [HttpGet]
        public async ValueTask<SayModel> Get()
        {
            return await Task
                .FromResult(new SayModel
                {
                    Name = nameof(ClassNetStandardSayController)
                })
                .ConfigureAwait(false);
        }
    }
}