﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.Libs.Child.Components
{
    public class SixthChildViewComponent :
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