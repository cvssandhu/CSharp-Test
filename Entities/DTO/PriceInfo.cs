using System.Text.Json.Serialization;

namespace Entities
{
    public class PriceInfo
    {
        /// <summary>
        /// Product found
        /// Found = “Y”
        /// ------------
        /// Product not found
        /// Found = “N”
        /// </summary>
        public string? Found { get; set; }

        /// <summary>
        /// product info
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Product? Product { get; set; }

        /// <summary>
        /// a unique 64-bit number for the scan.
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ScanID { get; set; }


    }

}