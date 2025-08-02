using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVentas.BaseDeDatos.Entidades
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column("CodigoCategoria")]
        public int CodigoCategoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Nombre")]
        public string Nombre { get; set; }

        //Los productos se relacionan con la categoria de N:1
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
