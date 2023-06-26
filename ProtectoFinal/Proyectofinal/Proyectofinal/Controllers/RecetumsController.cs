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
    public class RecetumsController : Controller
    {
        private readonly ProyectoFinalDemeterContext _context;

        public RecetumsController(ProyectoFinalDemeterContext context)
        {
            _context = context;
        }

        // GET: Recetums
        public async Task<IActionResult> Index()
        {
            var proyectoFinalDemeterContext = _context.Receta.Include(r => r.IdInsumoNavigation).Include(r => r.IdProductoNavigation);
            return View(await proyectoFinalDemeterContext.ToListAsync());
        }

        // GET: Recetums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receta == null)
            {
                return NotFound();
            }

            var recetum = await _context.Receta
                .Include(r => r.IdInsumoNavigation)
                .Include(r => r.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdReceta == id);
            if (recetum == null)
            {
                return NotFound();
            }

            return View(recetum);
        }

        // GET: Recetums/Create
        public IActionResult Create()
        {
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: Recetums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReceta,IdProducto,Cantidad,IdInsumo")] Recetum recetum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recetum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", recetum.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", recetum.IdProducto);
            return View(recetum);
        }

        // GET: Recetums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receta == null)
            {
                return NotFound();
            }

            var recetum = await _context.Receta.FindAsync(id);
            if (recetum == null)
            {
                return NotFound();
            }
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", recetum.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", recetum.IdProducto);
            return View(recetum);
        }

        // POST: Recetums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReceta,IdProducto,Cantidad,IdInsumo")] Recetum recetum)
        {
            if (id != recetum.IdReceta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recetum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetumExists(recetum.IdReceta))
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
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", recetum.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", recetum.IdProducto);
            return View(recetum);
        }

        // GET: Recetums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receta == null)
            {
                return NotFound();
            }

            var recetum = await _context.Receta
                .Include(r => r.IdInsumoNavigation)
                .Include(r => r.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdReceta == id);
            if (recetum == null)
            {
                return NotFound();
            }

            return View(recetum);
        }

        // POST: Recetums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receta == null)
            {
                return Problem("Entity set 'ProyectoFinalDemeterContext.Receta'  is null.");
            }
            var recetum = await _context.Receta.FindAsync(id);
            if (recetum != null)
            {
                _context.Receta.Remove(recetum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecetumExists(int id)
        {
          return (_context.Receta?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
    }
}
