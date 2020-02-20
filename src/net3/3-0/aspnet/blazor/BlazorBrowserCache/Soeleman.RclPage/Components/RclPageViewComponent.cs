using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.RclPage.Components
{
    public class RclPageViewComponent :
        ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task
                .FromResult(this.View("Default"))
                .ConfigureAwait(false);
        }
    }
}