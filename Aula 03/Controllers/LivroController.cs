using Aula_03.Data;
using Aula_03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_03.Controllers
{
    public class LivroController : Controller
    {
        private DataContext dataContext;

        public LivroController(DataContext dc)
        {
            dataContext = dc;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Livro livro)
        {
            if(ModelState.IsValid == false)
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "Os dados informados são inválidos";
                return View();
            }
            else
            {
                dataContext.Livros.Add(livro);
                dataContext.SaveChanges();

                TempData["TipoMensagem"] = "Sucesso";
                TempData["Mensagem"] = "Livro cadastrado com sucesso";
                return RedirectToAction("Index");
            }

        }
    
        public IActionResult Index()
        {
            List<Livro> lista = dataContext.Livros.ToList();
            return View(lista); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Livro livro)
        {
            if(livro == null)
                return RedirectToAction("Index");
            else
            {
                if(livro.Titulo == null)
                    livro.Titulo = "";
                livro.Titulo = livro.Titulo.Trim().ToUpper();
                List<Livro> lista = dataContext.Livros.Where(x => x.Titulo.ToUpper().Contains(livro.Titulo)).ToList();
                return View(lista);
            }
        }

        public ActionResult Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Livro livro = dataContext.Livros.FirstOrDefault(x => x.Id == ID);
                if(livro != null)
                {
                    dataContext.Livros.Remove(livro);
                    dataContext.SaveChanges();

                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = $"Livro {livro.Titulo} removido com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return NoContent();
        }

        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                Livro livro = dataContext.Livros.FirstOrDefault(x => x.Id == ID);
                if (livro != null)
                    return View(livro);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Livro livro)
        {
            if (ModelState.IsValid)
            {
                if(dataContext.Livros.Any(x => x.Id == livro.Id))
                {
                    try
                    {
                        dataContext.Livros.Update(livro);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "Erro";
                        ViewBag.Mensagem = "O livro não pode ser atualizado";
                        return View();
                    }
                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = "Livro atualizado com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return NoContent();
        }
    }
}
