using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System.Collections.Generic;

namespace Dominio.Servicos
{
    public class ServicoVenda : IServicoVenda
    {
        readonly IRepositorioVenda RepositorioVenda;
        readonly IRepositorioVendaProdutos RepositorioVendaProdutos;

        public ServicoVenda(IRepositorioVenda RepositorioVenda, IRepositorioVendaProdutos RepositorioVendaProdutos)
        {
            this.RepositorioVenda = RepositorioVenda;
            this.RepositorioVendaProdutos = RepositorioVendaProdutos;
        }

        public IEnumerable<Venda> Buscar()
        {
            return RepositorioVenda.Buscar();
        }

        public Venda Buscar(int id)
        {
            return RepositorioVenda.Buscar(id);
        }

        public void Criar(Venda Venda)
        {
            RepositorioVenda.Criar(Venda);
        }

        public void Deletar(int id)
        {
            RepositorioVenda.Deletar(id);
        }

        public IEnumerable<GraficoViewModel> ListaGrafico()
        {
            return RepositorioVendaProdutos.ListaGrafico();
        }
    }
}
