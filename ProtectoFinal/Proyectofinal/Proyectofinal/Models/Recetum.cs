using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Recetum
    {
        [Key]
        [Required]
        public int IdReceta { get; set; }
        public int? IdProducto { get; set; }
        public short? Cantidad { get; set; }
        public int? IdInsumo { get; set; }

        public virtual Insumo? IdInsumoNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
