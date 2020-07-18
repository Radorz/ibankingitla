using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    public partial class IbankingContext : IdentityDbContext
    {
        public IbankingContext()
        {
        }

        public IbankingContext(DbContextOptions<IbankingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<ProductosUsers> ProductosUsers { get; set; }
        public virtual DbSet<TiposProductos> TiposProductos { get; set; }
        public virtual DbSet<Transacciones> Transacciones { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5VA8VTG\\MSSQLSERVER01;Database=Ibanking;persist security info=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pagos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ProductoDestino)
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.Property(e => e.ProductoOrigen)
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.HasOne(d => d.ProductoDestinoNavigation)
                    .WithMany(p => p.PagosProductoDestinoNavigation)
                    .HasForeignKey(d => d.ProductoDestino)
                    .HasConstraintName("FK_Table_1_ProductosUsersDestino");

                entity.HasOne(d => d.ProductoOrigenNavigation)
                    .WithMany(p => p.PagosProductoOrigenNavigation)
                    .HasForeignKey(d => d.ProductoOrigen)
                    .HasConstraintName("FK_Pagos_ProductosUsers");

                entity.HasOne(d => d.TipoProductoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.TipoProducto)
                    .HasConstraintName("FK_Pagos_TiposProductos");
            });

            modelBuilder.Entity<ProductosUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Idtipo).HasColumnName("idtipo");

                entity.Property(e => e.Idusuario)
                    .IsRequired()
                    .HasColumnName("idusuario")
                    .HasMaxLength(450);
            
                entity.Property(e => e.tipo)
                   .IsRequired()
                   .HasColumnName("tipo")
                   .HasMaxLength(20);

                entity.Property(e => e.Balance).HasColumnType("decimal(19, 2)");


                entity.HasOne(d => d.IdtipoNavigation)
                    .WithMany(p => p.ProductosUsers)
                    .HasForeignKey(d => d.Idtipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductosUsers_TiposProductos");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.ProductosUsers)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductosUsers_Users");
            });

            modelBuilder.Entity<TiposProductos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Transacciones>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cuentadestino)
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.Property(e => e.Cuentaorigen)
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.CuentadestinoNavigation)
                    .WithMany(p => p.TransaccionesCuentadestinoNavigation)
                    .HasForeignKey(d => d.Cuentadestino)
                    .HasConstraintName("FK_Transacciones_ProductosUsers");

                entity.HasOne(d => d.CuentaorigenNavigation)
                    .WithMany(p => p.TransaccionesCuentaorigenNavigation)
                    .HasForeignKey(d => d.Cuentaorigen)
                    .HasConstraintName("FK_Transacciones_ProductosUsersorigen");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
