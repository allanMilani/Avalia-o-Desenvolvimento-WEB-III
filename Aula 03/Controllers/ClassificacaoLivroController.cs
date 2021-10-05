using Aula_03.Data;
using Aula_03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_03.Controllers
{
    public class ClassificacaoLivroController : Controller
    {
        private DataContext dataContext;

        public ClassificacaoLivroController(DataContext dc)
        {
            dataContext = dc;
        }

        public IActionResult Index()
        {
            List<ClassificacaoLivro> lista = dataContext.TBClassificacaoLivro.ToList();
            return View(lista);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ClassificacaoLivro classificacaoLivro)
        {
            if (classificacaoLivro == null)
                return RedirectToAction("Index");
            else
            {
                if (classificacaoLivro.Classificacao == null)
                    classificacaoLivro.Classificacao = "";
                classificacaoLivro.Classificacao = classificacaoLivro.Classificacao.Trim().ToUpper();
                List<ClassificacaoLivro> lista = dataContext.TBClassificacaoLivro.Where(
                                                x => x.Classificacao.ToUpper().Contains(classificacaoLivro.Classificacao)
                                                ).ToList();
                return View(lista);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassificacaoLivro classificacaoLivro)
        {
            if(ModelState.IsValid == false)
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "Os dados informados são inválidos";
                return View();
            }
            else
            {
                try
                {
                    dataContext.TBClassificacaoLivro.Add(classificacaoLivro);
                    dataContext.SaveChanges();
                }
                catch
                {
                    ViewBag.TipoMensagem = "Erro";
                    ViewBag.Mensagem = "A classificação não pode ser cadastrada";
                    return View();
                }

                TempData["TipoMensagem"] = "Sucesso";
                TempData["Mensagem"] = "Classificação cadastrada com sucesso";
                return RedirectToAction("Index");

            }
        }

        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                ClassificacaoLivro classificacaoLivro = dataContext.TBClassificacaoLivro.FirstOrDefault(x => x.ID == ID);
                if (classificacaoLivro != null)
                    return View(classificacaoLivro);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClassificacaoLivro classificacaoLivro)
        {
            if (ModelState.IsValid)
            {
                if (dataContext.TBClassificacaoLivro.Any(x => x.ID == classificacaoLivro.ID))
                {
                    try{
                        dataContext.TBClassificacaoLivro.Update(classificacaoLivro);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "Erro";
                        ViewBag.Mensagem = "A classificação não pode ser atualizada";
                    }

                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = "Classificação atualizada com sucessso";
                    return RedirectToAction("Index");

                }
            }
            return NoContent();
        }

        public ActionResult Delete(int? ID)
        {
            if (ID.HasValue)
            {
                ClassificacaoLivro classificacaoLivro = dataContext.TBClassificacaoLivro.FirstOrDefault(x => x.ID == ID);
                if(classificacaoLivro != null)
                {
                    try
                    {
                        dataContext.TBClassificacaoLivro.Remove(classificacaoLivro);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "Erro";
                        ViewBag.Mensagem = "A classficicação não pode ser deletada";
                        return View();
                    }

                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = $"Classificação {classificacaoLivro.Classificacao} deletada com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return NoContent();
        }

    }
}
