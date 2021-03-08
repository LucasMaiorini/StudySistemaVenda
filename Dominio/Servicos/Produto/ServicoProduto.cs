using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.Entidades;
using System.Collections.Generic;

namespace Dominio.Servicos
{
    public class ServicoProduto : IServicoProduto
    {
        readonly IRepositorioProduto RepositorioProduto;

        public ServicoProduto(IRepositorioProduto RepositorioProduto)
        {
            this.RepositorioProduto = RepositorioProduto;
        }

        public IEnumerable<Produto> Buscar()
        {
            return RepositorioProduto.Buscar();
        }

        public Produto Buscar(int id)
        {
            return RepositorioProduto.Buscar(id);
        }

        public void Criar(Produto Produto)
        {
            RepositorioProduto.Criar(Produto);
        }

        public void Deletar(int id)
        {
            RepositorioProduto.Deletar(id);
        }
    }
}
