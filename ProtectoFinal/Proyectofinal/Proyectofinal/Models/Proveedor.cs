using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Compras = new HashSet<Compra>();
        }
        [Key]
        [Required]
        public int IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Producto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Ciudad { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
