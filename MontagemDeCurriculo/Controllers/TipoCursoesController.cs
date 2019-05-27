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
    public class TipoCursoesController : Controller
    {
        private readonly Contexto _context;

        public TipoCursoesController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoCursoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposCursos.ToListAsync());
        }

      

        // GET: TipoCursoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCursoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoCursoId,Tipo")] TipoCurso tipoCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCurso);
        }

        // GET: TipoCursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCurso = await _context.TiposCursos.FindAsync(id);
            if (tipoCurso == null)
            {
                return NotFound();
            }
            return View(tipoCurso);
        }

        // POST: TipoCursoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoCursoId,Tipo")] TipoCurso tipoCurso)
        {
            if (id != tipoCurso.TipoCursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCursoExists(tipoCurso.TipoCursoId))
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
            return View(tipoCurso);
        }
              
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tipoCurso = await _context.TiposCursos.FindAsync(id);
            _context.TiposCursos.Remove(tipoCurso);
            await _context.SaveChangesAsync();
            return Json(tipoCurso.Tipo + " excluido com sucesso.");
        }

        private bool TipoCursoExists(int id)
        {
            return _context.TiposCursos.Any(e => e.TipoCursoId == id);
        }
    }
}
