using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using SistemaVenda.Dominio.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Entidades
{
    public class RepositorioVendaProdutos : IRepositorioVendaProdutos
    {
        protected ApplicationDbContext DbSetContext;

        public RepositorioVendaProdutos(ApplicationDbContext DbContext)
        {
            this.DbSetContext = DbContext;
        }

        public IEnumerable<GraficoViewModel> ListaGrafico()
        {
            var lista = DbSetContext.VendaProdutos.Include(x => x.Produto).AsNoTracking().AsEnumerable()
                .GroupBy(x => x.CodigoProduto).Select(x => new GraficoViewModel
                {
                    CodigoProduto = x.First().CodigoProduto,
                    Descricao = x.First().Produto.Descricao,
                    TotalVendido = x.Sum(z => z.Quantidade)
                })
                .ToList();
            return lista;
        }
    }
}
