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
}
    