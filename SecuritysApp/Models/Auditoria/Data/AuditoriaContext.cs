using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecuritysApp.Models.Auditoria.Entidades;

namespace SecuritysApp.Models.Auditoria.Data;

public partial class AuditoriaContext : DbContext
{
    public AuditoriaContext()
    {
    }

    public AuditoriaContext(DbContextOptions<AuditoriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditoriaMain> AuditoriaMain { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=dscent02;Database=Alpine2;User Id=sa;Password=policia;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriaMain>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PK__Auditori__095694C3140003AA");

            entity.Property(e => e.Accion).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Ip).HasMaxLength(50);
            entity.Property(e => e.Navegador).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Sistema).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
