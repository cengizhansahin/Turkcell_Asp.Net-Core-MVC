using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;
        public ProductController(AppDbContext context)
        {
            //DI Container
            //Dependency Injection Pattern
            _productRepository = new ProductRepository();
            _context = context;
            //Linq method
            if (!_context.Products.Any())
            {
                _context.Products.AddRange(
                new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "White", Width = 10, Height = 20 },
                new Product() { Name = "Kalem2", Price = 200, Stock = 200, Color = "White", Width = 10, Height = 20 },
                new Product() { Name = "Kalem3", Price = 300, Stock = 300, Color = "White", Width = 10, Height = 20 }
                );
                _context.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Remove(int id)
        {
            //_productRepository.RemoveProduct(id);
            Product deletedProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(deletedProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}
