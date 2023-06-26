using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class DetalleVenta
    {
        [Key]
        [Required]
        public int IdDetalleVenta { get; set; }
        public short? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public int? IdVenta { get; set; }
        public int IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; }
    }
}
