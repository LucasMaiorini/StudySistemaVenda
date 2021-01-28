using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        protected ApplicationDbContext mContext;

        public ProdutoController(ApplicationDbContext context)
        {
            this.mContext = context;
        }

        public IActionResult Index()
        {
            var lista = mContext.Produto.Include(x => x.Categoria).ToList();
            mContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaCategoria()
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
            };

            foreach (var item in mContext.Categoria.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao
                });
            }
            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ProdutoViewModel
            {
                ListaCategorias = ListaCategoria()
            };
            if (id != null)
            {
                var entidade = mContext.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
                viewModel.Quantidade = entidade.Quantidade;
                viewModel.Valor = entidade.Valor;
                viewModel.CodigoCategoria = entidade.CodigoCategoria;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                var obj = new Produto()
                {
                    Codigo = entidade.Codigo,
                    Descricao = entidade.Descricao,
                    Quantidade = entidade.Quantidade,
                    Valor = (decimal)entidade.Valor,
                    CodigoCategoria = (int)entidade.CodigoCategoria
                };
                if (entidade.Codigo == null)
                {
                    mContext.Produto.Add(obj);
                }
                else
                {
                    mContext.Entry(obj).State = EntityState.Modified;
                }
                mContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                entidade.ListaCategorias = ListaCategoria();
                return View(entidade);
            }

        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entidade = mContext.Produto.Where(ent => ent.Codigo == id).FirstOrDefault();
            mContext.Produto.Remove(entidade);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
