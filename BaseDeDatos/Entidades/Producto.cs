using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVentas.BaseDeDatos.Entidades
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [Column("CodigoProducto")]
        public int CodigoProducto { get; set;}

        [Required]
        [StringLength(200)]
        [Column("Nombre")]
        public String Nombre { get; set; }

        [Required]
        [Column("Precio", TypeName = "decimal(10,2")]
        public decimal Precio;

        //Puntero hacia la categoria como esta establecido en la relacion N:1
        //Lo que limita a que un producto solo puede tener una categoria y las categorias
        //tengan varios productos
       
        [ForeignKey(nameof(Categoria))]
        [Column("CodigoCategoria")]
        public int CodigoCategoria { get; set; }


        
        //Propiedad para generar una tabla virtual
        public virtual Categoria Categoria { get; set;}

    }
}
