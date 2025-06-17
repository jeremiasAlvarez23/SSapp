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

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<RolMenu> RolMenu { get; set; }

    public virtual DbSet<Sistema> Sistema { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistema { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=dscent02;Database=Alpine;User Id=sa;Password=policia;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED23026B2B711");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Componente).HasMaxLength(100);
            entity.Property(e => e.Icono).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Ruta).HasMaxLength(255);

            entity.HasOne(d => d.Sistema).WithMany(p => p.Menu)
                .HasForeignKey(d => d.SistemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_Sistema");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F121577D8C");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolMenu__3214EC07D3FB0B5C");

            entity.HasOne(d => d.Menu).WithMany(p => p.RolMenu)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_RolMenu_Menu");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolMenu)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_RolMenu_Rol");
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.SistemaId).HasName("PK__Sistema__4C36BB86C1765091");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UrlBase).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B84C39AD37");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.SistemaId });

            entity.Property(e => e.RolSistema).HasMaxLength(100);

            entity.HasOne(d => d.Sistema).WithMany(p => p.UsuarioSistema)
                .HasForeignKey(d => d.SistemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioSistema_Sistema");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioSistema)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioSistema_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
