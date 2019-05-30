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
    public class FormacoesAcademicasController : Controller
    {
        private readonly Contexto _context;

        public FormacoesAcademicasController(Contexto context)
        {
            _context = context;
        }

      

        // GET: FormacoesAcademicas/Create
        public IActionResult Create(int id)
        {
            var formcao = new FormacaoAcademica
            {
                CurriculoId = id
            };
            ViewData["TipoCursoId"] = new SelectList(_context.TiposCursos, "TipoCursoId", "Tipo");
            return View(formcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormacaoAcademicaId,TipoCursoId,Instituicao,AnoInicio,AnoFim,NomeCurso,CurriculoId")] FormacaoAcademica formacaoAcademica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formacaoAcademica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Datails", "Curriculos", new { id = formacaoAcademica.CurriculoId});
            }
            
            ViewData["TipoCursoId"] = new SelectList(_context.TiposCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

        // GET: FormacoesAcademicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacaoAcademica = await _context.FormacoesAcademicas.FindAsync(id);
            if (formacaoAcademica == null)
            {
                return NotFound();
            }            
            ViewData["TipoCursoId"] = new SelectList(_context.TiposCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormacaoAcademicaId,TipoCursoId,Instituicao,AnoInicio,AnoFim,NomeCurso,CurriculoId")] FormacaoAcademica formacaoAcademica)
        {
            if (id != formacaoAcademica.FormacaoAcademicaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formacaoAcademica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormacaoAcademicaExists(formacaoAcademica.FormacaoAcademicaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = formacaoAcademica.CurriculoId });
            }           
            ViewData["TipoCursoId"] = new SelectList(_context.TiposCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

            
        [HttpPost]

        public async Task<JsonResult> Delete(int id)
        {
            var formacaoAcademica = await _context.FormacoesAcademicas.FindAsync(id);
            _context.FormacoesAcademicas.Remove(formacaoAcademica);
            await _context.SaveChangesAsync();
            return Json("Formcação acadêmica excluída");
        }

        private bool FormacaoAcademicaExists(int id)
        {
            return _context.FormacoesAcademicas.Any(e => e.FormacaoAcademicaId == id);
        }
    }
}
