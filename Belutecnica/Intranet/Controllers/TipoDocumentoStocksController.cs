using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intranet.Data;
using Intranet.Models;
using Intranet.Models.Stock;
using Microsoft.AspNetCore.Authorization;

namespace Intranet
{
    [Authorize]
    public class TipoDocumentoStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoDocumentoStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoDocumentoStocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDocumentoStock.ToListAsync());
        }

        // GET: TipoDocumentoStocks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumentoStock = await _context.TipoDocumentoStock
                .FirstOrDefaultAsync(m => m.documento == id);
            if (tipoDocumentoStock == null)
            {
                return NotFound();
            }

            return View(tipoDocumentoStock);
        }

        // GET: TipoDocumentoStocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDocumentoStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("documento,tipo,descricao")] TipoDocumentoStock tipoDocumentoStock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDocumentoStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDocumentoStock);
        }

        // GET: TipoDocumentoStocks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumentoStock = await _context.TipoDocumentoStock.FindAsync(id);
            if (tipoDocumentoStock == null)
            {
                return NotFound();
            }
            return View(tipoDocumentoStock);
        }

        // POST: TipoDocumentoStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("documento,tipo,descricao")] TipoDocumentoStock tipoDocumentoStock)
        {
            if (id != tipoDocumentoStock.documento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDocumentoStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDocumentoStockExists(tipoDocumentoStock.documento))
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
            return View(tipoDocumentoStock);
        }

        // GET: TipoDocumentoStocks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumentoStock = await _context.TipoDocumentoStock
                .FirstOrDefaultAsync(m => m.documento == id);
            if (tipoDocumentoStock == null)
            {
                return NotFound();
            }

            return View(tipoDocumentoStock);
        }

        // POST: TipoDocumentoStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tipoDocumentoStock = await _context.TipoDocumentoStock.FindAsync(id);
            _context.TipoDocumentoStock.Remove(tipoDocumentoStock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDocumentoStockExists(string id)
        {
            return _context.TipoDocumentoStock.Any(e => e.documento == id);
        }
    }
}
