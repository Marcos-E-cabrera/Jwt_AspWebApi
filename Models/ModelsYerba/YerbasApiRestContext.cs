using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiYerbas.Models.ModelsYerba;

public partial class YerbasApiRestContext : DbContext
{
    public YerbasApiRestContext()
    {
    }

    public YerbasApiRestContext(DbContextOptions<YerbasApiRestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Yerba> Yerbas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.; Database=YerbasApiRest; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Yerba>(entity =>
        {
            entity.ToTable("yerbas");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
