using Microsoft.EntityFrameworkCore;
using ControlVentas.BaseDeDatos.Entidades;

namespace ControlVentas.BaseDeDatos.Context
{
    public class VentasDbContext : DbContext
    {
        public VentasDbContext(DbContextOptions<VentasDbContext> opciones): base(opciones) { }

        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Venta> ventas {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //configuracion del codigo que maneja la tabla de categorias
            modelBuilder.Entity<Categoria>(Entidad =>
            {
                Entidad.HasKey(c => c.CodigoCategoria);
                Entidad.Property(c => c.CodigoCategoria).ValueGeneratedOnAdd();
                Entidad.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            });

            //configuracion del codigo que maneja la tabla de productos
            modelBuilder.Entity<Producto>(Entidad =>
            {
                Entidad.HasKey(p => p.CodigoProducto);
                Entidad.Property(p => p.CodigoProducto).ValueGeneratedOnAdd();
                Entidad.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
                Entidad.Property(p => p.Precio).IsRequired().HasColumnType("decimal(10.2");

                Entidad.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CodigoCategoria)
                .OnDelete(DeleteBehavior.Restrict);
                });

            //Tabla intermedia para conectar prodiuctos con ventas y tener una venta real
            //sin las limitantes de una venta por producto
            modelBuilder.Entity<ItemVenta>(Entidad =>
            {
                Entidad.HasKey(iv => iv.id);
                Entidad.Property(iv => iv.id).ValueGeneratedOnAdd();
                Entidad.Property(iv => iv.Cantidad).IsRequired();

                //Puntero hacia la venta de la que depende el item al ser una entidad intermedia
                //en una relacion N:1
                Entidad.HasOne<Venta>(iv => iv.Venta)
                .WithMany(v => v.items) //todos los items de la venta
                .HasForeignKey(iv => iv.codigoVenta) 
                .OnDelete(DeleteBehavior.Cascade);

                //Puntero hacia el producto con una relacion 1:N 
                Entidad.HasOne(iv => iv.Producto)
                .WithMany()
                .HasForeignKey(iv => iv.codigoProducto)
                .OnDelete(DeleteBehavior.Restrict);

                //Indices para optimizar consultas debido a que sera una tabla con gran concurrencia de datos
                Entidad.HasIndex(iv => iv.codigoVenta)
                .HasDatabaseName("IX_ItemVenta_CodigoVenta");

                Entidad.HasIndex(iv => iv.codigoProducto)
                .HasDatabaseName("IX_ItemVenta_CodigoProducto");

                //Indice para generar reportes por fecha
                Entidad.HasIndex(iv => new { iv.codigoProducto, iv.codigoVenta })
                .HasDatabaseName("IX_ItemVenta_Producto_Venta");
            });

            //configuracion del codigo que maneja la tabla de ventas
            modelBuilder.Entity<Venta>(Entidad =>
            {
                Entidad.HasKey(v => v.CodigoVenta);
                Entidad.Property(v => v.CodigoVenta).ValueGeneratedOnAdd();
                Entidad.Property(v => v.Fecha).IsRequired().HasColumnType("date");

                //puntero hacia los indices 
                Entidad.HasMany(v => v.items)
                .WithOne(iv => iv.Venta)
                .HasForeignKey(iv => iv.codigoVenta)
                .OnDelete(DeleteBehavior.Cascade);

                Entidad.HasIndex(v => v.Fecha)
                .HasDatabaseName("IX_Venta_Fecha");
                
            }
            );
        }
    }
}
