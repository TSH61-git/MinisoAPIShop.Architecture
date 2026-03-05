using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductSearchParams
    {
        public string? Desc { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int>? CategoryIDs { get; set; }
        public string? OrderBy { get; set; }

    }
}
