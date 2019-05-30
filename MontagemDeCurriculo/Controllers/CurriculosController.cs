using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemDeCurriculo.Models;
using MontagemDeCurriculo.ViewModels;
using Rotativa.AspNetCore;

namespace MontagemDeCurriculo.Controllers
{
    public class CurriculosController : Controller
    {
        private readonly Contexto _context;

        public CurriculosController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contexto = await _context.Curriculos.Include(c => c.Usuario).ToListAsync();
            return View(contexto);
        }

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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("UsuarioId").ToString());
            if (ModelState.IsValid)
            {
                _context.Add(curriculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curriculo);
        }

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
            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("UsuarioId").ToString());
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
            return View(curriculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var curriculo = await _context.Curriculos.FindAsync(id);
            _context.Curriculos.Remove(curriculo);
            await _context.SaveChangesAsync();
            return Json(curriculo.Nome + "excluido com sucesso");
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.CurriculoId == id);
        }

        public IActionResult VisualizarComoPDF()
        {
            var id = HttpContext.Session.GetInt32("UsuarioId");

            var curriculo = new CurriculoViewModel
            {
                Objetivos = _context.Objetivos.Where(x => x.Curriculo.UsuarioId == id).ToList(),
                FormacaoAcademicas = _context.FormacoesAcademicas.Include(x => x.TipoCurso).Where(x => x.Curriculo.UsuarioId == id).ToList(),
                ExperienciaProfissionals = _context.ExperienciaProfissionais.Where(x => x.Curriculo.UsuarioId == id).ToList(),
                Idiomas = _context.Idiomas.Where(x => x.Curriculo.UsuarioId == id).ToList()
            };

            return new ViewAsPdf("PDF", curriculo) { FileName = "Curriculo.pdf" };
        }
    }
}
