using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ClienteController : Controller
    {
        readonly IServicoAplicacaoCliente ServicoAplicacaoCliente;

        public ClienteController(IServicoAplicacaoCliente servicoAplicacaoCliente)
        {
            this.ServicoAplicacaoCliente = servicoAplicacaoCliente;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoCliente.Buscar());
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ClienteViewModel();
            if (id != null)
            {
                viewModel = ServicoAplicacaoCliente.Buscar((int)id);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                ServicoAplicacaoCliente.Criar(entidade);
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
            ServicoAplicacaoCliente.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}
