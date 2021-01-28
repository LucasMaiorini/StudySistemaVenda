using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext mContext;

        public VendaController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Venda> lista = mContext.Venda.ToList();
            mContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaProdutos()
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
            };

            foreach (var item in mContext.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao
                });
            }
            return lista;

        }

        private IEnumerable<SelectListItem> ListaCliente()
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
            };

            foreach (var item in mContext.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome
                });
            }
            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new VendaViewModel
            {
                ListaClientes = ListaCliente(),
                ListaProdutos = ListaProdutos()
            };

            if (id != null)
            {
                var entidade = mContext.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                //Aqui devem estar os campos que precisam ser gravados na database
                viewModel.Codigo = entidade.Codigo;
                viewModel.Data = entidade.Data;
                viewModel.Total = entidade.Total;
                viewModel.CodigoCliente = entidade.CodigoCliente;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                var obj = new Venda()
                {
                    Codigo = entidade.Codigo,
                    Data = (DateTime)entidade.Data,
                    CodigoCliente = (int)entidade.CodigoCliente,
                    Total = entidade.Total,
                    Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(entidade.JsonProdutos)
                };
                if (entidade.Codigo == null)
                {
                    mContext.Venda.Add(obj);
                }
                else
                {
                    mContext.Entry(obj).State = EntityState.Modified;
                }
                mContext.SaveChanges();
            }
            else
            {
                entidade.ListaClientes = ListaCliente();
                entidade.ListaProdutos = ListaProdutos();
                return View(entidade);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            //var entidade = mContext.Venda.Where(ent => ent.Codigo == id).FirstOrDefault();
            var entidade = new Venda() { Codigo = id };
            mContext.Attach(entidade);
            mContext.Remove(entidade);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //Retorna o valor de um produto especificado pelo Codigo (id). É chamada quando um produto muda na View Cadastro.
        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return mContext.Produto.Where(x => x.Codigo == CodigoProduto).Select(x => x.Valor).FirstOrDefault();
        }

    }

}
