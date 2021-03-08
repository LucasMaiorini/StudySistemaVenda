using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;
using System;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        readonly IServicoAplicacaoVenda ServicoAplicacaoVenda;
        readonly IServicoAplicacaoProduto ServicoAplicacaoProduto;
        readonly IServicoAplicacaoCliente ServicoAplicacaoCliente;


        public VendaController(
            IServicoAplicacaoVenda ServicoAplicacaoVenda,
            IServicoAplicacaoProduto ServicoAplicacaoProduto,
            IServicoAplicacaoCliente ServicoAplicacaoCliente)
        {
            this.ServicoAplicacaoVenda = ServicoAplicacaoVenda;
            this.ServicoAplicacaoProduto = ServicoAplicacaoProduto;
            this.ServicoAplicacaoCliente = ServicoAplicacaoCliente;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoVenda.Buscar());
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var VM = new VendaViewModel();

            if (id != null)
            {
                VM = ServicoAplicacaoVenda.Buscar((int)id);
            }
            VM.ListaClientes = ServicoAplicacaoCliente.BuscarSelectList();
            VM.ListaProdutos = ServicoAplicacaoProduto.BuscarSelectList();

            return View(VM);
        }


        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            //Valida a ViewModel
            if (ModelState.IsValid)
            {
                ServicoAplicacaoVenda.Criar(entidade);
            }
            else
            {
                entidade.ListaClientes = ServicoAplicacaoCliente.BuscarSelectList();
                entidade.ListaProdutos = ServicoAplicacaoProduto.BuscarSelectList();

                return View(entidade);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            ServicoAplicacaoVenda.Deletar(id);
            return RedirectToAction("Index");
        }

        //Retorna o valor de um produto especificado pelo Codigo (id). É chamada quando um produto muda na View Cadastro.
        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return (decimal)ServicoAplicacaoProduto.Buscar(CodigoProduto).Valor;

        }

    }

}
