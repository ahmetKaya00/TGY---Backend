using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTOs
{
    
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün adı 2-100 karakter arasında olmalıdır.")]
        public string Name {get;set;} = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en az 500 karakter olmalıdır.")]
        public string Description {get;set;} = string.Empty;

        [Range(0.01, 999999.99, ErrorMessage = "Fiyat 0.01 ile 999999.99 arasında olmalıdır.")]
        public decimal Price {get;set;}

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
        public int StockQuantity;
    }
}