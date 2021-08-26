using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TestRent.Data;
using TestRent.Data.DbModels;
using TestRent.Models;

namespace TestRent.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ApplicationDbContext _db;

        public TransactionController(ILogger<TransactionController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionViewModel input)
        {
            if (input is null)
                return Json(new { Message = "Coś nie pykło. :/" });

            var tDb = await _db.Clients.FindAsync(input.TransactionId);

            if (tDb != null)
                return Json(new { Message = "Identyczna transakcja znajduje się już w naszym systemie." });

            //string[] formats= { "dd'.'MM'.'yyyy", "MM'/'dd'/'yyyy" };
            DateTime date;
            if (!DateTime.TryParseExact(input.ReturnDate, "dd'.'MM'.'yyyy",
                                       CultureInfo.CurrentCulture,
                                       DateTimeStyles.AssumeLocal,
                                       out date))
            {
                date = Convert.ToDateTime(input.ReturnDate);
            }


            try
            {
                var datetest = date.ToShortDateString();

                var book = await _db.Books.FindAsync(input.RentedBookId);
                book.AmountOfBooks -= 1;
                await _db.Transactions.AddAsync(new TransactionDb
                {
                    ClientId = input.ClientId,
                    RentedBook = book,
                    TransactionGuid = Guid.NewGuid(),
                    ReturnDate = date.ToShortDateString(),
                    TenancyDate = DateTime.Now.ToShortDateString()
                });

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { Message = "Mamy errora Panie!!!" });
            }



            return Json(new { Message = "Operacja dodania transakcji do systemu przebiegła pomyśnie." });
        }


        public async Task<IActionResult> GetTransactions()
        {
            var tDb = await _db.Transactions.ToListAsync();

            var transactions = new List<object>();

            //public DateTime? TenancyDate { get; set; } = null;
            //public DateTime? ReturnDate { get; set; } = null;

            tDb.ForEach(t =>
            {
                var book = _db.Books.SingleOrDefault(b => b.BookId == t.RentedBookId);
                var client = _db.Clients.SingleOrDefault(c => c.ClientId == t.ClientId);
                transactions.Add(new
                {
                    Id = t.TransactionId,
                    Guid = t.TransactionGuid,
                    TenancyDate = t.TenancyDate,
                    ReturnDate = t.ReturnDate,
                    BookTitle = book.Title,
                    BookId = book.BookId,
                    ClientFirstName = client.FirstName,
                    ClientLastName = client.LastName
                });
            });

            return Json(transactions);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction([FromBody] string transactionId)
        {
            var transactionDb = await _db.Transactions.AsNoTracking().SingleOrDefaultAsync(x => x.TransactionId == Convert.ToInt32(transactionId));

            if (transactionDb is null)
                return Json(new { Message = "Ups... Nie znaleziono danego klienta." });

            var book = _db.Books.SingleOrDefault(b => b.BookId == transactionDb.RentedBookId);
            book.AmountOfBooks += 1;

            _db.Books.Update(book);

            await _db.SaveChangesAsync();

            _db.Transactions.Remove(transactionDb);

            await _db.SaveChangesAsync();

            return Json(new { Message = "Klient został pomyślnie usunięty." });
        }
    }
}
