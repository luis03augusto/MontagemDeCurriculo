using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MontagemDeCurriculo.ViewComponents
{
    public class FormacaoAcademicasViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public FormacaoAcademicasViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.FormacoesAcademicas.Include(x => x.TipoCurso).Where(x => x.CurriculoId == id).ToListAsync());
        }
    }
}
