using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        [Key]
        [Required]
        public int IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? VentaRapida { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
