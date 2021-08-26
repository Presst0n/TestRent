using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRent.Data;
using TestRent.Data.DbModels;

namespace TestRent.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ApplicationDbContext _db;

        public ClientController(ILogger<ClientController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetClient([FromBody] string clientId)
        {
            var clientDb = await _db.Clients.FindAsync(Convert.ToInt32(clientId));

            return Json(clientDb);
        }


        public async Task<IActionResult> GetClients()
        {
            var clientsDb = await _db.Clients.ToListAsync();

            return Json(clientsDb);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDb clientInput)
        {
            if (clientInput is null)
                return Json(new { Message = "Coś nie pykło." });

            var clientDb = await _db.Clients.FindAsync(clientInput.ClientId);

            if (clientDb != null)
                return Json(new { Message = "Dany klient znajduje się już w naszym systemie." });

            var entityEntry = await _db.Clients.AddAsync(new ClientDb
            {
                City = clientInput.City,
                FirstName = clientInput.FirstName,
                LastName = clientInput.LastName,
                PostalCode = clientInput.PostalCode,
                Street = clientInput.Street
            });
            
            await _db.SaveChangesAsync();

            return Json(new { Message = "Operacja dodania klienta do systemu przebiegła pomyśnie.", data = new { clientId = entityEntry.Entity.ClientId }});
        }


        [HttpDelete] 
        public async Task<IActionResult> DeleteClient([FromBody] string clientId)
        {
            var client = await _db.Clients.AsNoTracking().SingleOrDefaultAsync(x => x.ClientId == Convert.ToInt32(clientId));

            if (client is null)
                return Json(new { Message = "Ups... Nie znaleziono danego klienta." });

            int ReturnedBooks = 0;

            _db.Transactions.AsNoTracking().Where(t => t.ClientId == client.ClientId).ToList().ForEach(i => 
            {
                var book = _db.Books.SingleOrDefault(b => b.BookId == i.RentedBookId);
                book.AmountOfBooks += 1;

                _db.Books.Update(book);
                ReturnedBooks += 1;
            });

            await _db.SaveChangesAsync();

            _db.Clients.Remove(client);

            await _db.SaveChangesAsync();

            return Json(new { Message = "Klient został pomyślnie usunięty.", Data = new { ReturnedBooks = ReturnedBooks } });
        }


    }
}
