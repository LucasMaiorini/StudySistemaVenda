using Dominio.Repositorio;
using Repositorio.Contexto;
using SistemaVenda.Dominio.Entidades;

namespace Repositorio.Entidades
{
    public class RepositorioCategoria : Repositorio<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoria(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
