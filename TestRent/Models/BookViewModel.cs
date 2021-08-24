using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRent.Models
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string PublishingDate { get; set; }
        public DateTime TenancyDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
