using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.Libs.Core.Components
{
    public class NavMenuItemViewComponent :
        ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            string theme = "white")
        {
            (string BgColor, string NavBar, string NavLink) cssTheme;

            switch(theme)
            {
                case "dark":
                    cssTheme = ("bg-dark", "navbar-dark", string.Empty);
                    break;

                case "blue":
                    cssTheme = ("bg-primary", "navbar-light", string.Empty);
                    break;

                default:
                    cssTheme = ("bg-white", "navbar-light", "text-dark");
                    break;
            }

            return await Task
                .FromResult(this.View("Default", cssTheme))
                .ConfigureAwait(false);
        }
    }
}