using Aula_03.Data;
using Aula_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_03.Controllers
{
    public class LivroController : Controller
    {
        private DataContext dataContext;
        public IEnumerable<SelectListItem> listaEditora;
        public IEnumerable<SelectListItem> listaClassificacao;
        public LivroController(DataContext dc)
        {
            dataContext = dc;
            listaEditora = dataContext.TBEditora.ToList()
                .Select(
                    e => new SelectListItem()
                    {
                        Text = e.NomeEditora,
                        Value = e.ID.ToString()
                    }).ToList();
            listaClassificacao = dataContext.TBClassificacaoLivro.ToList()
                    .Select(
                lc => new SelectListItem()
                {
                    Text = lc.Classificacao,
                    Value = lc.ID.ToString()
                }).ToList();
        }


        private bool ExistLivro(Livro livro)
        {
            var result = dataContext.TBLivro.Where(
                    l => l.Titulo == livro.Titulo && l.DataPublicacao == livro.DataPublicacao
                ).ToList();
            return (result.Count() >= 0 || result == null) ? true : false;
        }

        public IActionResult Create()
        {
            ViewBag.Editoras = listaEditora;
            ViewBag.Classificacoes = listaClassificacao;
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public asy IActionResult Create(Livro livro)
        public async Task<ActionResult> Create(Livro livro)
        {
            ViewBag.Editoras = listaEditora;
            ViewBag.Classificacoes = listaClassificacao;

            if (
                livro.Titulo.Trim() == "" ||
                livro.Editora.ID == 0 ||
                livro.ClassificacaoLivro.ID == 0
                )
            {
                //ViewBag.TipoMensagem = "Erro";
                //ViewBag.Mensagem = "Os dados informados são inválidos";
                //return View();
                return BadRequest("Os dados informados são inválidos");
            }

            if (livro.DataPublicacao > DateTime.Today)
            {
                //ViewBag.TipoMensagem = "Erro";
                //ViewBag.Mensagem = "A data da publicação não pode ser uma data futura";
                //return View();
                return BadRequest("A data da publicação não pode ser uma data futura");
            }

            if (livro.NumeroPagina < 1)
            {
                //ViewBag.TipoMensagem = "Erro";
                //ViewBag.Mensagem = "O numero de páginas deve ser no mínimo uma páginas";
                //return View();
                return BadRequest("O numero de páginas deve ser no mínimo uma páginas");
            }

            if (!ExistLivro(livro))
            {
                //ViewBag.TipoMensagem = "Erro";
                //ViewBag.Mensagem = "O livro informado já existe";
                //return View();
                return BadRequest("O livro informado já existe");
            }

            livro.EditoraID = livro.Editora.ID;
            livro.Editora = null;

            livro.ClassificacaoLivroID = livro.ClassificacaoLivro.ID;
            livro.ClassificacaoLivro = null;

            dataContext.TBLivro.Add(livro);
            //dataContext.SaveChanges();
            await dataContext.SaveChangesAsync();

            //TempData["TipoMensagem"] = "Sucesso";
            //TempData["Mensagem"] = "Livro cadastrado com sucesso";
            //return RedirectToAction("Index");
            return Ok(true);

        }

        public IActionResult Index()
        {


            ViewBag.Editoras = listaEditora;
            ViewBag.Classificacoes = listaClassificacao;

            List<Livro> lista = dataContext.TBLivro
                .Include(e => e.Editora)
                .Include(c => c.ClassificacaoLivro)
                .ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Livro livro)
        {

            ViewBag.Editoras = listaEditora;
            ViewBag.Classificacoes = listaClassificacao;

            if (livro == null)
                return RedirectToAction("Index");
            else
            {
                //if (livro.Titulo == null)
                //    livro.Titulo = "";
                //livro.Titulo = livro.Titulo.Trim().ToUpper();
                //List<Livro> lista = dataContext.TBLivro.Where(x => x.Titulo.ToUpper().Contains(livro.Titulo)).ToList();
                string consulta = "SELECT * FROM TBLivro AS l WHERE 1 = 1 ";
                List<object> parametros = new List<object>();

                if (livro.Titulo != null && livro.Titulo.Trim() != "")
                {
                    consulta += " AND l.Titulo LIKE @titulo ";
                    parametros.Add(new SqlParameter("@titulo", "%" + livro.Titulo + "%"));
                }

                if (livro.EditoraID > 0)
                {
                    consulta += " AND l.EditoraID = @editora ";
                    parametros.Add(new SqlParameter("@editora", livro.EditoraID));
                }

                if (livro.ClassificacaoLivroID > 0)
                {
                    consulta += " AND l.ClassificacaoLivroID = @classificacao ";
                    parametros.Add(new SqlParameter("@classificacao", livro.ClassificacaoLivroID));
                }

                consulta += " AND l.AcessoOnline = @acesso ";
                parametros.Add(new SqlParameter("@acesso", (livro.AcessoOnline) ? 1 : 0));

                List<Livro> lista = dataContext.TBLivro
                    .FromSqlRaw(consulta, parametros.ToArray())
                    .Include(e => e.Editora)
                    .Include(c => c.ClassificacaoLivro)
                    .ToList();

                return View(lista);
            }
        }
        //public ActionResult Delete(int? ID)
        [HttpDelete]
        public async Task<ActionResult> Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Livro livro = dataContext.TBLivro.FirstOrDefault(x => x.ID == ID);
                if((DateTime.Today.Year - livro.DataPublicacao.Year) >= 30)
                {
                    //TempData["TipoMensagem"] = "Erro";
                    //TempData["Mensagem"] = $"Livro {livro.Titulo} possui mais de 30 anos, por isso não pode ser removido.";
                    //return RedirectToAction("Index");
                    return BadRequest($"Livro {livro.Titulo} possui mais de 30 anos, por isso não pode ser removido.");
                }
                if (livro != null)
                {
                    dataContext.TBLivro.Remove(livro);
                    //dataContext.SaveChanges();
                    await dataContext.SaveChangesAsync();

                    //TempData["TipoMensagem"] = "Sucesso";
                    //TempData["Mensagem"] = $"Livro {livro.Titulo} removido com sucesso";
                    //return RedirectToAction("Index");
                    return Ok(true);
                }
            }
            return BadRequest("ID inválido");
        }

        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                ViewBag.Editoras = listaEditora;
                ViewBag.Classificacoes = listaClassificacao;

                Livro livro = dataContext.TBLivro.FirstOrDefault(x => x.ID == ID);
                if (livro != null)
                    return View(livro);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Livro livro)
        {
            ViewBag.Editoras = listaEditora;
            ViewBag.Classificacoes = listaClassificacao;

            if (
                livro.Titulo.Trim() == "" ||
                livro.Editora.ID == 0 ||
                livro.ClassificacaoLivro.ID == 0
                )
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "Os dados informados são inválidos";
                return View();
            }

            if (livro.DataPublicacao > DateTime.Today)
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "A data da publicação não pode ser uma data futura";
                return View();
            }

            if (livro.NumeroPagina < 1)
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "O numero de páginas deve ser no mínimo uma páginas";
                return View();
            }

            if (!ExistLivro(livro))
            {
                ViewBag.TipoMensagem = "Erro";
                ViewBag.Mensagem = "O livro informado já existe";
                return View();
            }

            livro.EditoraID = livro.Editora.ID;
            livro.Editora = null;

            livro.ClassificacaoLivroID = livro.ClassificacaoLivro.ID;
            livro.ClassificacaoLivro = null;
            if (dataContext.TBLivro.Any(x => x.ID == livro.ID))
            {
                try
                {
                    dataContext.TBLivro.Update(livro);
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
            return NoContent();
        }
    }
}
