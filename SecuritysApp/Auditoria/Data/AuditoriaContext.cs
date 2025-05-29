using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecuritysApp.Auditoria.Entidades;

namespace SecuritysApp.Auditoria.Data;

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
        => optionsBuilder.UseSqlServer("Server=tcp:securitysapp.database.windows.net,1433;Initial Catalog=securitysauditoria;Persist Security Info=False;User ID=securityadmin;Password=adminpuky1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriaMain>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PK__Auditori__095694C37544B958");

            entity.Property(e => e.Accion).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Exitoso).HasDefaultValue(true);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Ip).HasMaxLength(45);
            entity.Property(e => e.Navegador).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Sistema).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
