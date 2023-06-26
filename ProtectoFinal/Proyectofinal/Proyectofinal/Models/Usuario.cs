using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
            Insumos = new HashSet<Insumo>();
            Productos = new HashSet<Producto>();
            Proveedors = new HashSet<Proveedor>();
            Venta = new HashSet<Venta>();
        }

        [Key]
        [Required]
        public int IdUsuario { get; set; }
        public string? CedulaUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contraseña { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Insumo> Insumos { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
