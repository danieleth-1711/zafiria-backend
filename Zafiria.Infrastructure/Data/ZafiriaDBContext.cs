using Microsoft.EntityFrameworkCore;
using Zafiria.Core.Entities;

namespace Zafiria.Infrastructure.Data;

public class ZafiriaDbContext : DbContext
{
    public ZafiriaDbContext(DbContextOptions<ZafiriaDbContext> options)
        : base(options) { }

    public DbSet<Joya> Joyas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Ocasion> Ocasiones { get; set; }
    public DbSet<ImagenJoya> ImagenesJoya { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos iniciales - Categorias
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Mujeres", Descripcion = "Joyas para mujeres" },
            new Categoria { Id = 2, Nombre = "Niños", Descripcion = "Joyas para niños" },
            new Categoria { Id = 3, Nombre = "Varones", Descripcion = "Joyas para varones" }
        );

        // Datos iniciales - Ocasiones
        modelBuilder.Entity<Ocasion>().HasData(
            new Ocasion { Id = 1, Nombre = "Boda", Descripcion = "Joyas para bodas" },
            new Ocasion { Id = 2, Nombre = "Bautizo", Descripcion = "Joyas para bautizos" },
            new Ocasion { Id = 3, Nombre = "Graduacion", Descripcion = "Joyas para graduaciones" },
            new Ocasion { Id = 4, Nombre = "Compromiso", Descripcion = "Joyas para compromisos" },
            new Ocasion { Id = 5, Nombre = "Profesion", Descripcion = "Joyas para profesionales" }
        );
    }
}