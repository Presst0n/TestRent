using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestRent.Data;
using TestRent.Data.DbModels;
using TestRent.Models;

namespace TestRent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var booksDb = await _db.Books?.ToListAsync();
            return Json(booksDb);
        }


        public async Task<IActionResult> GetBook([FromBody] string bookId)
        {
            var booksDb = await _db.Books.FindAsync(Convert.ToInt32(bookId));
            return Json(booksDb);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDb book)
        {
            var bookDb = await _db.Books.FindAsync(book?.BookId);

            if (bookDb != null)
                return Json(new { Message = "Dana książka znajduje już się w naszym systemie." });

            //var newClient = new ClientDb
            //{
            //    City = "Warszawa",
            //    FirstName = "Jędrzej",
            //    LastName = "Paluszkiewicz",
            //    PostalCode = "00 - 006",
            //    Street = "Piłsudskiego 19/3",
            //};


            try
            {
                //await _db.Clients.AddAsync(newClient);
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                //var loadedClient = await _db.Clients.FirstOrDefaultAsync();

                //if (loadedClient != null)
                //{
                //    var loadedBook = await _db.Books.FindAsync(2);

                //    if (loadedBook != null)
                //    {

                //        var newTransaction = new TransactionDb
                //        {
                //            TransactionGuid = Guid.NewGuid(),
                //            Client = loadedClient,
                //            RentedBook = loadedBook
                //        };

                //        await _db.Transactions.AddAsync(newTransaction);
                //        await _db.SaveChangesAsync();
                //    }
                //}


                //var loadedTransaction = await _db.Transactions.FirstOrDefaultAsync();
                //var loadedClient = await _db.Clients.Include(c => c.Transactions).ThenInclude(e => e.RentedBook).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return Json(new { Message = "Książka została dodana pomyślnie." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook([FromBody] BookDb book)
        {
            var bookDb = await _db.Books.FindAsync(book.BookId);

            if (bookDb is null)
                return Json(new { Message = "Ups... Nie znaleziono takiej książki." });

            bookDb.Author = book.Author;
            bookDb.Genre = book.Genre;
            bookDb.PublishingDate = book.PublishingDate;
            bookDb.AmountOfBooks = book.AmountOfBooks;
            //bookDb.ReturnDate = book.ReturnDate;
            //bookDb.TenancyDate = book.TenancyDate;
            bookDb.Title = book.Title;

            await _db.SaveChangesAsync();

            return Json(new { Message = "Książka została zaktualizowana pomyślnie." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromBody] string bookId)
        {
            var bookDb = await _db.Books.FindAsync(Convert.ToInt32(bookId));

            if (bookDb is null)
                return Json(new { Message = "Ups... Nie znaleziono takiej książki." });

            _db.Books.Remove(bookDb);

            await _db.SaveChangesAsync();

            return Json(new { Message = "Książka została usunięta pomyślnie." });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
