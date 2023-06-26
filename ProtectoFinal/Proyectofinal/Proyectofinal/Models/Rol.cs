using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }
        [Key]
        [Required]
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
