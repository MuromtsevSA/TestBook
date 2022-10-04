using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TestBook.Model;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public string path = Directory.GetCurrentDirectory();

    public BookContext()
    {
        Database.EnsureCreated();
    }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Server=DESKTOP-HCJS4U4; Database={path}\\BookDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book[] 
            {
                new Book { Id=1, Name="Руслан и людмила", Date="1813", ISBN="166-000-22", Description="jdjJJj", Authors = new List<Author>(){ new Author(){ Id=1,Name = "Пушкин"}}},
                new Book { Id=2, Name="Война и мир", Date="1813", ISBN="166-000-22", Description="jdjAAAj", Authors = new List<Author>(){ new Author(){ Id=1,Name = "Пушкин" }}},
                new Book { Id=3, Name="Пиковая дама", Date="1813", ISBN="166-000-22", Description="jdjAAj", Authors = new List<Author>(){ new Author(){ Id=1,Name = "Пушкин" }}}
            });
    }
}