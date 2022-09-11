using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleDeUsuarios.Data;
using ControleDeUsuarios.Models;

namespace ControleDeUsuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ControleDeUsuariosContext _context;

        public UsuariosController(ControleDeUsuariosContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("/Inicio")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        [Route("/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Route("/Novo")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("/Novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tell,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var cadastroInvalido = ValidaCadastro(usuario);

                if (cadastroInvalido.Result != true)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Create));
                }

            }
            return View(usuario);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Route("EditReg/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReg(int id, [Bind("Id,Name,Tell,Email")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cadastroInvalido = ValidaCadastro(usuario);

                if (cadastroInvalido.Result != true)
                {
                    try
                    {
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.Id))
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
                else
                {
                    return RedirectToAction(nameof(Edit));
                }
            }
            return View(usuario);
        }

        [Route("/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        public async Task<Boolean> ValidaCadastro(Usuario usuario)
        {
            bool cadastroInvalido = false;
            var registrosTabela = await _context.Usuario.ToListAsync();

            foreach (var contatoCadastrado in registrosTabela)
            {
                if (usuario.Name.ToUpper() == contatoCadastrado.Name.ToUpper())
                {
                    cadastroInvalido = true;
                    return cadastroInvalido;
                }
            }

            return cadastroInvalido;
        }
    }
}

