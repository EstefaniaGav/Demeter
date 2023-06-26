using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Proyectofinal.Models
{
    public partial class Insumo
    {
        public Insumo()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            Receta = new HashSet<Recetum>();
        }

        [Key]
        [Required]
        public int IdInsumo { get; set; }
        public string? NombreInsumo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public short? CantidadInsumo { get; set; }
        public string? Imagen { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<Recetum> Receta { get; set; }
    }
}
