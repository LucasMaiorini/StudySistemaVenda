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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext mContext;

        public ClienteController(ApplicationDbContext context)
        {
            this.mContext = context;
        }

        public IActionResult Index()
        {
            var lista = mContext.Cliente.ToList();
            mContext.Dispose();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ClienteViewModel();
            if (id != null)
            {
                var entidade = mContext.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Nome = entidade.Nome;
                viewModel.CNPJ_CPF = entidade.CNPJ_CPF;
                viewModel.Email = entidade.Email;
                viewModel.Celular = entidade.Celular;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                var obj = new Cliente()
                {
                    Codigo = entidade.Codigo,
                    Nome = entidade.Nome,
                    CNPJ_CPF = entidade.CNPJ_CPF,
                    Email = entidade.Email,
                    Celular = entidade.Celular
                };
                if (entidade.Codigo == null)
                {
                    mContext.Cliente.Add(obj);
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
            var entidade = mContext.Cliente.Where(ent => ent.Codigo == id).FirstOrDefault();
            mContext.Cliente.Remove(entidade);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
