using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class FacturaDetallesController : Controller
    {
        private readonly DbcrudContext _context;

        public FacturaDetallesController(DbcrudContext context)
        {
            _context = context;
        }

        // GET: FacturaDetalles
        public async Task<IActionResult> Index()
        {
            var dbcrudContext = _context.FacturaDetalles.Include(f => f.Factura).Include(f => f.Producto);
            return View(await dbcrudContext.ToListAsync());
        }

        // GET: FacturaDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles
                .Include(f => f.Factura)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(m => m.FacturaDetalleId == id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }

            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId");
            return View();
        }

        // POST: FacturaDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,FacturaDetalleId,ProductoId,Precio")] FacturaDetalle facturaDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturaDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", facturaDetalle.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", facturaDetalle.ProductoId);
            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles.FindAsync(id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", facturaDetalle.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", facturaDetalle.ProductoId);
            return View(facturaDetalle);
        }

        // POST: FacturaDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,FacturaDetalleId,ProductoId,Precio")] FacturaDetalle facturaDetalle)
        {
            if (id != facturaDetalle.FacturaDetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaDetalleExists(facturaDetalle.FacturaDetalleId))
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
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", facturaDetalle.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", facturaDetalle.ProductoId);
            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles
                .Include(f => f.Factura)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(m => m.FacturaDetalleId == id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }

            return View(facturaDetalle);
        }

        // POST: FacturaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturaDetalle = await _context.FacturaDetalles.FindAsync(id);
            if (facturaDetalle != null)
            {
                _context.FacturaDetalles.Remove(facturaDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaDetalleExists(int id)
        {
            return _context.FacturaDetalles.Any(e => e.FacturaDetalleId == id);
        }
    }
}
