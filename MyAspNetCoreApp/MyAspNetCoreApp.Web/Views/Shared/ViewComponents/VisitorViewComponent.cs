using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
