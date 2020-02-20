﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Soeleman.Libs.Core.Components
{
    public class StyleComponentCssViewComponent :
        ViewComponent
    {
        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task
                .FromResult(this.View("Default"))
                .ConfigureAwait(false);
        }
    }
}