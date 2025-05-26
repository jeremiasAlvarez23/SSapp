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

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Sistema> Sistema { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistema { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:securitysapp.database.windows.net,1433;Initial Catalog=securitysapp;Persist Security Info=False;User ID=securityadmin;Password=adminpuky1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F1F72097D6");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.SistemaId).HasName("PK__Sistema__4C36BB86FD1C1546");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UrlBase).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8C4CFA24F");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534F57F84F0").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.SistemaId }).HasName("PK__UsuarioS__5FFE8C0012FDE5BE");

            entity.Property(e => e.RolSistema).HasMaxLength(50);
            entity.Property(e => e.TieneAcceso).HasDefaultValue(true);

            entity.HasOne(d => d.Sistema).WithMany(p => p.UsuarioSistema)
                .HasForeignKey(d => d.SistemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSi__Siste__7A672E12");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioSistema)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSi__Usuar__797309D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
