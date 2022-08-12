
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class RequestResponse
    {
        public Request? request { get; set; }
        public SearchRequest? SearchRequest { get; set; }
        public SearchTopProducts? SearchTopProducts{ get; set; }

    }
    [Table("devtest.Request")]
    public class Request
    {
        [Key]
        public Int64 Rid { get; set; }

        [Required]
        public DateTime? Timestamp { get; set; }

        [Required]

        [Column(TypeName = "char")]
        public char? Kind { get; set; }

    }


    [Table("devtest.SearchRequest")]
    public class SearchRequest
    {

        [Key]
        public Int64 Rid { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Search { get; set; }

        [Required]
        [Column(TypeName = "char")]
        public char? SuccessInd { get; set; }

        [Required]
        public Int32? Hits { get; set; }

    }

    [Table("devtest.SearchTopProducts")]
    public class SearchTopProducts
    {
        [Required]
        [Key, Column(Order = 0)]
        public Int64? Rid { get; set; }
        [Required]

        [Key, Column(Order = 1)]
        public Int32? Order { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(50)")]
        public string? ProductId { get; set; }

    }    


}