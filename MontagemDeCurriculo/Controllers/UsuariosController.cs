using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemDeCurriculo.Models;

namespace MontagemDeCurriculo.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;

        public UsuariosController(Contexto context)
        {
            _context = context;
        }


        // GET: Usuarios/Create
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("UsuarioId,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                InformacaoLogin informacao = new InformacaoLogin
                {
                    UsuarioId = usuario.UsuarioId,
                    EnderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);


                return RedirectToAction("Index", "Curriculos");
            }
            return View(usuario);
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
