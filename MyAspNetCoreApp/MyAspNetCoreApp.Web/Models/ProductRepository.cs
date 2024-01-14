namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
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
