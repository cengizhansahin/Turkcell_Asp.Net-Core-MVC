using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewmodels = await _context.Products.Select(x => new ProductListComponentViewModel()
            {
                Name = x.Name,
                Description = x.Description,
            }).ToListAsync();
            return View(viewmodels);
        }
    }
}
