using System.ComponentModel.DataAnnotations;

namespace CSharpTest.Model
{
    public class SearchProductModel
    {
        [Required]
        public string? Branch { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Search value cannot exceed 50 characters. ")]

        public string? Search { get; set; }
        
        public string? Screen { get; set; }
        public string? StartAt { get; set; }
        public string? Limit { get; set; }
    }
}

