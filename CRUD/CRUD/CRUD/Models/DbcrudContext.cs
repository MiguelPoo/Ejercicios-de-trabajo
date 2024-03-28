using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Models;

public partial class DbcrudContext : DbContext
{
    public DbcrudContext()
    {
    }

    public DbcrudContext(DbContextOptions<DbcrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("server=.\\SQLExpress;Database=DBcrud;Trusted_Connection=True;Trust Server Certificate=true");
        }
        


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Factura");

            entity.Property(e => e.FacturaId)
                .ValueGeneratedNever()
                .HasColumnName("FacturaID");
            entity.Property(e => e.Total)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.ToTable("FacturaDetalle");

            entity.Property(e => e.FacturaDetalleId)
                .ValueGeneratedNever()
                .HasColumnName("FacturaDetalleID");
            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.Precio).HasColumnType("money");
            entity.Property(e => e.ProductoId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ProductoID");

            entity.HasOne(d => d.Factura).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK_FacturaDetalle_Factura");

            entity.HasOne(d => d.Producto).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_FacturaDetalle_FacturaDetalle");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Producto");

            entity.Property(e => e.ProductoId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ProductoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
