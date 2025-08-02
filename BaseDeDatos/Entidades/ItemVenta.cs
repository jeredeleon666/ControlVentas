using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVentas.BaseDeDatos.Entidades
{
    [Table("ItemVenta")]
    public class ItemVenta
    {
        [Key]
        [Column("ItemVenta")]
        public int id { get; set; }

        [ForeignKey(nameof(Venta))]
        [Column("CodigoVenta")]
        public int codigoVenta {  get; set; }

        //puntero hacia el producto
        [ForeignKey(nameof(Producto))]
        [Column("CodigoProducto")]
        public int codigoProducto { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio establecido para el producto es invalido")]
        [Column("Cantidad")]
        public int Cantidad {  get; set; }  

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad vendida debe de ser mayor a 0")]
        [Column("PrecioUnitario", TypeName = "decimal(10,2")]
        public decimal precioUnitario { get; set;}

        //Vistas virtuales para navegacion
        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
