using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestRent.Data.DbModels
{
    [Index(nameof(ClientId), IsUnique = false)]
    [Index(nameof(RentedBookId), IsUnique = false)]
    public class TransactionDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public Guid TransactionGuid { get; set; }
        public string TenancyDate { get; set; }
        public string ReturnDate { get; set; }

        public int ClientId { get; set; }
        public int RentedBookId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual ClientDb Client { get; set; }

        [ForeignKey(nameof(RentedBookId))]
        public virtual BookDb RentedBook { get; set; }
    }
}
