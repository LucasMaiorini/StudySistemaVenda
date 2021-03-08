using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Newtonsoft.Json;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoVenda : IServicoAplicacaoVenda
    {
        private readonly IServicoVenda ServicoDominioVenda;

        //construtor
        public ServicoAplicacaoVenda(IServicoVenda ServicoDominioVenda)
        {
            this.ServicoDominioVenda = ServicoDominioVenda;
        }

        public VendaViewModel Buscar(int codigoVenda)
        {
            var Venda = ServicoDominioVenda.Buscar(codigoVenda);

            //mapeamento do tipo Entidade para o ViewModel.
            var VM = new VendaViewModel()
            {
                Codigo = Venda.Codigo,
                Data = Venda.Data,
                CodigoCliente = Venda.CodigoCliente,
                Total = Venda.Total
            };
            return VM;
        }

        public void Criar(VendaViewModel VM)
        {
            var Venda = new Venda()
            {
                Codigo = VM.Codigo,
                Data = (DateTime)VM.Data,
                CodigoCliente = (int)VM.CodigoCliente,
                Total = VM.Total,
                Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(VM.JsonProdutos)
            };
            ServicoDominioVenda.Criar(Venda);
        }

        public void Deletar(int id)
        {
            ServicoDominioVenda.Deletar(id);
        }

        public IEnumerable<VendaViewModel> Buscar()
        {
            var lista = ServicoDominioVenda.Buscar();
            var listaVenda = new List<VendaViewModel>();
            foreach (var item in lista)
            {
                var Venda = new VendaViewModel()
                {
                    Codigo = item.Codigo,
                    Data = item.Data,
                    CodigoCliente = item.CodigoCliente,
                    Total = item.Total
                };
                listaVenda.Add(Venda);
            }
            return listaVenda;
        }

        public IEnumerable<GraficoViewModel> ListaGrafico()
        {
            List<GraficoViewModel> lista = new List<GraficoViewModel>();
            var auxLista = ServicoDominioVenda.ListaGrafico();

            foreach (var item in auxLista)
            {
                GraficoViewModel grafico = new GraficoViewModel()
                {
                    CodigoProduto = item.CodigoProduto,
                    Descricao = item.Descricao,
                    TotalVendido = item.TotalVendido
                };
                lista.Add(grafico);
            }
            return lista;
        }
    }
}
