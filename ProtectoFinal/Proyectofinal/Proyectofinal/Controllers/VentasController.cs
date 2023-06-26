using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class VentasController : Controller
    {
        private readonly ProyectoFinalDemeterContext _context;

        public VentasController(ProyectoFinalDemeterContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var proyectoFinalDemeterContext = _context.Ventas.Include(v => v.IdProductoNavigation).Include(v => v.IdUsuarioNavigation);
            return View(await proyectoFinalDemeterContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdProductoNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,IdProducto,Fecha,VentaRapida,IdUsuario")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                venta.Iva = 19;

                _context.Add(venta);
                await _context.SaveChangesAsync();

                // Obtener el IdVenta asignado después de guardar
                int idVenta = venta.IdVenta;

                // Obtener los detalles de venta asociados a la venta
                var detallesVenta = await _context.DetalleVentas
                    .Where(dv => dv.IdVenta == idVenta)
                    .ToListAsync();

               
                await _context.SaveChangesAsync();

                // Redireccionar a la página de DetalleVentas con el IdVenta
                return RedirectToAction("Create", "DetalleVentas", new { idVenta = venta.IdVenta });
            }

            // Si hay un error en el ModelState, volver a cargar los datos en la vista
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", venta.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            ViewBag.Title = "Create";
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", venta.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            ViewBag.Title = "Edit";
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,IdProducto,Fecha,VentaRapida,IdUsuario,SubTotal,Iva,Total")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", venta.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.IdUsuario);
            ViewBag.Title = "Edit";
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdProductoNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.IdVenta == id);
        }

        // GET: Ventas/CalculateSubtotalAndTotal
        [HttpGet]
        public async Task<IActionResult> ConfirmarVenta(int? id, DetalleVenta detalleVenta)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Cargar los detalles de venta incluyendo la propiedad de navegación "Productos"
            var detallesVenta = await _context.DetalleVentas
                .Include(d => d.IdProducto)
                .Where(d => d.IdVenta == id)
                .ToListAsync();

            // Calcula el total de la venta sumando los precios totales de los detalles de venta
            decimal subtotalVenta = detallesVenta.Sum(d => d.IdProductoNavigation?.Precio * d.Cantidad ?? 0);
  
            // Agrega el monto del pago a domicilio al total de la venta
            decimal? totalVenta= subtotalVenta * (1 + venta.Iva / 100);

            // Asigna el total de la venta a la propiedad TotalVenta del modelo Venta
            venta.Total = subtotalVenta + totalVenta;

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "Ventas");
        }

    }
}


