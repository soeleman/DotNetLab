﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.Libs.Hello.Components
{
    public class HelloDotNetViewComponent :
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