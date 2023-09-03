using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PermisosMVC.Models.DB;

namespace WebApiYerbas.Models.ModelsUsuario
{
    public partial class MiSistema02Context : DbContext
    {
        public MiSistema02Context()
        {
        }

        public MiSistema02Context(DbContextOptions<MiSistema02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Modulo> Modulos { get; set; }

        public virtual DbSet<Operacione> Operaciones { get; set; }

        public virtual DbSet<Rol> Rols { get; set; }

        public virtual DbSet<RolOperacion> RolOperacions { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=.; Database=MiSistema02; Trusted_Connection=True; TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("Modulo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operacione>(entity =>
            {
                entity.ToTable("Operaciones");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.Operaciones)
                    .HasForeignKey(d => d.IdModulo)
                    .HasConstraintName("FK_Operaciones_Modulo");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolOperacion>(entity =>
            {
                entity.ToTable("Rol_Operacion");

                entity.HasOne(d => d.IdOperacionNavigation).WithMany(p => p.RolOperacions)
                    .HasForeignKey(d => d.IdOperacion)
                    .HasConstraintName("FK_Rol_Operacion_Operaciones");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolOperacions)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Rol_Operacion_Rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
