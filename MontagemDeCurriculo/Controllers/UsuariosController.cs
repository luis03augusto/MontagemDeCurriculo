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
using MontagemDeCurriculo.ViewModels;

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
                InformacaoLogin informacao = new InformacaoLogin
                {
                    UsuarioId = usuario.UsuarioId,
                    EnderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                _context.Add(informacao);
                await _context.SaveChangesAsync();

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

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid && _context.Usuarios.Any(x => x.Email == login.Email && x.Senha == login.Senha))
            {

                int id = _context.Usuarios.Where(x => x.Email == login.Email && x.Senha == login.Senha).Select(x => x.UsuarioId).Single();

                InformacaoLogin informacao = new InformacaoLogin
                {
                    UsuarioId = id,
                    EnderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                _context.Add(informacao);
                await _context.SaveChangesAsync();


                HttpContext.Session.SetInt32("UsuarioId", id);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, login.Email)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);


                return RedirectToAction("Index", "Curriculos");

            }
            return View(login);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Usuarios");
        }

        public JsonResult UsuarioExiste(string email)
        {
            if (!_context.Usuarios.Any(x => x.Email == email))
                return Json(true);
            return Json("Email existente");

        }
    }
}
