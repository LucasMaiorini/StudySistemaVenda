using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext mContext;

        public CategoriaController(ApplicationDbContext context)
        {
            this.mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> listaCategoria = mContext.Categoria.ToList();
            mContext.Dispose();
            return View(listaCategoria);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();
            if (id != null)
            {
                var entidade = mContext.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                var obj = new Categoria()
                {
                    Codigo = entidade.Codigo,
                    Descricao = entidade.Descricao
                };
                if (entidade.Codigo == null)
                {
                    mContext.Categoria.Add(obj);
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
                return View(entidade);
            }

        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entidade = mContext.Categoria.Where(ent => ent.Codigo == id).FirstOrDefault();
            mContext.Categoria.Remove(entidade);  
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
