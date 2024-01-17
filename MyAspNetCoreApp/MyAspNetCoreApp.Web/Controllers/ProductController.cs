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
            //if (!_context.Products.Any())
            //{
            //    _context.Products.AddRange(
            //    new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "White" },
            //    new Product() { Name = "Kalem2", Price = 200, Stock = 200, Color = "White" },
            //    new Product() { Name = "Kalem3", Price = 300, Stock = 300, Color = "White" }
            //    );
            //    _context.SaveChanges();
            //}
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
        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            // Request Header-Body



            // 1.Yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            // 2.Yöntem
            //Product newProduct = new() { Name = Name, Price = Price, Color = Color, Stock = Stock };


            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}
