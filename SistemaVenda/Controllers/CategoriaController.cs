using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        readonly IServicoAplicacaoCategoria ServicoAplicacaoCategoria;

        public CategoriaController(IServicoAplicacaoCategoria servicoAplicacaoCategoria)
        {
            ServicoAplicacaoCategoria = servicoAplicacaoCategoria;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoCategoria.Buscar());
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new CategoriaViewModel();
            if (id != null)
            {
                viewModel = ServicoAplicacaoCategoria.Buscar((int)id);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                ServicoAplicacaoCategoria.Criar(entidade);
            }
            else
            {
                return View(entidade);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            ServicoAplicacaoCategoria.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}
