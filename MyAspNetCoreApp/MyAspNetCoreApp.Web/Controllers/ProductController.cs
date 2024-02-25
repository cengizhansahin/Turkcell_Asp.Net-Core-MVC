using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        public ProductController(AppDbContext context, IMapper mapper)
        {
            //DI Container
            //Dependency Injection Pattern
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;
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
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }
        public async Task<IActionResult> GetById(int id)
        {
            if (id == null)
                return NotFound("Id bulunamadı!");
            var result = await _context.Products.FindAsync(id);
            return View(_mapper.Map<ProductViewModel>(result));
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
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3},
                {"6 Ay",6 },
                {"12 Ay",12 }
            };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new ColorSelectList(){Data="Mavi",Value="Mavi"},
                new ColorSelectList(){Data="Kırmızı",Value="2"},
                new ColorSelectList(){Data="Sarı",Value="3"}
            }, "Value", "Data");
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            // Request Header-Body



            // 1.Yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            // 2.Yöntem
            //Product newProduct = new() { Name = Name, Price = Price, Color = Color, Stock = Stock };
            //if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            //{
            //    ModelState.AddModelError(string.Empty, "Ürün ismi A harfi ile başlayamaz!");
            //}
            ViewBag.Expire = new Dictionary<string, int>()
                {
                    {"1 Ay",1 },
                    {"3 Ay",3},
                    {"6 Ay",6 },
                    {"12 Ay",12 }
                };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                new ColorSelectList(){Data="Mavi",Value="Mavi"},
                new ColorSelectList(){Data="Kırmızı",Value="2"},
                new ColorSelectList(){Data="Sarı",Value="3"}
                }, "Value", "Data");
            if (ModelState.IsValid)
            {
                try
                {
                    //throw new Exception("Db hatası!");
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();
                    TempData["status"] = "Ürün başarıyla eklendi.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.");
                    return View();
                }
            }
            else
            {
                return View();
            }


        }
        [HttpGet]
        public IActionResult Update(int id)
        {

            var product = _context.Products.FirstOrDefault(product => product.Id == id);

            ViewBag.radioExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3},
                {"6 Ay",6 },
                {"12 Ay",12 }
            };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new ColorSelectList(){Data="Mavi",Value="Mavi"},
                new ColorSelectList(){Data="Kırmızı",Value="Kırmızı"},
                new ColorSelectList(){Data="Sarı",Value="Sarı"}
            }, "Value", "Data", product.Color);

            return View(_mapper.Map<ProductViewModel>(product));
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel obj)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.radioExpireValue = obj.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
                {
                    {"1 Ay",1 },
                    {"3 Ay",3},
                    {"6 Ay",6 },
                    {"12 Ay",12 }
                };
                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                    new ColorSelectList(){Data="Mavi",Value="Mavi"},
                    new ColorSelectList(){Data="Kırmızı",Value="Kırmızı"},
                    new ColorSelectList(){Data="Sarı",Value="Sarı"}
                }, "Value", "Data", obj.Color);
                Console.WriteLine("Hata");
                return View();

            }
            _context.Products.Update(_mapper.Map<Product>(obj));
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(product => product.Name.ToLower() == Name.ToLower());
            if (anyProduct)
            {
                return Json("Kaydetmeye çalıştığınız ürün ismi veri tabanında bulunmaktadır!");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
