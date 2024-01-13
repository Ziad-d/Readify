using CodingWiki_DataAccess.FluentConfig;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicitionDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }

        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }

        public ApplicitionDbContext(DbContextOptions<ApplicitionDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CodingWiki;TrustServerCertificate=True;Trusted_Connection=True;")
            //      .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5);

            modelBuilder.Entity<BookAuthorMap>().HasKey(u => new { u.BookId, u.AuthorId });

            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorMapConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Spider without Duty", ISBN = "123B12", Price = 10.99m, PublisherId = 1 },
                new Book { BookId = 2, Title = "Fortune of Time", ISBN = "12123B12", Price = 11.99m, PublisherId = 1 }
                );

            var bookList = new Book[]
            {
                 new Book { BookId = 3, Title = "Fake Sunday", ISBN = "77652", Price = 20.99m, PublisherId = 2 },
                 new Book { BookId = 4, Title = "Cookie Jar", ISBN = "CC12B12", Price = 25.99m, PublisherId = 3 },
                 new Book { BookId = 5, Title = "Cloudy Forest", ISBN = "90392B33", Price = 40.99m, PublisherId = 3 }
            };
            modelBuilder.Entity<Book>().HasData(bookList);

            modelBuilder.Entity<Publisher>().HasData(
               new Publisher { PublisherId = 1, PublisherName = "Pub 1 Jimmy", Location = "Chicago" },
               new Publisher { PublisherId = 2, PublisherName = "Pub 2 John", Location = "New York" },
               new Publisher { PublisherId = 3, PublisherName = "Pub 3 Ben", Location = "Hawaii" }
               );
        }
    }
}
