using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.RclView.Components
{
    public class RclViewViewComponent :
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
