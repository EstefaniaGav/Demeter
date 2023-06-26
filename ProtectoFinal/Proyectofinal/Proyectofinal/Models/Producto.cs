using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
            Receta = new HashSet<Recetum>();
            Venta = new HashSet<Venta>();
        }
        [Key]
        [Required]
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? Precio { get; set; }
        public string? Imagen { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
        public virtual ICollection<Recetum> Receta { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
