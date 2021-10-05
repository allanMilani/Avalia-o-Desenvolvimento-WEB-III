using Aula_03.Data;
using Aula_03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Aula_03.Controllers
{
    public class EditoraController : Controller
    {
        private DataContext dataContext;
        public EditoraController(DataContext dc)
        {
            dataContext = dc;
        }
        public IActionResult Index()
        {
            List<Editora> lista = dataContext.TBEditora.ToList();
            return View(lista);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Editora editora)
        {
            if(editora == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (editora.NomeEditora == null)
                    editora.NomeEditora = "";
                editora.NomeEditora = editora.NomeEditora.Trim().ToUpper();
                List<Editora> lista = dataContext.TBEditora.Where(
                                        x => x.NomeEditora.ToUpper().Contains(editora.NomeEditora)
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
        public IActionResult Create(Editora editora)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "Os dados informados são inválidos";
                return View();
            }
            else
            {
                try
                {
                    dataContext.TBEditora.Add(editora);
                    dataContext.SaveChanges();
                }
                catch
                {
                    ViewBag.TipoMensagem = "Erro";
                    ViewBag.Mensagem = "A editora não pode ser cadastrada";
                    return View();
                }

                TempData["TipoMensagem"] = "Sucesso";
                TempData["Mensagem"] = "Editora cadastrada com sucesso";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                Editora editora = dataContext.TBEditora.FirstOrDefault(x => x.ID == ID);
                if (editora != null)
                    return View(editora);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Editora editora)
        {
            if (ModelState.IsValid)
            {
                if (dataContext.TBEditora.Any(x => x.ID == editora.ID))
                {
                    try
                    {
                        dataContext.TBEditora.Update(editora);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "Erro";
                        ViewBag.Mensagem = "A editora não pode ser atualizada";
                        return View();
                    }

                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = "Editora atualizada com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return NoContent();
        }

        public ActionResult Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Editora editora = dataContext.TBEditora.FirstOrDefault(x => x.ID == ID);
                if (editora != null)
                {
                    try
                    {
                        dataContext.TBEditora.Remove(editora);
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.TipoMensagem = "Erro";
                        ViewBag.Mensagem = "A editora não pode ser deletada";
                        return View();
                    }


                    TempData["TipoMensagem"] = "Sucesso";
                    TempData["Mensagem"] = $"Editora {editora.NomeEditora} deletada com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return NoContent();
        }

    }
}
