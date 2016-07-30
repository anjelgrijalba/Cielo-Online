namespace CIELO_TM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BaseDatos : DbContext
    {
        public BaseDatos()
            : base("name=BaseDatos1")
        {
        }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CARTS> CARTS { get; set; }
        public virtual DbSet<CATEGORIA> CATEGORIA { get; set; }
        public virtual DbSet<COMENTARIO> COMENTARIO { get; set; }
        public virtual DbSet<DETALLES_ORDEN> DETALLES_ORDEN { get; set; }
        public virtual DbSet<INVENTARIO> INVENTARIO { get; set; }
        public virtual DbSet<MARCA> MARCA { get; set; }
        public virtual DbSet<ORDEN> ORDEN { get; set; }
        public virtual DbSet<PRODUCTOS> PRODUCTOS { get; set; }
        public virtual DbSet<PROVEDORES> PROVEDORES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.COMENTARIO)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.ID_USUARIO);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.ORDEN)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.cliente_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORIA>()
                .Property(e => e.CATEGORIA1)
                .IsUnicode(false);

            modelBuilder.Entity<COMENTARIO>()
                .Property(e => e.COMENTARIO1)
                .IsUnicode(false);

            modelBuilder.Entity<MARCA>()
                .Property(e => e.MARCA1)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.IMAGEN1)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.IMAGEN2)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.IMAGEN3)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.PRODUCTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.PRECIO_VENTA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRODUCTOS>()
                .Property(e => e.GENERO)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTOS>()
                .HasMany(e => e.COMENTARIO)
                .WithRequired(e => e.PRODUCTOS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCTOS>()
                .HasMany(e => e.DETALLES_ORDEN)
                .WithOptional(e => e.PRODUCTOS)
                .HasForeignKey(e => e.ProductoId);

            modelBuilder.Entity<PROVEDORES>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEDORES>()
                .Property(e => e.APELLIDO)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEDORES>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEDORES>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEDORES>()
                .Property(e => e.CORREO)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEDORES>()
                .HasMany(e => e.INVENTARIO)
                .WithOptional(e => e.PROVEDORES)
                .HasForeignKey(e => e.ID_PROVEDOR);

            modelBuilder.Entity<PROVEDORES>()
                .HasMany(e => e.PRODUCTOS)
                .WithRequired(e => e.PROVEDORES)
                .HasForeignKey(e => e.ID_PROVEDOR);
        }
    }
}
