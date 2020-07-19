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

       
        public virtual DbSet<Beneficiarios> Beneficiarios { get; set; }
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
                optionsBuilder.UseSqlServer("Server=DESKTOP-DJ6VOP6\\SQLEXPRESS;Database=Ibanking;persist security info=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Beneficiarios>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsFixedLength();

                entity.Property(e => e.Idcreador)
                    .IsRequired()
                    .HasColumnName("idcreador")
                    .HasMaxLength(450);

                entity.Property(e => e.NoCuenta)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .IsFixedLength();

                entity.HasOne(d => d.IdcreadorNavigation)
                    .WithMany(p => p.Beneficiarios)
                    .HasForeignKey(d => d.Idcreador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiarios_Users");
            });

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
            });

            modelBuilder.Entity<ProductosUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Balance).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Idtipo).HasColumnName("idtipo");

                entity.Property(e => e.Idusuario)
                    .IsRequired()
                    .HasColumnName("idusuario")
                    .HasMaxLength(450);

                entity.Property(e => e.LimiteTarjeta).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MontoPrestamo).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .HasMaxLength(20)
                    .IsUnicode(false);
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

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
       
    }
}
