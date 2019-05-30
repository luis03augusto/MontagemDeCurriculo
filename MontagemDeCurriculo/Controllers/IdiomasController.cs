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
    public class IdiomasController : Controller
    {
        private readonly Contexto _context;

        public IdiomasController(Contexto context)
        {
            _context = context;
        }
  
        public IActionResult Create(int id)
        {
            var idioma = new Idioma
            {
                CurriculoId = id
            };
            return View(idioma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdiomaId,Nome,Nivel,CurriculoId")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idioma);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Curriculo", new { id = idioma.CurriculoId});
            }            
            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas.FindAsync(id);
            if (idioma == null)
            {
                return NotFound();
            }
            
            return View(idioma);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdiomaId,Nome,Nivel,CurriculoId")] Idioma idioma)
        {
            if (id != idioma.IdiomaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idioma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdiomaExists(idioma.IdiomaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculo", new { id = idioma.CurriculoId });

            }
            return View(idioma);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var idioma = await _context.Idiomas.FindAsync(id);
            _context.Idiomas.Remove(idioma);
            await _context.SaveChangesAsync();
            return Json("Idioma excluído com sucesso.");
        }

        private bool IdiomaExists(int id)
        {
            return _context.Idiomas.Any(e => e.IdiomaId == id);
        }
    }
}
