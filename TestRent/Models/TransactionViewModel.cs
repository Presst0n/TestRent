using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRent.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public Guid TransactionGuid { get; set; }
        public string TenancyDate { get; set; }
        public string ReturnDate { get; set; }


        public int ClientId { get; set; }
        public int RentedBookId { get; set; }
    }
}
