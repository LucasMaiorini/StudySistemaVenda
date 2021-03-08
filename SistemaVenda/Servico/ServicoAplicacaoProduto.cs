using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoProduto : IServicoAplicacaoProduto
    {
        private readonly IServicoProduto ServicoDominioProduto;

        //construtor
        public ServicoAplicacaoProduto(IServicoProduto ServicoDominioProduto)
        {
            this.ServicoDominioProduto = ServicoDominioProduto;
        }

        public ProdutoViewModel Buscar(int codigoProduto)
        {
            var Produto = ServicoDominioProduto.Buscar(codigoProduto);

            //mapeamento do tipo Entidade para o ViewModel.
            var VM = new ProdutoViewModel()
            {
                Codigo = Produto.Codigo,
                Descricao = Produto.Descricao,
                Quantidade = Produto.Quantidade,
                Valor = Produto.Valor,   
                CodigoCategoria = Produto.CodigoCategoria
            };
            return VM;
        }

        public void Criar(ProdutoViewModel VM)
        {
            var Produto = new Produto()
            {
                Codigo = VM.Codigo,
                Descricao = VM.Descricao,
                Quantidade = VM.Quantidade,
                Valor = (decimal)VM.Valor,
                CodigoCategoria = (int)VM.CodigoCategoria,
            };
            ServicoDominioProduto.Criar(Produto);
        }

        public void Deletar(int id)
        {
            ServicoDominioProduto.Deletar(id);
        }

        public IEnumerable<ProdutoViewModel> Buscar()
        {
            var lista = ServicoDominioProduto.Buscar();
            var listaProduto = new List<ProdutoViewModel>();
            foreach (var item in lista)
            {
                var Produto = new ProdutoViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    Valor = item.Valor,
                    CodigoCategoria = item.CodigoCategoria,
                    DescricaoCategoria = item.Categoria.Descricao
                };
                listaProduto.Add(Produto);
            }
            return listaProduto;
        }

        public IEnumerable<SelectListItem> BuscarSelectList()
        {
            var lista = new List<SelectListItem>();
            var listaProvisoria = this.Buscar();
            foreach (var item in listaProvisoria)
            {
                var produto = new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao,
                };
                lista.Add(produto);
            }
            return lista;
        }
    }
}
