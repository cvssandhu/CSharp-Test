using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SearchProductRequestDTO
    {
        public string? Branch { get; set; }
        public string? Search { get; set; }

        public string? Screen { get; set; }
        public string? StartAt { get; set; }
        public string? Limit { get; set; }
    }
}
