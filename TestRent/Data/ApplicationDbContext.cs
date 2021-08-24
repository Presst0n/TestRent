using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRent.Data.DbModels;

namespace TestRent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BookDb> Books { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDb>().HasData(
                new BookDb
                {
                    BookId = 1,
                    Title = "Ender's Shadow",
                    Author = "Orson Scott Card",
                    Genre = "Science Fiction",
                    PublishingDate = "15 grudnia, 2000"
                },
                new BookDb
                {
                    BookId = 2,
                    Title = "Children of Húrin",
                    Author = "J. R. R. Tolkien",
                    Genre = "Fantasy",
                    PublishingDate = "1 sierpnia, 2013"
                },
                new BookDb
                {
                    BookId = 3,
                    Title = "Silmarillion",
                    Author = "J. R. R. Tolkien",
                    Genre = "Fantasy",
                    PublishingDate = "2 września, 1983"
                },
                new BookDb
                {
                    BookId = 4,
                    Title = "Dune",
                    Author = "Frank Herbert",
                    Genre = "Science Fiction",
                    PublishingDate = "29 maja, 2007"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
