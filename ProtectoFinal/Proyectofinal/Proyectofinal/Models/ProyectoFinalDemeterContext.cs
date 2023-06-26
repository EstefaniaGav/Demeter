using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyectofinal.Models
{
    public partial class ProyectoFinalDemeterContext : DbContext
    {
        public ProyectoFinalDemeterContext()
        {
        }

        public ProyectoFinalDemeterContext(DbContextOptions<ProyectoFinalDemeterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Recetum> Receta { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog= ProyectoFinalDemeter;integrated security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__A9D5994E5B7350C8");

                entity.Property(e => e.IdCompra).HasColumnName("ID_Compra");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Compra")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Compras__ID_Prov__440B1D61");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Compras__ID_Usua__44FF419A");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleC)
                    .HasName("PK__Detalle___CAC07C0368746882");

                entity.ToTable("Detalle_Compras");

                entity.Property(e => e.IdDetalleC).HasColumnName("ID_Detalle_C");

                entity.Property(e => e.CantidadI).HasColumnName("Cantidad_I");

                entity.Property(e => e.IdCompra).HasColumnName("ID_Compra");

                entity.Property(e => e.IdInsumo).HasColumnName("ID_Insumo");

                entity.Property(e => e.NombreInsumoC)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Insumo_C");

                entity.Property(e => e.ValorCompra)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Valor_Compra");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__Detalle_C__ID_Co__48CFD27E");

                entity.HasOne(d => d.IdInsumoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdInsumo)
                    .HasConstraintName("FK__Detalle_C__ID_In__47DBAE45");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__Detalle___0157010A33C2B46E");

                entity.ToTable("Detalle_Ventas");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("ID_DetalleVenta");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Detalle_V__ID_Pr__5441852A");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__Detalle_V__ID_Ve__534D60F1");
            });

            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.HasKey(e => e.IdInsumo)
                    .HasName("PK__Insumos__2C6ADA07DE6316CB");

                entity.Property(e => e.IdInsumo).HasColumnName("ID_Insumo");

                entity.Property(e => e.CantidadInsumo).HasColumnName("Cantidad_Insumo");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Vencimiento")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.NombreInsumo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Insumo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Insumos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Insumos__ID_Usua__3D5E1FD2");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__9B4120E2095754CF");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Productos__ID_Us__4BAC3F29");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__7D65272FA19A742D");

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Proveedor__ID_Us__403A8C7D");
            });

            modelBuilder.Entity<Recetum>(entity =>
            {
                entity.HasKey(e => e.IdReceta)
                    .HasName("PK__Receta__19C0463156B079CE");

                entity.Property(e => e.IdReceta).HasColumnName("ID_Receta");

                entity.Property(e => e.IdInsumo).HasColumnName("ID_Insumo");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.HasOne(d => d.IdInsumoNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdInsumo)
                    .HasConstraintName("FK__Receta__ID_Insum__5812160E");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Receta__ID_Produ__571DF1D5");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__202AD2209E5C21F2");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__DE4431C510CCED92");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.CedulaUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cedula_Usuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Usuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuarios__ID_Rol__398D8EEE");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Ventas__3CD842E55FCA6E6E");

                entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");

                entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Iva)
                    .HasColumnType("numeric(13, 4)")
                    .HasComputedColumnSql("([Total]*(0.19))", false);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.VentaRapida).HasColumnName("Venta_Rapida");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Ventas__ID_Produ__4E88ABD4");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Ventas__ID_Usuar__5070F446");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
