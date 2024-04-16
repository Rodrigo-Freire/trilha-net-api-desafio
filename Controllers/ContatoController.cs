using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ProjetoMVC_FInal.Context;
using _ProjetoMVC_FInal.Models;
using Microsoft.AspNetCore.Mvc;

namespace _ProjetoMVC_FInal.Controllers
{
    public class ContatoController : Controller
    {

        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        public IActionResult Editar(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return RedirectToAction(nameof(Index));

            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return RedirectToAction(nameof(Index));

            return View(contato);
        }
        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return RedirectToAction(nameof(Index));

            return View(contato);
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}