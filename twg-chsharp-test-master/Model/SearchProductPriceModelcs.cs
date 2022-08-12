using System.ComponentModel.DataAnnotations;

namespace CSharpTest.Model
{
    public class SearchProductPriceModel
    {
        [Required]
        public string? Barcode { get; set; }
        public string? Branch { get; set; }            

    }
}
