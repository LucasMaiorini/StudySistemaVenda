using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        readonly IServicoAplicacaoProduto ServicoAplicacaoProduto;
        readonly IServicoAplicacaoCategoria ServicoAplicacaoCategoria;

        public ProdutoController(IServicoAplicacaoProduto ServicoAplicacaoProduto, IServicoAplicacaoCategoria ServicoAplicacaoCategoria)
        {
            this.ServicoAplicacaoProduto = ServicoAplicacaoProduto;
            this.ServicoAplicacaoCategoria = ServicoAplicacaoCategoria;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoProduto.Buscar());
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ProdutoViewModel();
            if (id != null)
            {
                viewModel = ServicoAplicacaoProduto.Buscar((int)id);
            }
            viewModel.ListaCategorias = ServicoAplicacaoCategoria.BuscarSelectList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                ServicoAplicacaoProduto.Criar(entidade);
            }
            else
            {
                entidade.ListaCategorias = ServicoAplicacaoCategoria.BuscarSelectList();

                return View(entidade);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            ServicoAplicacaoProduto.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}
