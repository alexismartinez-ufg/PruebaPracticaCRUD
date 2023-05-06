using Microsoft.EntityFrameworkCore;
using PruebaCRUD.Models;

public class LibrosContext : DbContext
{
    public LibrosContext(DbContextOptions<LibrosContext> options)
            : base(options)
    {

    }

    public DbSet<Libro> Libros { get; set; }
    public DbSet<Reservacion> Reservaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
