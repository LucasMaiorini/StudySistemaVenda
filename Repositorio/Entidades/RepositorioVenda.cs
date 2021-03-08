using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using SistemaVenda.Dominio.Entidades;
using System.Linq;

namespace Repositorio.Entidades
{
    public class RepositorioVenda : Repositorio<Venda>, IRepositorioVenda
    {
        public RepositorioVenda(ApplicationDbContext dbContext) : base(dbContext) { }

        public override void Deletar(int id)
        {
            var listaProdutos = DbSetContext
                .Where(y => y.Codigo == id)
                .Select(x => x.Produtos)
                .AsNoTracking().ToList();

            foreach (var item in listaProdutos[0])
            {

                var vendaProdutos = new VendaProdutos()
                {
                    CodigoVenda = item.CodigoVenda,
                    CodigoProduto = item.CodigoProduto
                };
                //Delete dos produtos da venda
                db.Set<VendaProdutos>();
                db.Attach(vendaProdutos);
                db.Remove(vendaProdutos);
                //db.SaveChanges();
                //Delete da venda
                base.Deletar(id);
            }

        }
    }
}
