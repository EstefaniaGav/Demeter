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
    public class DetalleComprasController : Controller
    {
        private readonly ProyectoFinalDemeterContext _context;

        public DetalleComprasController(ProyectoFinalDemeterContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var proyectoFinalDemeterContext = _context.DetalleCompras.Include(d => d.IdCompraNavigation).Include(d => d.IdInsumoNavigation);
            return View(await proyectoFinalDemeterContext.ToListAsync());
        }

        // GET: DetalleCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdCompraNavigation)
                .Include(d => d.IdInsumoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleC == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            return View();
        }

        // POST: DetalleCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleC,IdInsumo,NombreInsumoC,CantidadI,IdCompra")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();

                // Sumar la cantidad del detalle de compra a la cantidad de insumos correspondiente
                var insumo = await _context.Insumos.FindAsync(detalleCompra.IdInsumo);
                if (insumo != null)
                {
                    insumo.CantidadInsumo += detalleCompra.CantidadI;
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detalleCompra.IdCompra);
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", detalleCompra.IdInsumo);
            return View(detalleCompra);
        }

    }
}