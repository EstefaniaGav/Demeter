using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public partial class DetalleCompra
    {
        [Key]
        [Required]
        public int IdDetalleC { get; set; }
        public int? IdInsumo { get; set; }
        public string? NombreInsumoC { get; set; }
        public decimal? ValorCompra { get; set; }
        public short? CantidadI { get; set; }
        public int? IdCompra { get; set; }

        public virtual Compra? IdCompraNavigation { get; set; }
        public virtual Insumo? IdInsumoNavigation { get; set; }
    }
}
