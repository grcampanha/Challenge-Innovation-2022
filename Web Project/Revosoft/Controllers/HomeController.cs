using Revosoft.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Revosoft.Context;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Revosoft.Repositories.Interfaces;

namespace Revosoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index(string? warning)
        {
            if(warning != null)
             ModelState.AddModelError("", warning);

            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha, bool manterlogado)
        {
            Usuarios usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (usuario != null)
            {
                int usuarioId = usuario.UsuariosId;
                string nome = usuario.Nome;

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,usuarioId.ToString()),
                    new Claim(ClaimTypes.Name,nome)
                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = manterlogado,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    });

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Usuário ou Senha Incorretos!");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Login", "Home");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login([Bind("Email,Senha")] string Email, string Senha)
        //{
        //    if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Senha))
        //    {
        //        var usuarios = (from us in _context.Usuarios
        //                        where us.Email == Email
        //                              || us.Senha == Senha
        //           );

        //        if (usuarios != null)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    //var resultado = from obj in _context.Usuarios select obj;

        //    //resultado = resultado.Where(x => x.Email == Email && x.Senha == Senha);
        //    ModelState.AddModelError("", "Falha ao realizar o login!!");
        //    return View();
        //}

        //public async Task<IActionResult> login(int? Email, int? Senha)
        //{
        //    if (Email == null || Senha == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuarios = await _context.Usuarios.FindAsync(Email, Senha);
        //    if (usuarios == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(usuarios);
        //}





        //[HttpPost]
        //public async Task<IActionResult> Login(string Email, string Senha, int pageindex = 1, string sortExpression = "Email")
        //{
        //    var result = _context.Usuarios.AsQueryable();

        //    if (result != null)
        //    {
        //        result = result.Where(u => u.Email.Contains(Email));

        //        result = result.Where(u => u.Senha.Contains(Senha));

        //        if(result != null)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        //if (result.Succeeded)
        //        //{
        //        //    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
        //        //    {
        //        //        return RedirectToAction("Index", "Home");
        //        //    }
        //        //    return Redirect(loginVM.ReturnUrl);
        //        //}
        //    }

        //    //var model = await PagingList.CreateAsync(result, 5, pageindex, sortExpression, "Email");
        //    //model.RouteValue = new RouteValueDictionary { { "apelido", apelido }, { "nome", nome }, { "cnpj", cnpj }, { "telefone", telefone }, { "celular", celular }, { "estado", estado }, { "categoria", categoria }, { "ativo", ativo } };

        //    ModelState.AddModelError("", "Falha ao realizar o login!!");
        //    return View(result);
        //}

        // GET: Veículos
        [Authorize]
        public async Task<IActionResult> Cars(string modelo, int pageindex = 1, string sortExpression = "Modelo")
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var resultado = _context.Veiculos.Include(s => s.Usuarios).AsQueryable();

            if (!string.IsNullOrEmpty(modelo))
            {
                resultado = resultado.Where(p => p.Modelo.Contains(modelo));
            }

            resultado = resultado.Where(u => u.UsuariosId.ToString() == userId);

            var model = await PagingList.CreateAsync(resultado, 50, pageindex, sortExpression, "Modelo");
            model.RouteValue = new RouteValueDictionary { { "modelo", modelo } };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Store(string produto, int pageindex = 1, string sortExpression = "Produto")
        {
            var resultado = _context.Store.AsQueryable();

            if (!string.IsNullOrEmpty(produto))
            {
                resultado = resultado.Where(p => p.Produto.Contains(produto));
            }
            var model = await PagingList.CreateAsync(resultado, 50, pageindex, sortExpression, "Produto");
            model.RouteValue = new RouteValueDictionary { { "produto", produto } };

            return View(model);
        }

        [Authorize]
        public IActionResult Roda()
        {
            return View();
        }

        // GET: Skills/5
        [Authorize]
        public async Task<IActionResult> Skills(int? id)
        {
            string warning = "Adicione um veículo para acessar as Skills!";
            if (id == null)
            {
                return NotFound();
            }
            var verificacao = _context.Veiculos.FirstOrDefault(vn => vn.UsuariosId == id);
            if (verificacao == null)
            {
                return RedirectToAction("Index", "Home", new { warning });
            }
            var veiculo = _context.Veiculos.Where(v => v.UsuariosId == id).OrderBy(p => p.VeiculosId).OrderByDescending(p => p.VeiculosId).Take(1).Single();
            var pecas = _context.Pecas.Where(p => p.VeiculosId == veiculo.VeiculosId).OrderBy(p => p.PecasId).OrderByDescending(p => p.PecasId).Take(1).Single();

            var veiculos = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.VeiculosId == pecas.VeiculosId);
            if (veiculos == null)
            {
                return NotFound();
            }
            
            return View(veiculos);
        }

        // GET: Skills/5
        [Authorize]
        public async Task<IActionResult> SkillsCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pecas = _context.Pecas.Where(p => p.VeiculosId == id).OrderBy(p => p.PecasId).OrderByDescending(p => p.PecasId).Take(1).Single();
            if (pecas == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.VeiculosId == id);
            if (veiculos == null)
            {
                return NotFound();
            }

            return View(veiculos);
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signup([Bind("UsuariosId,Nome,Sobrenome,Senha,Email,Telefone")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(usuarios);
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // GET: Cars/AddCar
        [Authorize]
        public IActionResult AddCar()
        {
            ViewData["UsuariosId"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "Nome");
            return View();
        }

        // POST: Cars/AddCar
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar([Bind("VeiculosId,Placa,Modelo,Ano,UsuariosId")] Veiculos veiculos)
        {
            veiculos.Placa = veiculos.Placa.ToUpper();
            if (ModelState.IsValid)
            {
                _context.Add(veiculos);
                await _context.SaveChangesAsync();
                

                Pecas pecas = new Pecas()
                {
                    VeiculosId = veiculos.VeiculosId,
                    CambioScore = 100,
                    MotorScore = 100,
                    PneuScore = 100,
                    BateriaScore = 100
                };

                _context.Add(pecas);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Cars));
            }
            ViewData["UsuariosId"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "Nome", veiculos.UsuariosId);
            return View(veiculos);
        }

        // GET: Veiculos/DeleteVeiculo/5
        [Authorize]
        public async Task<IActionResult> DeleteVeiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.VeiculosId == id);
            if (veiculos == null)
            {
                return NotFound();
            }

            return View(veiculos);
        }

        // POST: Veiculos/Delete/5
        [Authorize]
        [HttpPost, ActionName("DeleteVeiculo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculos = await _context.Veiculos.FindAsync(id);
            _context.Veiculos.Remove(veiculos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cars));
        }
    }
}