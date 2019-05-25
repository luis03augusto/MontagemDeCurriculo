using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemDeCurriculo.Models;

namespace MontagemDeCurriculo.Controllers
{
    public class CurriculoesController : Controller
    {
        private readonly Contexto _context;

        public CurriculoesController(Contexto context)
        {
            _context = context;
        }

        // GET: Curriculoes
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Curriculos.Include(c => c.Usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Curriculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoId == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // GET: Curriculoes/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email");
            return View();
        }

        // POST: Curriculoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curriculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", curriculo.UsuarioId);
            return View(curriculo);
        }

        // GET: Curriculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", curriculo.UsuarioId);
            return View(curriculo);
        }

        // POST: Curriculoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            if (id != curriculo.CurriculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculoExists(curriculo.CurriculoId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", curriculo.UsuarioId);
            return View(curriculo);
        }

        // GET: Curriculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoId == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // POST: Curriculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curriculo = await _context.Curriculos.FindAsync(id);
            _context.Curriculos.Remove(curriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.CurriculoId == id);
        }
    }
}
