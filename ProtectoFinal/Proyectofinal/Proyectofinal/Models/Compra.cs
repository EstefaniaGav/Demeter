using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public partial class Compra
    {
        
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }
        [Key]
        [Required]
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Proveedor? IdProveedorNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
