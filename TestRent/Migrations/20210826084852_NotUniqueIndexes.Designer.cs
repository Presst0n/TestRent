﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestRent.Data;

namespace TestRent.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210826084852_NotUniqueIndexes")]
    partial class NotUniqueIndexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestRent.Data.DbModels.BookDb", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountOfBooks")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishingDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            AmountOfBooks = 12,
                            Author = "Orson Scott Card",
                            Genre = "Science Fiction",
                            PublishingDate = "15 grudnia, 2000",
                            Title = "Ender's Shadow"
                        },
                        new
                        {
                            BookId = 2,
                            AmountOfBooks = 17,
                            Author = "J. R. R. Tolkien",
                            Genre = "Fantasy",
                            PublishingDate = "1 sierpnia, 2013",
                            Title = "Children of Húrin"
                        },
                        new
                        {
                            BookId = 3,
                            AmountOfBooks = 5,
                            Author = "J. R. R. Tolkien",
                            Genre = "Fantasy",
                            PublishingDate = "2 września, 1983",
                            Title = "Silmarillion"
                        },
                        new
                        {
                            BookId = 4,
                            AmountOfBooks = 15,
                            Author = "Frank Herbert",
                            Genre = "Science Fiction",
                            PublishingDate = "29 maja, 2007",
                            Title = "Dune"
                        });
                });

            modelBuilder.Entity("TestRent.Data.DbModels.ClientDb", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TestRent.Data.DbModels.TransactionDb", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("RentedBookId")
                        .HasColumnType("int");

                    b.Property<string>("ReturnDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenancyDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TransactionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TransactionId");

                    b.HasIndex("ClientId");

                    b.HasIndex("RentedBookId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TestRent.Data.DbModels.TransactionDb", b =>
                {
                    b.HasOne("TestRent.Data.DbModels.ClientDb", "Client")
                        .WithOne("Transaction")
                        .HasForeignKey("TestRent.Data.DbModels.TransactionDb", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestRent.Data.DbModels.BookDb", "RentedBook")
                        .WithOne("Transaction")
                        .HasForeignKey("TestRent.Data.DbModels.TransactionDb", "RentedBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("RentedBook");
                });

            modelBuilder.Entity("TestRent.Data.DbModels.BookDb", b =>
                {
                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TestRent.Data.DbModels.ClientDb", b =>
                {
                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}