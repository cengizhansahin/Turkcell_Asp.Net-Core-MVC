namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new(){
            new() { Id = 1, Name = "Kalem 1", Price = 100, Stock = 100 },
            new() { Id = 2, Name = "Kalem 2", Price = 200, Stock = 200 },
            new() { Id = 3, Name = "Kalem 3", Price = 300, Stock = 300 }
        };
        public List<Product> GettAll() => _products;
        public void AddProduct(Product product) => _products.Add(product);
        public void RemoveProduct(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                throw new Exception($"Bu id({id}) ye ait ürün bulunamadı!");
            else
                _products.Remove(product);
        }
        public void UpdateProduct(Product updateProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == updateProduct.Id);
            if (product == null)
            {
                throw new Exception($"Bu id({updateProduct}) ye ait ürün bulunamadı!");
            }
            else
            {
                product.Name = updateProduct.Name;
                product.Stock = updateProduct.Stock;
                product.Price = updateProduct.Price;
                var index = _products.FindIndex(x => x.Id == updateProduct.Id);
                _products[index] = product;
            }
        }
    }
}
