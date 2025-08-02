using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVentas.BaseDeDatos.Entidades
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        [Column("CodigoVenta")]
        public int CodigoVenta { get; set; }

        [Required]
        [Column("Fecha", TypeName = "date")]
        public DateTime Fecha { get; set; }

        //Puntero hacia los items de ventas en una relacion de 1:N
        public virtual ICollection<ItemVenta> items { get; set; } = new List<ItemVenta>();
        public int CodigoProducto { get; set; }

        //Vista virtual para consultas
        public virtual Producto Producto { get; set; }
    }
}
