using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Remote(action: "HasProductName", controller: "Product")]
        [Required(ErrorMessage = "İsim alanı boş olamaz!")]
        [StringLength(50, ErrorMessage = "İsim alanına en fazla 50 karakter girilebilir!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş olamaz!")]
        [Range(1, 1000, ErrorMessage = "Fiyat alanı 1 ile 1000 arasında bir değer olmalıdır!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stok alanı boş olamaz!")]
        [Range(1, 200, ErrorMessage = "Stok alanı 1 ile 200 arasında bir değer olmalıdır!")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş olamaz!")]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "Açıklama alanına en az 50 en fazla 300 karakter içerebilir!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Renç seçimi boş olamaz!")]

        public string? Color { get; set; }
        [Required(ErrorMessage = "Yayınlanma tarihi boş olamaz!")]

        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
        [Required(ErrorMessage = "Yayınlanma süresi boş olamaz!")]

        public int? Expire { get; set; }
    }
}
