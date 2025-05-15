using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Data;

public partial class SecuritysContext : DbContext
{
    public SecuritysContext()
    {
    }

    public SecuritysContext(DbContextOptions<SecuritysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:securitysapp.database.windows.net,1433;Initial Catalog=securitysapp;Persist Security Info=False;User ID=securityadmin;Password=adminpuky1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8160BEE2D");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
